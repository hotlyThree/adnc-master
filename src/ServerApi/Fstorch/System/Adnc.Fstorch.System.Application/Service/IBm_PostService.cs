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

    public interface IBm_PostService : IAppService
    {

        /// <summary>
        /// 插入工作职务编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入工作职务编码")]
        Task<ReturnString> InsertBmPostAsync(Bm_PostDto input);
        /// <summary>
        /// 更新工作职务编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新工作职务编码")]
        Task<ReturnString> ModiBmPostAsync(long CompanyID, string PostName, Bm_PostDto input);

        [OperateLog(LogName = "查询工作职务编码")]
        Task<List<Bm_Post>> QueryBmPostAsync(long CompanyID, string PostName, string IsValid);

        [OperateLog(LogName = "删除工作职务编码")]
        Task<ReturnString> DeleteBmPostAsync(long CompanyID, string PostName);


        

    }
}
