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
    /// 正火新系统工作职务编码表
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_PostServiceController : ControllerBase
    {
        private readonly IBm_PostService _IBm_PostService;
        
        /// <summary>
        /// 工作职务编码入口
        /// </summary>
        public Bm_PostServiceController( IBm_PostService iBm_PostService)
        {            
            _IBm_PostService = iBm_PostService;
        }


        #region 工作职务编码表
        /// <summary>
        /// 增加工作职务编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmPostAsync([FromBody] Bm_PostDto input)
           => await _IBm_PostService.InsertBmPostAsync(input);


        /// <summary>
        /// 删除工作职务编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmPostAsync(long CompanyID, string PostName)
           => await _IBm_PostService.DeleteBmPostAsync(CompanyID, PostName);

        /// <summary>
        /// 修改工作职务编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmPostAsync(long CompanyID, string PostName, [FromBody] Bm_PostDto input)
           => await _IBm_PostService.ModiBmPostAsync(CompanyID, PostName, input);


        /// <summary>
        /// 查询工作职务编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Post>>> QueryBmPostAsync(long CompanyID, string PostName, string IsValid)
           => await _IBm_PostService.QueryBmPostAsync(CompanyID, PostName, IsValid);

        #endregion
    }
}
