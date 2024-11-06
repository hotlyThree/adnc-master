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
    /// ������ϵͳ�����������
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_DepartMentController : ControllerBase
    {
        private readonly IBm_DepartMentService _IBm_DepartMentService;
        /// <summary>
        /// ��������������
        /// </summary>
        public Bm_DepartMentController(IBm_DepartMentService iBm_DepartMentService)
        {
            _IBm_DepartMentService = iBm_DepartMentService;
           
        }

        #region �������ű����
        /// <summary>
        /// ���ӹ������ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmDepartMentAsync([FromBody] Bm_DepartMentDto input)
           => await _IBm_DepartMentService.InsertBmDepartMentAsync(input);


        /// <summary>
        /// ɾ���������ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmDepartMentAsync(long CompanyID, string DepartMentName)
           => await _IBm_DepartMentService.DeleteBmDepartMentAsync(CompanyID, DepartMentName);

        /// <summary>
        /// �޸Ĺ������ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmDepartMentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmDepartMentAsync(long CompanyID, string DepartMentName, [FromBody] Bm_DepartMentDto input)
           => await _IBm_DepartMentService.ModiBmDepartMentAsync(CompanyID, DepartMentName, input);


        /// <summary>
        /// ��ѯ�������ű����
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
