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

namespace Adnc.Fstorch.System.Api.Controllers.User
{
    /// <summary>
    /// 正火新系统人事档案表
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class Da_Print_TemplateController : ControllerBase
    {
        private readonly IDa_StaffService _IDa_StaffService;
        /// <summary>
        /// 人事档案入库
        /// </summary>
        public Da_Print_TemplateController(IDa_StaffService iDa_StaffService)
        {
            _IDa_StaffService= iDa_StaffService;
        }

        #region 人事档案表
        /// <summary>
        /// 增加人事档案表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertDaStaffAsync([FromBody] Da_StaffDto input)
           => await _IDa_StaffService.InsertDaStaffAsync(input);


        /// <summary>
        /// 删除人事档案表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteDaStaffAsync(long CompanyID, long ID)
           => await _IDa_StaffService.DeleteDaStaffAsync(CompanyID, ID);

        /// <summary>
        /// 修改人事档案表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiDaStaffAsync(long CompanyID, long ID, [FromBody] Da_StaffDto input)
           => await _IDa_StaffService.ModiDaStaffAsync(CompanyID, ID, input);


        /// <summary>
        /// 查询人事档案表-根据ID查询某一个人的档案
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryDaStaffIDAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Da_Staff>>> QueryDaStaffIDAsync(long CompanyID,long ID)
           => await _IDa_StaffService.QueryDaStaffIDAsync(CompanyID, ID);

        #endregion

    }
}
