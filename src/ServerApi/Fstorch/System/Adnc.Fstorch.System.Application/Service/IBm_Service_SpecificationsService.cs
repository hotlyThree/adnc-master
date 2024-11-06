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

    public interface IBm_Service_SpecificationsService : IAppService
    {

        /// <summary>
        /// 插入服务规范编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入服务规范编码")]
        Task<ReturnString> InsertBmServiceSpecificationsAsync(Bm_Service_SpecificationsDto input);
        /// <summary>
        /// 更新服务规范编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新服务规范编码")]
        Task<ReturnString> ModiBmServiceSpecificationsAsync(long CompanyID, long ID,Bm_Service_SpecificationsDto input);

        [OperateLog(LogName = "查询服务规范编码")]
        Task<List<Bm_Service_Specifications>> QueryBmServiceSpecificationsAsync(long CompanyID, string ServiceProcessID, long ID, string ServiceSpecificationName);

        [OperateLog(LogName = "删除服务规范编码")]
        Task<ReturnString> DeleteBmServiceSpecificationsAsync(long CompanyID, long ID);


        

    }
}
