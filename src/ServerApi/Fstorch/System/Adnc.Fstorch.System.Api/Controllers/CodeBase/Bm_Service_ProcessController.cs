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
    /// ������ϵͳ�������̱����
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Service_ProcessController : ControllerBase
    {
        private readonly IBm_Service_ProcessService _IBm_Service_ProcessService;
        /// <summary>
        /// �������̱������
        /// </summary>
        public Bm_Service_ProcessController( IBm_Service_ProcessService iBm_Service_ProcessService)
        {
            _IBm_Service_ProcessService = iBm_Service_ProcessService;
        }


        #region �������̱����
        /// <summary>
        /// ���ӷ������̱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmServiceProcessAsync([FromBody] Bm_Service_ProcessDto input)
           => await _IBm_Service_ProcessService.InsertBmServiceProcessAsync(input);


        /// <summary>
        /// ɾ���������̱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmServiceProcessAsync(long CompanyID, string ServiceProcessID)
           => await _IBm_Service_ProcessService.DeleteBmServiceProcessAsync(CompanyID, ServiceProcessID);

        /// <summary>
        /// �޸ķ������̱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmServiceProcessAsync(long CompanyID, string ServiceProcessID, [FromBody] Bm_Service_ProcessDto input)
           => await _IBm_Service_ProcessService.ModiBmServiceProcessAsync(CompanyID, ServiceProcessID, input);


        /// <summary>
        /// ��ѯ�������̱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmServiceProcessAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Service_Process>>> QueryBmServiceProcessAsync(long CompanyID, string ServiceProcessID)
           => await _IBm_Service_ProcessService.QueryBmServiceProcessAsync(CompanyID, ServiceProcessID);

        #endregion
    }
}
