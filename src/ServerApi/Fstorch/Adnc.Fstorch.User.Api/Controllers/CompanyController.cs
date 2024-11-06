using Adnc.Fstorch.User.Application.Dtos.Company;
using Adnc.Infra.Redis.Caching.Core.Interceptor;
using Microsoft.AspNetCore.Mvc;

namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 公司管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.CmpRoot}")]
    public class CompanyController : AdncControllerBase
    {
        private readonly ICompanyAppService _companyAppService;

        public CompanyController(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }

        /// <summary>
        /// 创建公司
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ins")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreateAsync([FromBody]CompanyCreation input)
            => CreatedResult(await _companyAppService.CreateAsync(input));


        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromRoute]long id, [FromBody]CompanyUpdation input)
            => Result(await _companyAppService.UpdateAsync(id, input));


        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del/{creater}/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteAsync([FromRoute]long id)
            => Result(await _companyAppService.DeleteAsync(id));

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("info")]
        [AllowAnonymous]
        public async Task<ActionResult<CompanyDto>> GetInfoAsync([FromQuery] long id)
            => Result(await _companyAppService.GetInfoAsync(id));

        /// <summary>
        /// 获取当前人员所在公司列表
        /// </summary>
        /// <param name="insider"></param>
        /// <returns></returns>
        [HttpGet("list/{insider}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CompanyDto>>> GetListAsync([FromRoute]string insider)
             => Result(await _companyAppService.GetListAsync(insider));

        /// <summary>
        /// 根据名称模糊查询获取公司列表
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="cname"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetListByNameAsync([FromQuery] long aid, [FromQuery] string? cname = "")
             => await _companyAppService.GetListByNameAsync(aid, cname);



        /// <summary>
        /// 申请待处理列表
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        [HttpGet("task/{aid}")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetWaitingTask([FromRoute]long aid)
            => await _companyAppService.GetWaitingTask(aid);

        /// <summary>
        /// 申请加入公司
        /// </summary>
        /// <param name="aid">当前账号id</param>
        /// <param name="cid">申请公司id</param>
        /// <param name="uid">申请时名片id</param>
        /// <returns></returns>
        [HttpPut("apply")]
        [AllowAnonymous]
        public async Task<ActionResult> ApplyCompany([FromQuery] long aid, [FromQuery] long cid, [FromQuery] long uid)
            => Result(await _companyAppService.ApplyCompany(aid, cid, uid));

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="aid">目标账号id</param>
        /// <param name="cid">公司id</param>
        /// <param name="type">1接受  2拒绝</param>
        /// <returns></returns>
        [HttpPut("review")]
        [AllowAnonymous]
        public async Task<ActionResult> Review([FromQuery]long aid, [FromQuery]string type, [FromQuery] long cid)
            => Result(await _companyAppService.Review(aid, type, cid));

        /// <summary>
        /// 移除指定人员
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpPut("remove")]
        [AllowAnonymous]
        public async Task<ActionResult> Remove([FromQuery] long aid, [FromQuery] long cid)
            => Result(await _companyAppService.Remove(aid, cid));

        /// <summary>
        /// 查询公司内部人员列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet("account/{cid}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<AccountDto>>> GetAccountListAsync([FromRoute] long cid)
            => Result(await _companyAppService.GetAccountListAsync(cid));

        /// <summary>
        /// pdf转图片
        /// </summary>
        /// <param name="pdfFile"></param>
        /// <returns></returns>
        /*[HttpPost("convertpdf")]
        [AllowAnonymous]
        public  async Task<ActionResult<string[]>> ConvertPdfToImages([FromForm]IFormFile pdfFile)
            => Result(await _companyAppService.ConvertPdfToImages(pdfFile));*/


        
    }
}
