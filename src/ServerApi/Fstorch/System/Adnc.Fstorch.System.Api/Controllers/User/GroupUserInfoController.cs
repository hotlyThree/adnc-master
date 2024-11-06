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
    /// 正火新系统工作组用户信息表
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class GroupUserInfoController : ControllerBase
    {
        private readonly IGroupUserInfoService _IGroupUserInfoService;
        /// <summary>
        /// 工作组用户信息表
        /// </summary>
        public GroupUserInfoController(IGroupUserInfoService iGroupUserInfoService)
        {
            _IGroupUserInfoService = iGroupUserInfoService;
        }

        #region 工作组用户信息表
        /// <summary>
        /// 增加工作组用户信息表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertGroupUserInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertGroupUserInfoAsync([FromBody] GroupUserInfoDto input)
           => await _IGroupUserInfoService.InsertGroupUserInfoAsync(input);


        /// <summary>
        /// 删除工作组用户信息表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteGroupUserInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteGroupUserInfoAsync(long CompanyID, long GroupID, long UserID,long  ID)
           => await _IGroupUserInfoService.DeleteGroupUserInfoAsync(CompanyID, GroupID,UserID,ID);

        
        /// <summary>
        /// 查询工作组用户信息表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryGroupUserInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<GroupUserInfo>>> QueryGroupUserInfoAsync(long CompanyID, long GroupID,long UserID)
           => await _IGroupUserInfoService.QueryGroupUserInfoAsync(CompanyID,GroupID,UserID);

        #endregion

    }
}
