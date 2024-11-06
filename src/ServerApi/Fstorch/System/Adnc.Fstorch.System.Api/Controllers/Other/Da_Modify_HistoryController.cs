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
using static Prometheus.DotNetRuntime.EventListening.Parsers.ContentionEventParser.Events;

namespace Adnc.Fstorch.System.Api.Controllers.Other
{
    /// <summary>
    /// ������ϵͳ�޸���ʷ
    /// </summary>
    [ApiController]
    [Route("System/Other")]
    public class Da_Modify_HistoryController : ControllerBase
    {
        private readonly IDa_Modify_HistoryService _IDa_Modify_HistoryService;
        /// <summary>
        /// �޸���ʷ
        /// </summary>
        public Da_Modify_HistoryController(IDa_Modify_HistoryService iDa_Modify_HistoryService)
        {
            _IDa_Modify_HistoryService = iDa_Modify_HistoryService;
        }

        #region �޸���ʷ
        /// <summary>
        /// �����޸���ʷ
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertDaModifyHistoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertDaModifyHistoryAsync([FromBody] Da_Modify_HistoryDto input)
           => await _IDa_Modify_HistoryService.InsertDaModifyHistoryAsync(input);


        /// <summary>
        /// ɾ���޸���ʷ
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteDaModifyHistoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteDaModifyHistoryAsync(long CompanyID, long ID)
           => await _IDa_Modify_HistoryService.DeleteDaModifyHistoryAsync(CompanyID, ID);

        /// <summary>
        /// ��ѯ�޸���ʷ
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryDaModifyHistoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Da_Modify_History>>> QueryDaModifyHistoryAsync(long CompanyID,string TableName,long InfoID)
           => await _IDa_Modify_HistoryService.QueryDaModifyHistoryAsync(CompanyID,TableName,InfoID);

        #endregion

    }
}
