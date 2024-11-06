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

namespace Adnc.Fstorch.System.Api.Controllers.Other
{
    /// <summary>
    /// 正火新系统打印方案设置
    /// </summary>
    [ApiController]
    [Route("System/Other")]
    public class Da_Print_TemplateController : ControllerBase
    {
        private readonly IDa_Print_TemplateService _IDa_Print_TemplateService;
        /// <summary>
        /// 打印方案设置
        /// </summary>
        public Da_Print_TemplateController(IDa_Print_TemplateService iDa_Print_TemplateService)
        {
            _IDa_Print_TemplateService = iDa_Print_TemplateService;
        }

        #region 打印方案设置
        /// <summary>
        /// 增加打印方案配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertDaPrintTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertDaPrintTemplateAsync([FromBody] Da_Print_TemplateDto input)
           => await _IDa_Print_TemplateService.InsertDaPrintTemplateAsync(input);


        /// <summary>
        /// 删除打印方案配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteDaPrintTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteDaPrintTemplateAsync(long CompanyID, long ID)
           => await _IDa_Print_TemplateService.DeleteDaPrintTemplateAsync(CompanyID, ID);

        /// <summary>
        /// 修改打印方案配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiDaPrintTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiDaPrintTemplateAsync(long CompanyID, long ID, [FromBody] Da_Print_TemplateDto input)
           => await _IDa_Print_TemplateService.ModiDaPrintTemplateAsync(CompanyID, ID, input);


        /// <summary>
        /// 查询打印方案配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryDaPrintTemplateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Da_Print_Template>>> QueryDaPrintTemplateAsync(long CompanyID,string ModuleID)
           => await _IDa_Print_TemplateService.QueryDaPrintTemplateAsync(CompanyID,ModuleID);

        #endregion

    }
}
