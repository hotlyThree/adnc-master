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
    /// ������ϵͳ������ֿ�Ȩ�ޱ�
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class GroupWareHouseInfoController : ControllerBase
    {
        private readonly IGroupWareHouseInfoService _IGroupWareHouseInfoService;
        /// <summary>
        /// �ֿ�Ȩ�ޱ�
        /// </summary>
        public GroupWareHouseInfoController(IGroupWareHouseInfoService iGroupWareHouseInfoService)
        {
            _IGroupWareHouseInfoService = iGroupWareHouseInfoService;
        }

        #region �ֿ�Ȩ�ޱ�
        /// <summary>
        /// ���ӹ�����ֿ�Ȩ�ޱ�
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertGroupWareHouseInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertGroupWareHouseInfoAsync([FromBody] GroupWareHouseInfoDto input)
           => await _IGroupWareHouseInfoService.InsertGroupWareHouseInfoAsync(input);


        /// <summary>
        /// ɾ��������ֿ�Ȩ�ޱ�
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteGroupWareHouseInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteGroupWareHouseInfoAsync(long CompanyID, long GroupID,long UserID,long ID)
           => await _IGroupWareHouseInfoService.DeleteGroupWareHouseInfoAsync(CompanyID, GroupID,UserID,ID);


        /// <summary>
        /// ��ѯ������ֿ�Ȩ�ޱ�
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryGroupWareHouseInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<GroupWareHouseInfo>>> QueryGroupWareHouseInfoAsync(long CompanyID,long GroupID,long UserID)
           => await _IGroupWareHouseInfoService.QueryGroupWareHouseInfoAsync(CompanyID,GroupID,UserID);

        #endregion

    }
}
