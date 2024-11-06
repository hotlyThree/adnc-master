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
    /// 正火新系统公共编码服务
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Public_BaseController : ControllerBase
    {
        private readonly IBm_Public_BaseSerevice _IBm_Public_BaseSerevice;
        /// <summary>
        /// 公共编码服务入口
        /// </summary>
        public Bm_Public_BaseController(IBm_Public_BaseSerevice iBm_Public_BaseSerevice)
        {
            _IBm_Public_BaseSerevice = iBm_Public_BaseSerevice;
        }


        #region 公共编码表
        /// <summary>
        /// 增加公共编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmPublicBaseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmPublicBaseAsync([FromBody] Bm_Public_BaseDto input)
           => await _IBm_Public_BaseSerevice.InsertBmPublicBaseAsync(input);


        /// <summary>
        /// 删除公共编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmPublicBaseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmPublicBaseAsync(long CompanyID, string ModuleID)
           => await _IBm_Public_BaseSerevice.DeleteBmPublicBaseAsync(CompanyID, ModuleID);

        /// <summary>
        /// 修改公共编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmPublicBaseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmPublicBaseAsync(long CompanyID, string ModuleID, [FromBody] Bm_Public_BaseDto input)
           => await _IBm_Public_BaseSerevice.ModiBmPublicBaseAsync(CompanyID, ModuleID, input);


        /// <summary>
        /// 查询公共编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmPublicBaseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Public_Base>>> QueryBmPublicBaseAsync(long CompanyID, string ModuleID, string ModuleName)
           => await _IBm_Public_BaseSerevice.QueryBmPublicBaseAsync(CompanyID, ModuleID, ModuleName);

        #endregion

    }
}
