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
    /// 正火新系统服务流程编码表
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Service_ProcessController : ControllerBase
    {
        private readonly IBm_Service_ProcessService _IBm_Service_ProcessService;
        /// <summary>
        /// 服务流程编码入口
        /// </summary>
        public Bm_Service_ProcessController( IBm_Service_ProcessService iBm_Service_ProcessService)
        {
            _IBm_Service_ProcessService = iBm_Service_ProcessService;
        }


        #region 服务流程编码表
        /// <summary>
        /// 增加服务流程编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmServiceProcessAsync([FromBody] Bm_Service_ProcessDto input)
           => await _IBm_Service_ProcessService.InsertBmServiceProcessAsync(input);


        /// <summary>
        /// 删除服务流程编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmServiceProcessAsync(long CompanyID, string ServiceProcessID)
           => await _IBm_Service_ProcessService.DeleteBmServiceProcessAsync(CompanyID, ServiceProcessID);

        /// <summary>
        /// 修改服务流程编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmServiceProcessAsync(long CompanyID, string ServiceProcessID, [FromBody] Bm_Service_ProcessDto input)
           => await _IBm_Service_ProcessService.ModiBmServiceProcessAsync(CompanyID, ServiceProcessID, input);


        /// <summary>
        /// 查询服务流程编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Service_Process>>> QueryBmServiceProcessAsync(long CompanyID, string ServiceProcessID)
           => await _IBm_Service_ProcessService.QueryBmServiceProcessAsync(CompanyID, ServiceProcessID);

        #endregion
    }
}
