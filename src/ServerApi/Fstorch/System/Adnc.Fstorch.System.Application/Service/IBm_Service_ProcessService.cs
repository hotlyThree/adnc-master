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

    public interface IBm_Service_ProcessService : IAppService
    {

        /// <summary>
        /// 插入服务流程编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入服务流程编码")]
        Task<ReturnString> InsertBmServiceProcessAsync(Bm_Service_ProcessDto input);
        /// <summary>
        /// 更新服务流程编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新服务流程编码")]
        Task<ReturnString> ModiBmServiceProcessAsync(long CompanyID, string ServiceProcessID, Bm_Service_ProcessDto input);

        [OperateLog(LogName = "查询服务流程编码")]
        Task<List<Bm_Service_Process>> QueryBmServiceProcessAsync(long CompanyID, string ServiceProcessID);

        [OperateLog(LogName = "删除服务流程编码")]
        Task<ReturnString> DeleteBmServiceProcessAsync(long CompanyID, string ServiceProcessID);


        

    }
}
