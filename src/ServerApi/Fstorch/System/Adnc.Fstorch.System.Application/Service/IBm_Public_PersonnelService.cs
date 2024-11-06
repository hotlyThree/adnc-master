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

    public interface IBm_Public_PersonnelService : IAppService
    {

        /// <summary>
        /// 插入个人基础编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入个人基础编码")]
        Task<ReturnString> InsertBmPublicPersonnelAsync(Bm_Public_PersonnelDto input);
        /// <summary>
        /// 更新个人基础编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新个人基础编码")]
        Task<ReturnString> ModiBmPublicPersonnelAsync(long CompanyID, long PersonnelID, string ModuleID,Bm_Public_PersonnelDto input);

        [OperateLog(LogName = "查询个人基础编码")]
        Task<List<Bm_Public_Personnel>> QueryBmPublicPersonnelAsync(long CompanyID, long PersonnelID, string ModuleID, string ModuleName);

        [OperateLog(LogName = "删除个人基础编码")]
        Task<ReturnString> DeleteBmPublicPersonnelAsync(long CompanyID,long PersonnelID, string ModuleID);


        

    }
}
