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

    public interface IBm_WX_TemplateService : IAppService
    {

        /// <summary>
        /// 插入微信模板编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入微信模板编码")]
        Task<ReturnString> InsertBmWXTemplateAsync(Bm_WX_TemplateDto input);
        /// <summary>
        /// 更新微信模板编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新微信模板编码")]
        Task<ReturnString> ModiBmWXTemplateAsync(long CompanyID, string SmsTemplateID, Bm_WX_TemplateDto input);

        [OperateLog(LogName = "查询微信模板编码")]
        Task<List<Bm_WX_Template>> QueryBmWXTemplateAsync(long CompanyID, string WxTemplateID, string WxTemplatetype, string WxTemplateName);

        [OperateLog(LogName = "删除微信模板编码")]
        Task<ReturnString> DeleteBmWXTemplateAsync(long CompanyID, string SmsTemplateID);


        

    }
}
