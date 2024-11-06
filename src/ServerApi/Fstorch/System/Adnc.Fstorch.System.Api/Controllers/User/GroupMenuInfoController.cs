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
    /// 正火新系统工作组菜单权限表
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class GroupMenuInfoController : ControllerBase
    {
        private readonly IGroupMenuInfoService _IGroupMenuInfoService;
        /// <summary>
        /// 工作组菜单权限表
        /// </summary>
        public GroupMenuInfoController(IGroupMenuInfoService iGroupMenuInfoService)
        {
            _IGroupMenuInfoService = iGroupMenuInfoService;
        }

        #region 工作组菜单权限表
        /// <summary>
        /// 增加工作组菜单权限表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertGroupMenuInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertGroupMenuInfoAsync([FromBody] GroupMenuInfoDto input)
           => await _IGroupMenuInfoService.InsertGroupMenuInfoAsync(input);


        /// <summary>
        /// 删除工作组菜单权限表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteGroupMenuInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteGroupMenuInfoAsync(long CompanyID, long GroupID,long MenuID, long ID)
           => await _IGroupMenuInfoService.DeleteGroupMenuInfoAsync(CompanyID, GroupID,MenuID,ID);

        /// <summary>
        /// 查询工作组菜单权限表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryGroupMenuInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<GroupMenuInfo>>> QueryGroupMenuInfoAsync(long CompanyID,long GroupID, long MenuID)
           => await _IGroupMenuInfoService.QueryGroupMenuInfoAsync(CompanyID, GroupID,MenuID);

        #endregion

    }
}
