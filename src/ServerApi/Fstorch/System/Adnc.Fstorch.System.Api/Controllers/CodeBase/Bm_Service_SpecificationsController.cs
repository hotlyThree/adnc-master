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
    /// ������ϵͳ����淶��
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Service_SpecificationsController : ControllerBase
    {
        private readonly IBm_Service_SpecificationsService _IBm_Service_SpecificationsService;
        /// <summary>
        /// ����淶�����
        /// </summary>
        public Bm_Service_SpecificationsController(IBm_Service_SpecificationsService iBm_Service_SpecificationsService)
        {
          
            _IBm_Service_SpecificationsService = iBm_Service_SpecificationsService;
        }


        #region ����淶�����
        /// <summary>
        /// ���ӷ���淶�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmServiceSpecificationsAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmServiceSpecificationsAsync([FromBody] Bm_Service_SpecificationsDto input)
           => await _IBm_Service_SpecificationsService.InsertBmServiceSpecificationsAsync(input);


        /// <summary>
        /// ɾ������淶�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmServiceSpecificationsAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmServiceSpecificationsAsync(long CompanyID, long ID)
           => await _IBm_Service_SpecificationsService.DeleteBmServiceSpecificationsAsync(CompanyID, ID);

        /// <summary>
        /// �޸ķ���淶�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmServiceSpecificationsAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmServiceSpecificationsAsync(long CompanyID, long ID, [FromBody] Bm_Service_SpecificationsDto input)
           => await _IBm_Service_SpecificationsService.ModiBmServiceSpecificationsAsync(CompanyID, ID, input);


        /// <summary>
        /// ��ѯ����淶�����
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmServiceSpecificationsAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Service_Specifications>>> QueryBmServiceSpecificationsAsync(long CompanyID, string ServiceProcessID, long ID, string ServiceSpecificationName)
           => await _IBm_Service_SpecificationsService.QueryBmServiceSpecificationsAsync(CompanyID, ServiceProcessID, ID, ServiceSpecificationName);

        #endregion
    }
}
