using Adnc.Shared.Application.Contracts.Attributes;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Fstorch.System.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adnc.Fstorch.System.Repository;
using Adnc.Shared.Application.Contracts.ResultModels;
using Adnc.Fstorch.System.Application.Dtos.CodeBase;

namespace Adnc.Fstorch.System.Application.Service
{

    public interface IBm_Sms_TemplateService : IAppService
    {

        /// <summary>
        /// 插入短信模板编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入短信模板编码")]
        Task<ReturnString> InsertBmSmsTemplateAsync(Bm_Sms_TemplateDto input);
        /// <summary>
        /// 更新短信模板编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新短信模板编码")]
        Task<ReturnString> ModiBmSmsTemplateAsync(long CompanyID, string SmsTemplateID, Bm_Sms_TemplateDto input);

        [OperateLog(LogName = "查询短信模板编码")]
        Task<List<Bm_Sms_Template>> QueryBmSmsTemplateAsync(long CompanyID, string SmsTemplateID, string SmsTemplatetype, string SmsTemplateName);

        [OperateLog(LogName = "删除短信模板编码")]
        Task<ReturnString> DeleteBmSmsTemplateAsync(long CompanyID, string SmsTemplateID);


        

    }
}
