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
using Adnc.Fstorch.System.Application.Dtos.CodeBase;
using System.Reflection;
using Adnc.Fstorch.System.Application.Dtos.Setting;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class SystemCompanyInfoService : AbstractAppService, ISystemCompanyInfoService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<SystemCompanyInfo> _SystemCompanyInfoRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public SystemCompanyInfoService(IEfRepository<SystemCompanyInfo> systemCompanyInfoRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _SystemCompanyInfoRepository = systemCompanyInfoRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;

        }

        #region 信息
        public async Task<ReturnString> DeleteSystemCompanyInfoAsync(long CompanyID, string ProviderID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            SystemCompanyInfo cinfo = await _SystemCompanyInfoRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) && x.ProviderID.Equals(ProviderID));
            if (cinfo != null)
            {
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<SystemCompanyInfo>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ProviderID.IsNotNullOrEmpty(), x => x.ProviderID.Equals(ProviderID));
                    int i = await _SystemCompanyInfoRepository.DeleteRangeAsync(whereExpression);
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
            //返回当前Msgcode的缓存值
            return rn;
        }

        public async Task<ReturnString> InsertSystemCompanyInfoAsync(SystemCompanyInfoDto inputDto)
        {
            var input = Mapper.Map<SystemCompanyInfo>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;

            SystemCompanyInfo cinfo = await _SystemCompanyInfoRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) &&
             x.ProviderID.Equals(input.ProviderID));
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    int irn = await _SystemCompanyInfoRepository.InsertAsync(input);
                    if (irn >= 0)
                    {
                        rn.MsgValue = "1";
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        rn.MsgValue = "0";
                        await _unitOfWork.RollbackAsync();
                    }
                }
                catch
                {
                    rn.MsgValue = "0";
                    await _unitOfWork.RollbackAsync();
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn;
        }

        public async Task<ReturnString> ModiSystemCompanyInfoAsync(long CompanyID, string ProviderID, SystemCompanyInfoDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            SystemCompanyInfo cinfo = await _SystemCompanyInfoRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.ProviderID.Equals(ProviderID), noTracking: false);
            if (cinfo != null)
            {
                if (cinfo.ProviderName != input.ProviderName)
                {
                    //检测工作组名称相同企业是否重复
                    int n = await _SystemCompanyInfoRepository.CountAsync(x => x.CompanyID.Equals(CompanyID) && x.ProviderName.Equals(input.ProviderName) && x.ProviderID.Equals(input.ProviderID) == false);
                    if (n > 0)
                    {
                        //存在重复名称,不能更新
                        rn.MsgValue = "3";
                        return rn;
                    }
                    cinfo.ProviderName = input.ProviderName;//可以更新名称
                }
                //复制
                cinfo.ProviderParentID = input.ProviderParentID;
                cinfo.ProviderAddress = input.ProviderAddress;
                cinfo.Lat = input.Lat;
                cinfo.Lng = input.Lng;
                cinfo.ProviderPhone = input.ProviderPhone;
                //。。。。。。。。
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _SystemCompanyInfoRepository.UpdateAsync(cinfo);
                    if (i > 0)
                    {
                        rn.MsgValue = "1";
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        rn.MsgValue = "0";
                        await _unitOfWork.RollbackAsync();
                    }
                }
                catch {
                    rn.MsgValue = "0";
                    await _unitOfWork.RollbackAsync();
                }
            }
            else
            {
                rn.MsgValue = "2";
            }
            return rn;
        }


        public async Task<List<SystemCompanyInfo>> QuerySystemCompanyInfoAsync(long CompanyID, string ProviderParentID, string ProviderID, string ProviderType, string ProviderStatus, string ProviderPhone)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<SystemCompanyInfo>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ProviderID.IsNotNullOrEmpty(), x => x.ProviderID.Equals(ProviderID))
             .AndIf(ProviderStatus.IsNotNullOrEmpty(), x => x.ProviderStatus.Equals(ProviderStatus))
             .AndIf(ProviderParentID.IsNotNullOrEmpty(), x => x.ProviderParentID.Equals(ProviderParentID))
             .AndIf(ProviderPhone.IsNotNullOrEmpty(), x => x.ProviderPhone.Equals(ProviderPhone))
             .AndIf(ProviderType.IsNotNullOrEmpty(), x => x.ProviderType.Equals(ProviderType));

            return _SystemCompanyInfoRepository.Where(whereExpression).OrderBy(x => x.ProviderID).ToList();
        }

        public async Task<string> FindLanguageAsync(string TableName, string TranslateName)
        {
            return await _cacheService.FindLanguageFromCacheAsync("B", TableName, TranslateName);
        }

        #endregion

    }
}
