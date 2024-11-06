using Adnc.Fstorch.System.Application.Service;
using Adnc.Fstorch.System.Repository;
using Adnc.Fstorch.System.Repository.Entities;
using Adnc.Fstorch.System.Application.Service.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyWalking.NetworkProtocol.V3;
using System.ComponentModel.Design;
using Grpc.Net.Client.Balancer;
using System.Reflection;
using Adnc.Fstorch.System.Application.Dtos.Other;
using Adnc.Fstorch.System.Application.Dtos.Group;
using System.Text.RegularExpressions;

namespace Adnc.Fstorch.System.Api.Controllers.User
{
    /// <summary>
    /// 正火新系统工作组表
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class GroupInfoController : ControllerBase
    {
        private readonly IGroupInfoService _IGroupInfoService;
        /// <summary>
        /// 工作组表
        /// </summary>
        public GroupInfoController(IGroupInfoService iGroupInfoService)
        {
            _IGroupInfoService = iGroupInfoService;
        }

        #region 工作组表
        /// <summary>
        /// 增加工作组表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertGroupInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertGroupInfoAsync([FromBody] GroupInfoDto input)
           => await _IGroupInfoService.InsertGroupInfoAsync(input);


        /// <summary>
        /// 删除工作组表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteGroupInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteGroupInfoAsync(long CompanyID, long GroupID)
           => await _IGroupInfoService.DeleteGroupInfoAsync(CompanyID, GroupID);

        /// <summary>
        /// 修改工作组表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiGroupInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiGroupInfoAsync(long CompanyID, long GroupID, [FromBody] GroupInfoDto input)
           => await _IGroupInfoService.ModiGroupInfoAsync(CompanyID, GroupID, input);


        /// <summary>
        /// 查询工作组表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryGroupInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<GroupInfo>>> QueryGroupInfoAsync(long CompanyID,string ProviderID,string GroupName,string GroupDescribe)
           => await _IGroupInfoService.QueryGroupInfoAsync(CompanyID, ProviderID,GroupName,GroupDescribe);

        #endregion

    }
}
