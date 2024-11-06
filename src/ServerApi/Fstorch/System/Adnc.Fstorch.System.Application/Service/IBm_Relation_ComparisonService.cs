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

    public interface IBm_Relation_ComparisonService : IAppService
    {

        /// <summary>
        /// 插入关系对照表编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "插入关系对照表编码")]
        Task<ReturnString> InsertBmRelationComparisonAsync(Bm_Relation_ComparisonDto input);
        /// <summary>
        /// 更新关系对照表编码
        /// </summary>
        /// <returns></returns>
        [OperateLog(LogName = "更新关系对照表编码")]
        Task<ReturnString> ModiBmRelationComparisonAsync(long CompanyID, long ID, Bm_Relation_ComparisonDto input);

        [OperateLog(LogName = "查询关系对照表编码")]
        Task<List<Bm_Relation_Comparison>> QueryBmRelationComparisonAsync(long CompanyID, string ServicetypeID, string ServiceBrandID,
            string BrandContentName, string ServiceProcessID, long ID);

        [OperateLog(LogName = "删除关系对照表编码")]
        Task<ReturnString> DeleteBmRelationComparisonAsync(long CompanyID, long ID);


        

    }
}
