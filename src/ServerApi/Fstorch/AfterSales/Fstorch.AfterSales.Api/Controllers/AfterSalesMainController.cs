
using Adnc.Demo.Shared.Const.Permissions.Fstorch;

namespace Fstorch.AfterSales.Api.Controllers
{
    /// <summary>
    /// 服务客户档案
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.AfterSalesMain}")]
    public class AfterSalesMainController : AdncControllerBase
    {
        private readonly IAfterSalesMainService _afterSalesMainService;
        public AfterSalesMainController(IAfterSalesMainService afterSalesMainService)
        {
            _afterSalesMainService = afterSalesMainService;
        }

        /// <summary>
        /// 创建客户服务档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/main")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long>> CreateAsync([FromBody]ServiceMainCreationDto input)
            => CreatedResult(await _afterSalesMainService.CreateAsync(input));


        /// <summary>
        /// 批量创建客户服务档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/main/batch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long[]>> CreateBatchAsync([FromBody]ServiceMainCreationBatchDto input)
            => CreatedResult(await _afterSalesMainService.CreateBatchAsync(input));

        /// <summary>
        /// 复制客户服务档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("copy/main/batch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long[]>> CopyServiceMainBatch([FromBody]ServiceMainCopyDto input)
            => CreatedResult(await _afterSalesMainService.CopyServiceMainBatch(input.Id, input.Num, input.OrderTaker));


        /// <summary>
        /// 更新客户服务档案信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/main")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> UpdateAsync([FromBody] ServiceMainUpdationDto input)
            => Result(await _afterSalesMainService.UpdateAsync(input));

       


        /// <summary>
        /// 更改服务客户档案状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("change/main/status")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ChangeStatus([FromBody]ServiceMainChangeStatusDto input)
            => Result(await _afterSalesMainService.ChangeStatus(input.Id, input.Status));

        /// <summary>
        /// 批量更改客户服务档案状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("change/main/status/batch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ChangeStatusBatch([FromBody]ServiceMainChangeStatusBatchDto input)
            => Result(await _afterSalesMainService.ChangeStatusBatch(input.Ids, input.Status));

        /// <summary>
        /// 增加服务客户产品明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/product")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long>> CreateServiceProductAsync([FromBody]ServiceProductCreationDto input)
            => CreatedResult(await _afterSalesMainService.CreateServiceProductAsync(input));

        /// <summary>
        /// 更新客户服务产品信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/product")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> UpdateServiceProductAsync([FromBody]ServiceProductUpdationDto input)
            => Result(await _afterSalesMainService.UpdateServiceProductAsync(input));

        /// <summary>
        /// 删除客户服务产品信息
        /// </summary>
        /// <param name="id">客户服务产品ID</param>
        /// <returns></returns>
        [HttpDelete("remove/product/{id}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> DeleteServiceProductAsync([FromRoute]long id)
            => Result(await _afterSalesMainService.DeleteServiceProductAsync(id));

        /// <summary>
        /// 获取服务客户档案分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("main/page")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<PageModelDto<ServiceMainDto>>> GetPagedAsync([FromBody] ServiceMainSearchPagedDto input)
            => await _afterSalesMainService.GetPagedAsync(input);

        /// <summary>
        /// 获取客户档案分页列表 自定义查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("main/custom/page")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<PageModelDto<ServiceMainDto>>> GetPagedCustomAsync([FromBody]ServiceMainSearchPagedCustomDto search)
            => Result(await _afterSalesMainService.GetPagedCustomAsync(search));

        /// <summary>
        /// 获取服务客户产品明细
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="mainid">服务客户档案ID</param>
        /// <returns></returns>
        [HttpGet("product/list/{mainid}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceProductDto>>> GetServiceProductAsync([FromQuery]long companyid, [FromRoute]long mainid)
            => Result(await _afterSalesMainService.GetServiceProductAsync(companyid, mainid));

        /// <summary>
        /// 获取编码是已使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("basic/isexists")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<bool>> ServiceMainIsExistsAsnyc([FromQuery] ServiceMainSearchExistsDto input)
            => Result(await _afterSalesMainService.ServiceMainIsExistsAsnyc(input));


        /// <summary>
        /// 临近客户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("main/adjacent")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceMainDto>>> AdjacentCustomer([FromQuery] ServiceMainAdjacentSearchDto input)
            => Result(await _afterSalesMainService.AdjacentCustomer(input));
    }
}
