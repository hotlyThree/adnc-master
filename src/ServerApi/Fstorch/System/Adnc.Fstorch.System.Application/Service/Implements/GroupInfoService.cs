using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Infra.IRepositories;
using Adnc.Shared.Application.Services;
using Adnc.Fstorch.System.Application.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adnc.Infra.IdGenerater.Yitter;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Adnc.Fstorch.System.Repository;
using Adnc.Shared.Application.Contracts.ResultModels;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.ComponentModel.Design;
using Adnc.Fstorch.System.Application.Dtos.Group;
using System.Reflection;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class GroupInfoService : AbstractAppService, IGroupInfoService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<GroupInfo> _groupinfoRepository;
        private readonly IEfRepository<GroupUserInfo> _groupuserinfoRepository;
        private readonly IEfRepository<GroupMenuInfo> _groupmenuinfoRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public GroupInfoService(IEfRepository<GroupInfo> groupinfoRepository, IEfRepository<GroupUserInfo> groupuserinfoRepository,
            CacheService cacheService, IUnitOfWork unitOfWork, IEfRepository<GroupMenuInfo> groupmenuinfoRepository)
        {
            _groupinfoRepository = groupinfoRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            _groupuserinfoRepository = groupuserinfoRepository;
            _groupmenuinfoRepository = groupmenuinfoRepository;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteGroupInfoAsync(long CompanyID,long GroupID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            GroupInfo cinfo = await _groupinfoRepository.FindAsync(x => x.Id.Equals(GroupID) && x.CompanyID.Equals(CompanyID));
            if (cinfo != null)
            {
                
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _groupinfoRepository.DeleteAsync(GroupID);//删除工作组
                    await _groupuserinfoRepository.DeleteRangeAsync(x => x.GroupID.Equals(GroupID));//删除工作组对应的操作人员
                    await _groupmenuinfoRepository.DeleteRangeAsync(x => x.GroupID.Equals(GroupID));//删除工作组菜单权限
                    _unitOfWork.Commit();    
                    rn.MsgValue = "1";   
                }
                catch
                {
                    _unitOfWork.Rollback();
                    rn.MsgValue = "0";
                }                
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn;
        }

        public async Task<ReturnString> InsertGroupInfoAsync(GroupInfoDto inputDto)
        {
            var input = Mapper.Map<GroupInfo>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            if (input.CompanyID == 0 || input.ProviderID == "" || input.GroupName == "")
            {
                rn.MsgValue = "3";
                return rn;
            }
            GroupInfo cinfo = await _groupinfoRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
            x.ProviderID.Equals(input.ProviderID) && x.GroupName.Equals(input.GroupName) );
            if (cinfo == null)
            {
                //查找当前工作组不存在的情况，则新增

                input.Id = IdGenerater.GetNextId();
                int irn = await _groupinfoRepository.InsertAsync(input);
                if (irn >= 0)
                {
                    rn.MsgValue = "1";
                }
                else
                {
                    rn.MsgValue = "0";
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn; 
        }

        public async Task<ReturnString> ModiGroupInfoAsync(long CompanyID, long GroupID, GroupInfoDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            if (input.CompanyID != CompanyID)
            {
                rn.MsgValue = "2";
                return rn;
            }
            GroupInfo cinfo = await _groupinfoRepository.FindAsync(x => x.Id.Equals(GroupID) && x.CompanyID.Equals(CompanyID), noTracking: false);
            if (cinfo != null)
            {
                if (cinfo.GroupName != input.GroupName)
                {
                    //检测工作组名称相同企业是否重复
                    int n= await _groupinfoRepository.CountAsync(x => x.CompanyID.Equals(CompanyID)  && x.ProviderID.Equals(input.ProviderID) && x.GroupName.Equals(input.GroupName) && x.Id.Equals(GroupID)==false);
                    if (n > 0)
                    {
                        //存在重复名称
                        rn.MsgValue = "0";
                        return rn;
                    }
                    cinfo.GroupName = input.GroupName;
                }
                //复制
                cinfo.GroupDescribe = input.GroupDescribe;                
                cinfo.OnlyOneAgent = input.OnlyOneAgent;
                cinfo.OnlyOnebranch = input.OnlyOnebranch;
                cinfo.OnlyOneSelf = input.OnlyOneSelf;
                cinfo.OnlyOneProvider = input.OnlyOneProvider;
                int i = await _groupinfoRepository.UpdateAsync(cinfo);
                if (i > 0)
                {
                    rn.MsgValue = "1";
                }
                else
                {
                    rn.MsgValue = "0";
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn;
        }

        public async Task<List<GroupInfo>> QueryGroupInfoAsync(long CompanyID, string ProviderID, string GroupName, string GroupDescribe)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<GroupInfo>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ProviderID.IsNotNullOrEmpty(), x => x.ProviderID.Equals(ProviderID))
             .AndIf(GroupName.IsNotNullOrEmpty(), x => EF.Functions.Like(x.GroupName, $"%{GroupName}%"))
             .AndIf(GroupDescribe.IsNotNullOrEmpty(), x => EF.Functions.Like(x.GroupDescribe, $"%{GroupDescribe}%"));

            return _groupinfoRepository.Where(whereExpression).ToList();
        }


        #endregion

    }
}
