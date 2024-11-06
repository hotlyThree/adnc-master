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
    /// ������ϵͳ���˱������
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Public_PersonnelController : ControllerBase
    {
        private readonly IBm_Public_PersonnelService _IBm_Public_PersonnelService;
        /// <summary>
        /// ���˱���������
        /// </summary>
        public Bm_Public_PersonnelController(IBm_Public_PersonnelService iBm_Public_PersonnelService)
        {
            _IBm_Public_PersonnelService = iBm_Public_PersonnelService;
        }


        #region ���˱����
        /// <summary>
        /// ���Ӹ��˱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmPublicPersonnelAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmPublicPersonnelAsync([FromBody] Bm_Public_PersonnelDto input)
           => await _IBm_Public_PersonnelService.InsertBmPublicPersonnelAsync(input);


        /// <summary>
        /// ɾ�����˱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmPublicPersonnelAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmPublicPersonnelAsync(long CompanyID, long PersonnelID, string ModuleID)
           => await _IBm_Public_PersonnelService.DeleteBmPublicPersonnelAsync(CompanyID, PersonnelID, ModuleID);

        /// <summary>
        /// �޸ĸ��˱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmPublicPersonnelAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmPublicPersonnelAsync(long CompanyID, long PersonnelID, string ModuleID, [FromBody] Bm_Public_PersonnelDto input)
           => await _IBm_Public_PersonnelService.ModiBmPublicPersonnelAsync(CompanyID, PersonnelID, ModuleID, input);


        /// <summary>
        /// ��ѯ���˱����
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmPublicPersonnelAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Public_Personnel>>> QueryBmPublicPersonnelAsync(long CompanyID, long PersonnelID, string ModuleID, string ModuleName)
           => await _IBm_Public_PersonnelService.QueryBmPublicPersonnelAsync(CompanyID, PersonnelID, ModuleID, ModuleName);

        #endregion
    }
}
