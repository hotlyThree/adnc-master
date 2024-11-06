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
    /// 正火新系统微信编码服务
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_WX_TemplateController : ControllerBase
    {
        private readonly IBm_WX_TemplateService _IBm_WX_TemplateService;
        /// <summary>
        /// 微信编码服务入口
        /// </summary>
        public Bm_WX_TemplateController(IBm_DepartMentService iBm_DepartMentService, IBm_PostService iBm_PostService,
            IBm_Public_BaseSerevice iBm_Public_BaseSerevice, IBm_Public_PersonnelService iBm_Public_PersonnelService,
            IBm_Relation_ComparisonService iBm_Relation_ComparisonService, IBm_Service_ProcessService iBm_Service_ProcessService,
            IBm_Service_SpecificationsService iBm_Service_SpecificationsService, IBm_Sms_TemplateService iBm_Sms_TemplateService,
            IBm_WX_TemplateService iBm_WX_TemplateService)
        {
          
            _IBm_WX_TemplateService = iBm_WX_TemplateService;
        }


        #region 微信编码表
        /// <summary>
        /// 增加微信编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmWXTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmWXTemplateAsync([FromBody] Bm_WX_TemplateDto input)
           => await _IBm_WX_TemplateService.InsertBmWXTemplateAsync(input);


        /// <summary>
        /// 删除微信编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmWXTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmWXTemplateAsync(long CompanyID, string SmsTemplateID)
           => await _IBm_WX_TemplateService.DeleteBmWXTemplateAsync(CompanyID, SmsTemplateID);

        /// <summary>
        /// 修改微信编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmWXTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmWXTemplateAsync(long CompanyID, string SmsTemplateID, [FromBody] Bm_WX_TemplateDto input)
           => await _IBm_WX_TemplateService.ModiBmWXTemplateAsync(CompanyID, SmsTemplateID, input);


        /// <summary>
        /// 查询微信编码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmWXTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_WX_Template>>> QueryBmWXTemplateAsync(long CompanyID, string WxTemplateID, string WxTemplatetype, string WxTemplateName)
           => await _IBm_WX_TemplateService.QueryBmWXTemplateAsync(CompanyID, WxTemplateID, WxTemplatetype, WxTemplateName);

        #endregion
    }
}
