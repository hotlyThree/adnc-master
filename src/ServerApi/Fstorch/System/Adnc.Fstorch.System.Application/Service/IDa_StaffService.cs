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

    public interface IDa_StaffService : IAppService
    {

        /// <summary>
        /// 插入人事档案编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入人事档案编码")]
        Task<ReturnString> InsertDaStaffAsync(Da_StaffDto input);
        /// <summary>
        /// 更新人事档案编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新人事档案编码")]
        Task<ReturnString> ModiDaStaffAsync(long CompanyID, long ID, Da_StaffDto input);

        [OperateLog(LogName = "查询人事档案编码")]
        Task<List<Da_Staff>> QueryDaStaffIDAsync(long CompanyID, long ID);

        [OperateLog(LogName = "删除人事档案编码")]
        Task<ReturnString> DeleteDaStaffAsync(long CompanyID, long ID);


        

    }
}
