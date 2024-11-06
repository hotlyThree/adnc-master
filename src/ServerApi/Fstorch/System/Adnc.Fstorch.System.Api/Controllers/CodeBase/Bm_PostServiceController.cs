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
    /// ������ϵͳ����ְ������
    /// </summary>
    [ApiController]
    [Route("System/CodeBase")]
    public class Bm_PostServiceController : ControllerBase
    {
        private readonly IBm_PostService _IBm_PostService;
        
        /// <summary>
        /// ����ְ��������
        /// </summary>
        public Bm_PostServiceController( IBm_PostService iBm_PostService)
        {            
            _IBm_PostService = iBm_PostService;
        }


        #region ����ְ������
        /// <summary>
        /// ���ӹ���ְ������
        /// </summary>
        /// <returns></returns>
        [HttpPost("InsertBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> InsertBmPostAsync([FromBody] Bm_PostDto input)
           => await _IBm_PostService.InsertBmPostAsync(input);


        /// <summary>
        /// ɾ������ְ������
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> DeleteBmPostAsync(long CompanyID, string PostName)
           => await _IBm_PostService.DeleteBmPostAsync(CompanyID, PostName);

        /// <summary>
        /// �޸Ĺ���ְ������
        /// </summary>
        /// <returns></returns>
        [HttpPost("ModiBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ReturnString>> ModiBmPostAsync(long CompanyID, string PostName, [FromBody] Bm_PostDto input)
           => await _IBm_PostService.ModiBmPostAsync(CompanyID, PostName, input);


        /// <summary>
        /// ��ѯ����ְ������
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueryBmPostAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Bm_Post>>> QueryBmPostAsync(long CompanyID, string PostName, string IsValid)
           => await _IBm_PostService.QueryBmPostAsync(CompanyID, PostName, IsValid);

        #endregion
    }
}
