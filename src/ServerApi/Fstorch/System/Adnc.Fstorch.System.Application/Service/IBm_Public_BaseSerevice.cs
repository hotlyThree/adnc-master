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

    public interface IBm_Public_BaseSerevice : IAppService
    {

        /// <summary>
        /// 插入公共基础编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入公共基础编码")]
        Task<ReturnString> InsertBmPublicBaseAsync(Bm_Public_BaseDto input);
        /// <summary>
        /// 更新公共基础编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新公共基础编码")]
        Task<ReturnString> ModiBmPublicBaseAsync(long CompanyID, string ModuleID, Bm_Public_BaseDto input);

        [OperateLog(LogName = "查询公共基础编码")]
        Task<List<Bm_Public_Base>> QueryBmPublicBaseAsync(long CompanyID, string ModuleID, string ModuleName);

        [OperateLog(LogName = "删除公共基础编码")]
        Task<ReturnString> DeleteBmPublicBaseAsync(long CompanyID, string ModuleID);


        

    }
}
