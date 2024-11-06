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
    public class GroupUserInfoService : AbstractAppService, IGroupUserInfoService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<GroupUserInfo> _groupuserinfoRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public GroupUserInfoService(IEfRepository<GroupUserInfo> groupuserinfoRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _groupuserinfoRepository = groupuserinfoRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
        }

        #region 工作组用户信息
        public async Task<ReturnString> DeleteGroupUserInfoAsync(long CompanyID,long GroupID, long UserID, long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            var whereExpression = ExpressionCreator
             .New<GroupUserInfo>()
             .AndIf(CompanyID>0,x=>x.CompanyID.Equals(CompanyID))
             .AndIf(GroupID > 0, x => x.GroupID.Equals(GroupID))
             .AndIf(UserID > 0, x => x.UserID.Equals(UserID))
             .AndIf(ID > 0, x => x.Id.Equals(ID));
             
            int i=await _groupuserinfoRepository.DeleteRangeAsync(whereExpression);
            if (i >= 0)
            {
                rn.MsgValue = i.ToString();
            }
            else
            {
                rn.MsgValue = "0";
            }
            return rn;
        }

        public async Task<ReturnString> InsertGroupUserInfoAsync(GroupUserInfoDto inputDto)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;

            int n=0;
            var input = Mapper.Map<GroupUserInfo>(inputDto);
            int i = await _groupuserinfoRepository.CountAsync(x => x.CompanyID.Equals(input.CompanyID) &&
                 x.GroupID.Equals(input.GroupID) && x.UserID.Equals(input.UserID));
            if (i <= 0)
            {
                _unitOfWork.BeginTransaction();
                //找不到相同的信息，则增加
                input.Id = IdGenerater.GetNextId();
                n=await _groupuserinfoRepository.InsertAsync(input);
                _unitOfWork.Commit();
            }
            
            if (n >= 0)
            {
                rn.MsgValue = n.ToString();
            }
            else
            {
                rn.MsgValue = "0";
            }
            
            return rn; 
        }

        public async Task<List<GroupUserInfo>> QueryGroupUserInfoAsync(long CompanyID,long GroupID,long UserID)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<GroupUserInfo>()
             .AndIf(CompanyID>0,x=>x.CompanyID.Equals(CompanyID))             
             .AndIf(GroupID > 0, x => x.GroupID.Equals(GroupID))
             .AndIf(UserID>0,x=>x.UserID.Equals(UserID));
            return _groupuserinfoRepository.Where(whereExpression).ToList();
        }


        #endregion

    }
}
