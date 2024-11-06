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
    /// ������ϵͳ���ű����
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_Sms_TemplateController : ControllerBase
    {
        private readonly IBm_Sms_TemplateService _IBm_Sms_TemplateService;
        /// <summary>
        /// ���ű���������
        /// </summary>
        public Bm_Sms_TemplateController(IBm_Sms_TemplateService iBm_Sms_TemplateService)
        {
         
            _IBm_Sms_TemplateService = iBm_Sms_TemplateService;
        }


        #region ���ű����
        /// <summary>
        /// ���Ӷ��ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmSmsTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmSmsTemplateAsync([FromBody] Bm_Sms_TemplateDto input)
           => await _IBm_Sms_TemplateService.InsertBmSmsTemplateAsync(input);


        /// <summary>
        /// ɾ�����ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmSmsTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmSmsTemplateAsync(long CompanyID, string SmsTemplateID)
           => await _IBm_Sms_TemplateService.DeleteBmSmsTemplateAsync(CompanyID, SmsTemplateID);

        /// <summary>
        /// �޸Ķ��ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmSmsTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmSmsTemplateAsync(long CompanyID, string SmsTemplateID, [FromBody] Bm_Sms_TemplateDto input)
           => await _IBm_Sms_TemplateService.ModiBmSmsTemplateAsync(CompanyID, SmsTemplateID, input);


        /// <summary>
        /// ��ѯ���ű����
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmSmsTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Sms_Template>>> QueryBmSmsTemplateAsync(long CompanyID, string SmsTemplateID, string SmsTemplatetype, string SmsTemplateName)
           => await _IBm_Sms_TemplateService.QueryBmSmsTemplateAsync(CompanyID, SmsTemplateID, SmsTemplatetype, SmsTemplateName);

        #endregion
    }
}
