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
using MongoDB.Bson;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class Bm_Relation_ComparisonService : AbstractAppService, IBm_Relation_ComparisonService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_Relation_Comparison> _Bm_Relation_ComparisonRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_Relation_ComparisonService(IEfRepository<Bm_Relation_Comparison> bm_Relation_ComparisonRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Bm_Relation_ComparisonRepository = bm_Relation_ComparisonRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmRelationComparisonAsync(long CompanyID,long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Relation_Comparison cinfo = await _Bm_Relation_ComparisonRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID)
                && x.Id.Equals(ID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_Relation_Comparison>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ID>0, x => x.Id.Equals(ID));
                    int i = await _Bm_Relation_ComparisonRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertBmRelationComparisonAsync(Bm_Relation_ComparisonDto inputDto)
        {
            var input = Mapper.Map<Bm_Relation_Comparison>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;

            Bm_Relation_Comparison cinfo = await _Bm_Relation_ComparisonRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.ServicetypeID.Equals(input.ServicetypeID) && x.ServiceProcessID.Equals(input.ServiceProcessID) &&
             x.BrandContentName.Equals(input.BrandContentName));
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    input.Id=IdGenerater.GetNextId();
                    int irn = await _Bm_Relation_ComparisonRepository.InsertAsync(input);
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

        public async Task<ReturnString> ModiBmRelationComparisonAsync(long CompanyID, long ID, Bm_Relation_ComparisonDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Relation_Comparison cinfo = await _Bm_Relation_ComparisonRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.Id.Equals(ID), noTracking: false);
            if (cinfo != null)
            {
                //检测工作组名称相同企业是否重复
                int n= await _Bm_Relation_ComparisonRepository.CountAsync(x => x.CompanyID.Equals(CompanyID)  
                    && x.Id.Equals(ID)==false &&
                    x.ServicetypeID.Equals(input.ServicetypeID) && 
                    x.ServiceProcessID.Equals(input.ServiceProcessID) &&
                    x.ServiceBrandID.Equals(input.ServiceBrandID) &&
                    x.BrandContentName.Equals(input.BrandContentName)                        
                    );
                if (n > 0)
                {
                    //存在重复名称,不能更新
                    rn.MsgValue = "3";
                    return rn;
                }
                //复制
                cinfo.ServiceProcessID = input.ServiceProcessID;                
                cinfo.ServicetypeID = input.ServicetypeID;
                cinfo.ServiceBrandID = input.ServiceBrandID;
                cinfo.BrandContentName = input.BrandContentName;
                
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Bm_Relation_ComparisonRepository.UpdateAsync(cinfo);
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

      
        public async Task<List<Bm_Relation_Comparison>> QueryBmRelationComparisonAsync(long CompanyID,  string ServicetypeID, string ServiceBrandID,
            string BrandContentName,string ServiceProcessID,long ID)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_Relation_Comparison>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ServicetypeID.IsNotNullOrEmpty(), x => x.ServicetypeID.Equals(ServicetypeID))
             .AndIf(ServiceBrandID.IsNotNullOrEmpty(),x=> x.ServiceBrandID.Equals(ServiceBrandID))
             .AndIf(BrandContentName.IsNotNullOrEmpty(), x => x.BrandContentName.Equals(BrandContentName))
             .AndIf(ServiceProcessID.IsNotNullOrEmpty(), x => x.ServiceProcessID .Equals(ServiceProcessID))
             .AndIf(ID > 0, x => x.Id.Equals(ID));

            return _Bm_Relation_ComparisonRepository.Where(whereExpression).OrderBy(x=>x.Id).ToList();
        }


        #endregion

    }
}
