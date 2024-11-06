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
using Adnc.Fstorch.System.Application.Dtos.Other;

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class Da_Modify_HistoryService : AbstractAppService, IDa_Modify_HistoryService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Da_Modify_History> _Da_Modify_HistoryRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Da_Modify_HistoryService(IEfRepository<Da_Modify_History> da_Modify_HistoryRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Da_Modify_HistoryRepository = da_Modify_HistoryRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteDaModifyHistoryAsync(long CompanyID,long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Da_Modify_History cinfo = await _Da_Modify_HistoryRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.Id.Equals(ID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Da_Modify_History>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ID>0, x => x.Id.Equals(ID));
                    int i = await _Da_Modify_HistoryRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertDaModifyHistoryAsync(Da_Modify_HistoryDto inputDto)
        {
            var input = Mapper.Map<Da_Modify_History>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            
            Da_Modify_History cinfo = await _Da_Modify_HistoryRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) &&
                x.InfoID.Equals(input.InfoID) &&
                x.ModifyPersonnel.Equals(input.ModifyPersonnel) &&              
                x.ModifyTime.Equals(input.ModifyTime));
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    input.Id = IdGenerater.GetNextId();
                    int irn = await _Da_Modify_HistoryRepository.InsertAsync(input);
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

       
        public async Task<List<Da_Modify_History>> QueryDaModifyHistoryAsync(long CompanyID,  string TableName, long InfoID)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Da_Modify_History>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(TableName.IsNotNullOrEmpty(), x => x.TableName.Equals(TableName))
             .AndIf(InfoID>0,x=> x.InfoID.Equals(InfoID));

            return _Da_Modify_HistoryRepository.Where(whereExpression).OrderByDescending(x=>x.ModifyTime).ToList();
        }


        #endregion

    }
}
