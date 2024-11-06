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
using Adnc.Fstorch.System.Application.Dtos.Setting;

namespace Adnc.Fstorch.System.Application.Service
{

    public interface ISystemCompanyInfoService : IAppService
    {

        /// <summary>
        /// 插入服务商编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入服务商编码")]
        Task<ReturnString> InsertSystemCompanyInfoAsync(SystemCompanyInfoDto input);
        /// <summary>
        /// 更新服务商编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新服务商编码")]
        Task<ReturnString> ModiSystemCompanyInfoAsync(long CompanyID, string ProviderID, SystemCompanyInfoDto input);

        [OperateLog(LogName = "查询服务商编码")]
        Task<List<SystemCompanyInfo>> QuerySystemCompanyInfoAsync(long CompanyID, string ProviderParentID, string ProviderID, string ProviderType, string ProviderStatus, string ProviderPhone);

        [OperateLog(LogName = "删除服务商编码")]
        Task<ReturnString> DeleteSystemCompanyInfoAsync(long CompanyID, string ProviderID);

        Task<string> FindLanguageAsync(string TableName, string TranslateName);



    }
}
