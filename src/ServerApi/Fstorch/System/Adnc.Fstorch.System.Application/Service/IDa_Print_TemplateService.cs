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
using Adnc.Fstorch.System.Application.Dtos.Other;

namespace Adnc.Fstorch.System.Application.Service
{

    public interface IDa_Print_TemplateService : IAppService
    {

        /// <summary>
        /// 插入打印模板使用编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入打印模板使用编码")]
        Task<ReturnString> InsertDaPrintTemplateAsync(Da_Print_TemplateDto input);
        /// <summary>
        /// 更新打印模板使用编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新打印模板使用编码")]
        Task<ReturnString> ModiDaPrintTemplateAsync(long CompanyID, long ID, Da_Print_TemplateDto input);

        [OperateLog(LogName = "查询打印模板使用编码")]
        Task<List<Da_Print_Template>> QueryDaPrintTemplateAsync(long CompanyID, string ModuleID);

        [OperateLog(LogName = "删除打印模板使用编码")]
        Task<ReturnString> DeleteDaPrintTemplateAsync(long CompanyID, long ID);


        

    }
}
