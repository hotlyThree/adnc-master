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
    public class Bm_DepartMentController : ControllerBase
    {
        private readonly IBm_DepartMentService _IBm_DepartMentService;
        /// <summary>
        /// 公共编码服务入口
        /// </summary>
        public Bm_DepartMentController(IBm_DepartMentService iBm_DepartMentService)
        {
            _IBm_DepartMentService = iBm_DepartMentService;
           
        }

        #region 工作部门编码表
        /// <summary>
        /// 增加工作部门编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmDepartMentAsync([FromBody] Bm_DepartMentDto input)
           => await _IBm_DepartMentService.InsertBmDepartMentAsync(input);


        /// <summary>
        /// 删除工作部门编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmDepartMentAsync(long CompanyID, string DepartMentName)
           => await _IBm_DepartMentService.DeleteBmDepartMentAsync(CompanyID, DepartMentName);

        /// <summary>
        /// 修改工作部门编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmDepartMentAsync(long CompanyID, string DepartMentName, [FromBody] Bm_DepartMentDto input)
           => await _IBm_DepartMentService.ModiBmDepartMentAsync(CompanyID, DepartMentName, input);


        /// <summary>
        /// 查询工作部门编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_DepartMent>>> QueryBmDepartMentAsync(long CompanyID, string DepartMentName, string IsValid)
           => await _IBm_DepartMentService.QueryBmDepartMentAsync(CompanyID, DepartMentName, IsValid);

        #endregion

       
    }
}
