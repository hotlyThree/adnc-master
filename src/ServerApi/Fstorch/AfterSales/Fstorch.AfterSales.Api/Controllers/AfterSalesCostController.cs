
using Adnc.Demo.Shared.Const.Permissions.Fstorch;

namespace Fstorch.AfterSales.Api.Controllers
{
    /// <summary>
    /// 费用管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.AfterSalesCost}")]
    public class AfterSalesCostController : AdncControllerBase
    {
        private readonly IAfterSalesCostService _afterSalesCostService;
        public AfterSalesCostController(IAfterSalesCostService afterSalesCostService)
        {
            _afterSalesCostService = afterSalesCostService;
        }

        /// <summary>
        /// 更新应付信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/cope")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> SetCopeInfo([FromBody]ServiceOrderCopeUpdationDto input)
            => Result(await _afterSalesCostService.SetCopeInfo(input));


        /// <summary>
        /// 获取应付信息
        /// </summary>
        /// <param name="serviceInfoId">客户ID</param>
        /// <param name="serviceOrderId">工单号</param>
        /// <returns></returns>
        [HttpGet("get/cope/{serviceInfoId}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<ServiceOrderCopeDto>> GetCopeInfo([FromRoute]long serviceInfoId, [FromQuery]string serviceOrderId)
            => Result(await _afterSalesCostService.GetCopeInfo(serviceInfoId, serviceOrderId));

        /// <summary>
        /// 更新应收信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/rec")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> SetRecInfo([FromBody]ServiceOrderRecUpdationDto input)
            => Result(await _afterSalesCostService.SetRecInfo(input));

        /// <summary>
        /// 获取应收信息
        /// </summary>
        /// <param name="serviceInfoId">客户ID</param>
        /// <param name="serviceOrderId">工单号</param>
        /// <returns></returns>
        [HttpGet("get/rec/{serviceInfoId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceOrderRecDto>> GetRecInfo([FromRoute]long serviceInfoId, [FromQuery]string serviceOrderId)
            => Result(await _afterSalesCostService.GetRecInfo(serviceInfoId, serviceOrderId));


        /// <summary>
        /// 更新工程师提成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/engineer")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> SetEngineerInfo([FromBody]ServiceOrderEngineerUpdationDto input)
            => Result(await _afterSalesCostService.SetEngineerInfo(input));

        /// <summary>
        /// 获取工人提成信息
        /// </summary>
        /// <param name="serviceInfoId">客户ID</param>
        /// <param name="serviceOrderId">工单号</param>
        /// <returns></returns>
        [HttpGet("get/engineer/{serviceInfoId}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceOrderEngineerDto>>> GetEngineerInfo([FromRoute]long serviceInfoId, [FromQuery]string serviceOrderId)
            => Result(await _afterSalesCostService.GetEngineerInfo(serviceInfoId, serviceOrderId));

        /// <summary>
        /// 审核应收优惠
        /// </summary>
        /// <param name="id">应收ID</param>
        /// <param name="reviewer">审核人员</param>
        /// <returns></returns>
        [HttpPut("rec/review/{id}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ReviewRec([FromRoute]long id, [FromQuery]long reviewer)
            => Result(await _afterSalesCostService.ReviewRec(id, reviewer));

        /// <summary>
        /// 设置工资
        /// </summary>
        /// <returns></returns>
        [HttpPut("set/wage")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> SetPayrollRecords([FromBody]GeneratePayrollDto input)
            => Result(await _afterSalesCostService.SetPayrollRecords(input));


        /// <summary>
        /// 生成工资
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="setmonth">生成工资月份</param>
        /// <returns></returns>
        [HttpPost("reset/wage/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> GeneratePayrollRecords([FromRoute]long companyid, [FromQuery]string setmonth)
            => Result(await _afterSalesCostService.GeneratePayrollRecords(companyid, setmonth));

        /// <summary>
        /// 查询工资
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="setmonth"></param>
        /// <returns></returns>
        [HttpGet("get/wage/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceOrderEngineerGrantDto>>> GetPayrollRecordsList([FromRoute]long companyid, [FromQuery]string setmonth)
            => Result(await _afterSalesCostService.GetPayrollRecordsList(companyid, setmonth));

        /// <summary>
        /// 标记工资是否发放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("mark/wage/batch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> MarkedWagePayment([FromBody]ServiceOrderEngineerGrantUpdationDto input)
            => Result(await _afterSalesCostService.MarkedWagePayment(input));

        /// <summary>
        /// 获取所有应收待审核
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [HttpGet("get/unrec/list/{companyid}")]
        //[AllowAnonymous]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceOrderRecDto>>> GetUnRecInfo([FromRoute]long companyid)
            =>Result(await _afterSalesCostService.GetUnRecInfo(companyid));


    }
}
