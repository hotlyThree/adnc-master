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
using Adnc.Fstorch.System.Application.Dtos.Setting;
using Z.BulkOperations;

namespace Adnc.Fstorch.System.Api.Controllers.Other
{
    /// <summary>
    /// ������ϵͳ�����̼ұ���
    /// </summary>
    [ApiController]
    [Route("System/Other")]
    public class SystemCompanyInfoController : ControllerBase
    {
        private readonly ISystemCompanyInfoService _ISystemCompanyInfoService;
        /// <summary>
        /// �����̼ұ���
        /// </summary>
        public SystemCompanyInfoController(ISystemCompanyInfoService iSystemCompanyInfoService)
        {
            _ISystemCompanyInfoService = iSystemCompanyInfoService;
        }

        #region �����̼ұ���
        /// <summary>
        /// ���ӷ����̼ұ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertSystemCompanyInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertSystemCompanyInfoAsync([FromBody] SystemCompanyInfoDto input)
           => await _ISystemCompanyInfoService.InsertSystemCompanyInfoAsync(input);


        /// <summary>
        /// ɾ�������̼ұ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteSystemCompanyInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteSystemCompanyInfoAsync(long CompanyID, string ProviderID)
           => await _ISystemCompanyInfoService.DeleteSystemCompanyInfoAsync(CompanyID,ProviderID);

        /// <summary>
        /// �޸ķ����̼ұ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiSystemCompanyInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiSystemCompanyInfoAsync(long CompanyID, string ProviderID, [FromBody] SystemCompanyInfoDto input)
           => await _ISystemCompanyInfoService.ModiSystemCompanyInfoAsync(CompanyID, ProviderID, input);


        /// <summary>
        /// ��ѯ�����̼ұ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("QuerySystemCompanyInfoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<SystemCompanyInfo>>> QuerySystemCompanyInfoAsync(long CompanyID,string ProviderParentID, string ProviderID,
            string ProviderType, string ProviderStatus, string ProviderPhone)
           => await _ISystemCompanyInfoService.QuerySystemCompanyInfoAsync(CompanyID,ProviderParentID,ProviderID,ProviderType,ProviderStatus,ProviderPhone);



        /// <summary>
        /// ��ȡAPI�ӿڵĴ������
        /// </summary>
        /// <returns></returns>
        [HttpPost("FindLanguageAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> FindLanguageAsync(string TableName, string TranslateName)
           => await _ISystemCompanyInfoService.FindLanguageAsync(TableName,TranslateName);

        #endregion

    }
}
