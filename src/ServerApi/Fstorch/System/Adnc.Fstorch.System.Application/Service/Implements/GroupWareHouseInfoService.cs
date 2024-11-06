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
using Adnc.Shared.Application.Contracts.Dtos;
using System.Reflection;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class GroupWareHouseInfoService : AbstractAppService, IGroupWareHouseInfoService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<GroupWareHouseInfo> _groupwarehouseinfoRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public GroupWareHouseInfoService(IEfRepository<GroupWareHouseInfo> groupwarehouseinfoRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _groupwarehouseinfoRepository = groupwarehouseinfoRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
        }

        #region 工作组仓库信息
        public async Task<ReturnString> DeleteGroupWareHouseInfoAsync(long CompanyID,long GroupID, long WarehouseCode, long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            var whereExpression = ExpressionCreator
             .New<GroupWareHouseInfo>()
             .AndIf(CompanyID>0,x=>x.CompanyID.Equals(CompanyID))
             .AndIf(GroupID > 0, x => x.GroupID.Equals(GroupID))
             .AndIf(WarehouseCode > 0, x => x.WarehouseCode.Equals(WarehouseCode))
             .AndIf(ID > 0, x => x.Id.Equals(ID));
             
            int i=await _groupwarehouseinfoRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertGroupWareHouseInfoAsync(GroupWareHouseInfoDto inputDto)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            var input = Mapper.Map<GroupWareHouseInfo>(inputDto);
            _unitOfWork.BeginTransaction();
            int n = 0;

            int i = await _groupwarehouseinfoRepository.CountAsync(x => x.CompanyID.Equals(input.CompanyID) &&
            x.GroupID.Equals(input.GroupID) && x.WarehouseCode.Equals(input.WarehouseCode));
            if (i <= 0)
            {
                //找不到相同的信息，则增加
                input.Id = IdGenerater.GetNextId();
                n = await _groupwarehouseinfoRepository.InsertAsync(input);

            }

            _unitOfWork.Commit();
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

        public async Task<List<GroupWareHouseInfo>> QueryGroupWareHouseInfoAsync(long CompanyID,long GroupID,long WarehouseCode)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<GroupWareHouseInfo>()
             .AndIf(CompanyID>0,x=>x.CompanyID.Equals(CompanyID))
             .AndIf(GroupID > 0, x => x.GroupID.Equals(GroupID))
             .AndIf(WarehouseCode > 0,x=>x.WarehouseCode.Equals(WarehouseCode));
            return _groupwarehouseinfoRepository.Where(whereExpression).ToList();
        }


        #endregion

    }
}
