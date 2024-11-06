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
    /// ������ϵͳ���µ�����
    /// </summary>
    [ApiController]
    [Route("System/User")]
    public class Da_Print_TemplateController : ControllerBase
    {
        private readonly IDa_StaffService _IDa_StaffService;
        /// <summary>
        /// ���µ������
        /// </summary>
        public Da_Print_TemplateController(IDa_StaffService iDa_StaffService)
        {
            _IDa_StaffService= iDa_StaffService;
        }

        #region ���µ�����
        /// <summary>
        /// �������µ�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertDaStaffAsync([FromBody] Da_StaffDto input)
           => await _IDa_StaffService.InsertDaStaffAsync(input);


        /// <summary>
        /// ɾ�����µ�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteDaStaffAsync(long CompanyID, long ID)
           => await _IDa_StaffService.DeleteDaStaffAsync(CompanyID, ID);

        /// <summary>
        /// �޸����µ�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiDaStaffAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiDaStaffAsync(long CompanyID, long ID, [FromBody] Da_StaffDto input)
           => await _IDa_StaffService.ModiDaStaffAsync(CompanyID, ID, input);


        /// <summary>
        /// ��ѯ���µ�����-����ID��ѯĳһ���˵ĵ���
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
