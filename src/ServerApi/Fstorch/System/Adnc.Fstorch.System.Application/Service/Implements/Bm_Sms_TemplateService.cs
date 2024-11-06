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

namespace Adnc.Fstorch.System.Application.Service.Implements
{
    public class Bm_Sms_TemplateService : AbstractAppService, IBm_Sms_TemplateService
    {
        /// <summary>
        /// 初始化数据 MsgCode=BM_GROUP_INSERT_1成功,_0失败，_2找不到对象 _3条件不满足MsgValue=1成功，0失败
        /// </summary>
        private readonly IEfRepository<Bm_Sms_Template> _Bm_Sms_TemplateRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public Bm_Sms_TemplateService(IEfRepository<Bm_Sms_Template> bm_Sms_TemplateRepository,
            CacheService cacheService, IUnitOfWork unitOfWork)
        {
            _Bm_Sms_TemplateRepository = bm_Sms_TemplateRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            
        }

        #region 工作组信息
        public async Task<ReturnString> DeleteBmSmsTemplateAsync(long CompanyID,string SmsTemplateID)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Sms_Template cinfo = await _Bm_Sms_TemplateRepository.FindAsync(x =>x.CompanyID.Equals(CompanyID) && x.SmsTemplateID.Equals(SmsTemplateID));
            if (cinfo != null)          
            {                
                //首先检测当前工作部门编码是否已使用,已使用则不能删除
                //
                _unitOfWork.BeginTransaction();
                try
                {
                    var whereExpression = ExpressionCreator
                     .New<Bm_Sms_Template>()
                     .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
                     .AndIf(SmsTemplateID.IsNotNullOrEmpty(), x => x.SmsTemplateID.Equals(SmsTemplateID));
                    int i = await _Bm_Sms_TemplateRepository.DeleteRangeAsync(whereExpression);
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

        public async Task<ReturnString> InsertBmSmsTemplateAsync(Bm_Sms_TemplateDto inputDto)
        {
            var input = Mapper.Map<Bm_Sms_Template>(inputDto);
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;

            Bm_Sms_Template cinfo = await _Bm_Sms_TemplateRepository.FindAsync(x => x.CompanyID.Equals(input.CompanyID) && 
             x.SmsTemplateID.Equals(input.SmsTemplateID) );
            if (cinfo == null)
            {
                //查找当前编码相同的数据，找不到则新增
                _unitOfWork.BeginTransaction();
                try
                {
                    int irn = await _Bm_Sms_TemplateRepository.InsertAsync(input);
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

        public async Task<ReturnString> ModiBmSmsTemplateAsync(long CompanyID, string SmsTemplateID, Bm_Sms_TemplateDto input)
        {
            ReturnString rn = new ReturnString();
            rn.MsgCode = MethodBase.GetCurrentMethod().Name;
            Bm_Sms_Template cinfo = await _Bm_Sms_TemplateRepository.FindAsync(x => x.CompanyID.Equals(CompanyID) &&
                x.SmsTemplateID.Equals(SmsTemplateID), noTracking: false);
            if (cinfo != null)
            {
                //复制
                cinfo.SmsTemplateMemo = input.SmsTemplateMemo;           
                cinfo.SmsTemplatetype=  input.SmsTemplatetype;
                cinfo.SmsTemplateName= input.SmsTemplateName;
                cinfo.Displaynumber = input.Displaynumber;
                _unitOfWork.BeginTransaction();
                try
                {
                    int i = await _Bm_Sms_TemplateRepository.UpdateAsync(cinfo);
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

      
        public async Task<List<Bm_Sms_Template>> QueryBmSmsTemplateAsync(long CompanyID,  string SmsTemplateID, string SmsTemplatetype,string SmsTemplateName)
        {
            //根据条件组合查询条件
            var whereExpression = ExpressionCreator
             .New<Bm_Sms_Template>()
             .AndIf(CompanyID > 0, x => x.CompanyID.Equals(CompanyID))
             .AndIf(SmsTemplateID.IsNotNullOrEmpty(), x => x.SmsTemplateID.Equals(SmsTemplateID))
             .AndIf(SmsTemplateName.IsNotNullOrEmpty(), x => x.SmsTemplateName.Equals(SmsTemplateName))
             .AndIf(SmsTemplatetype.IsNotNullOrEmpty(),x=> x.SmsTemplatetype.Equals(SmsTemplatetype));

            return _Bm_Sms_TemplateRepository.Where(whereExpression).OrderBy(x=>x.Displaynumber).ToList();
        }


        #endregion

    }
}
