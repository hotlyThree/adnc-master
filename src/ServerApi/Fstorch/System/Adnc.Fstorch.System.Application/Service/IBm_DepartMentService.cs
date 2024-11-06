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

    public interface IBm_DepartMentService : IAppService
    {

        /// <summary>
        /// 插入工作部门编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入工作部门编码")]
        Task<ReturnString> InsertBmDepartMentAsync(Bm_DepartMentDto input);
        /// <summary>
        /// 更新工作部门编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新工作部门编码")]
        Task<ReturnString> ModiBmDepartMentAsync(long CompanyID, string DepartMentName, Bm_DepartMentDto input);

        [OperateLog(LogName = "查询工作部门编码")]
        Task<List<Bm_DepartMent>> QueryBmDepartMentAsync(long CompanyID,string DepartMentName,string IsValid);

        [OperateLog(LogName = "删除工作部门编码")]
        Task<ReturnString> DeleteBmDepartMentAsync(long CompanyID, string DepartMentName);


        

    }
}
