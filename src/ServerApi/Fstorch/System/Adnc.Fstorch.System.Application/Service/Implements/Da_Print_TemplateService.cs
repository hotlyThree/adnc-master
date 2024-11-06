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
    public class Da_Print_TemplateService : AbstractAppService, IDa_Print_TemplateService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Da_Print_Template> _Da_Print_TemplateRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Da_Print_TemplateService(IEfRepository<Da_Print_Template> da_Print_TemplateRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Da_Print_TemplateRepository = da_Print_TemplateRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteDaPrintTemplateAsync(long CompanyID,long ID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Da_Print_Template cinfo = await _Da_Print_TemplateRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.Id.Equals(ID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Da_Print_Template>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(ID>0, x => x.Id.Equals(ID));
                    int i = await _Da_Print_TemplateRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertDaPrintTemplateAsync(Da_Print_TemplateDto inputDto)
        {
            var input = Mapper.Map<Da_Print_Template>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            
            Da_Print_Template cinfo = await _Da_Print_TemplateRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.ModuleID.Equals(input.ModuleID) );
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    input.Id = IdGenerater.GetNextId();
                    int irn = await _Da_Print_TemplateRepository.InsertAsync(input);
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

        public async Task<ReturnString> ModiDaPrintTemplateAsync(long CompanyID, long ID, Da_Print_TemplateDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Da_Print_Template cinfo = await _Da_Print_TemplateRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.Id.Equals(ID), noTracking: false);
            if (cinfo != null)
            {
                if (cinfo.ModuleID != input.ModuleID)
                {
                    //检测工作组名称相同企业是否重复
                    int n= await _Da_Print_TemplateRepository.CountAsync(x => x.CompanyID.Equals(CompanyID)  && x.ModuleID.Equals(input.ModuleID) && x.Id.Equals(ID)==false ) ;
                    if (n > 0)
                    {
                        //存在重复名称,不能更新
                        rn.MsgValue = "3";
                        return rn;
                    }
                    cinfo.ModuleID=input.ModuleID;
                }
                //复制
                cinfo.TemplateCondition = input.TemplateCondition;                
                cinfo.TemplateID= input.TemplateID;
                cinfo.TemplateName= input.TemplateName;
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Da_Print_TemplateRepository.UpdateAsync(cinfo);
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

      
        public async Task<List<Da_Print_Template>> QueryDaPrintTemplateAsync(long CompanyID,  string ModuleID)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Da_Print_Template>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(ModuleID.IsNotNullOrEmpty(), x => x.ModuleID.Equals(ModuleID));

            return _Da_Print_TemplateRepository.Where(whereExpression).OrderBy(x=>x.ModuleID).ToList();
        }


        #endregion

    }
}
