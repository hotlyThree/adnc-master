using Adnc.Fstorch.System.Application.Service;
using Adnc.Fstorch.System.Repository;
using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Fstorch.System.Application.Service.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyWalking.NetworkProtocol.V3;
using Adnc.Fstorch.System.Application.Dtos.CodeBase;
using System.ComponentModel.Design;
using Grpc.Net.Client.Balancer;
using System.Reflection;

namespace Adnc.Fstorch.System.Api.Controllers.CodeBase
{
    /// <summary>
    /// 正火新系统服务流程规范对照表
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Relation_ComparisonController : ControllerBase
    {
        private readonly IBm_Relation_ComparisonService _IBm_Relation_ComparisonService;
        /// <summary>
        /// 服务流程规范对照表入口
        /// </summary>
        public Bm_Relation_ComparisonController(IBm_Relation_ComparisonService iBm_Relation_ComparisonService)
        {
            _IBm_Relation_ComparisonService = iBm_Relation_ComparisonService;
        }


        #region 服务流程规范对照表
        /// <summary>
        /// 增加服务流程规范对照表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmRelationComparisonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmRelationComparisonAsync([FromBody] Bm_Relation_ComparisonDto input)
           => await _IBm_Relation_ComparisonService.InsertBmRelationComparisonAsync(input);


        /// <summary>
        /// 删除服务流程规范对照表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmRelationComparisonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmRelationComparisonAsync(long CompanyID, long ID)
           => await _IBm_Relation_ComparisonService.DeleteBmRelationComparisonAsync(CompanyID, ID);

        /// <summary>
        /// 修改服务流程规范对照表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmRelationComparisonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmRelationComparisonAsync(long CompanyID, long ID, [FromBody] Bm_Relation_ComparisonDto input)
           => await _IBm_Relation_ComparisonService.ModiBmRelationComparisonAsync(CompanyID, ID, input);


        /// <summary>
        /// 查询服务流程规范对照表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmRelationComparisonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Relation_Comparison>>> QueryBmRelationComparisonAsync(long CompanyID, string ServicetypeID, string ServiceBrandID, string BrandContentName, string ServiceProcessID, long ID)
           => await _IBm_Relation_ComparisonService.QueryBmRelationComparisonAsync(CompanyID, ServicetypeID, ServiceBrandID, BrandContentName, ServiceProcessID, ID);

        #endregion
    }
}
