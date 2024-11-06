using Adnc.Demo.Shared.Const.Permissions.Fstorch;

namespace Fstorch.AfterSales.Api.Controllers
{
    /// <summary>
    /// 服务客户工单管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.AfterSalesOrder}")]
    public class AfterSalesOrderController : AdncControllerBase
    {
        private readonly IAfterSalesOrderService _afterSalesOrderService;

        public AfterSalesOrderController(IAfterSalesOrderService afterSalesOrderService)
        {
            _afterSalesOrderService = afterSalesOrderService;
        }


        /// <summary>
        /// 派工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("dispatch/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long>> DispatchWorkOrderAsync([FromBody]ServiceOrderCreationDto input)
            => CreatedResult(await _afterSalesOrderService.DispatchWorkOrderAsync(input));


        /// <summary>
        /// 更新工单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("update/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> UpdateOrderAsync([FromBody]ServiceOrderUpdationDto input)
            => Result(await _afterSalesOrderService.UpdateOrderAsync(input));



        /// <summary>
        /// 批量派工
        /// </summary>
        /// <returns></returns>
        [HttpPost("dispatch/order/batch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long[]>> DispatchWorkOrderBatchAsync([FromBody]ServiceOrderDispatchBatchDto input)
            => CreatedResult(await _afterSalesOrderService.DispatchWorkOrderBatchAsync(input));


        /// <summary>
        /// 获取最新工单号
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [HttpPost("new/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<string>> GetNewWorkOrder([FromRoute]long companyid)
            => Result(await _afterSalesOrderService.GetNewWorkOrder(companyid));

        /// <summary>
        /// 改派
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Reassignment/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ReassignmentWorkOrderAsync([FromBody]ServiceOrderReassignmentDto input)
            => Result(await _afterSalesOrderService.ReassignmentWorkOrderAsync(input));

        /// <summary>
        /// 完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("complate/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ComplatedWorkOrderAsync([FromBody]ServiceOrderComplateDto input)
            => Result(await _afterSalesOrderService.ComplatedWorkOrderAsync(input));


        /// <summary>
        /// 作废工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("cancel/order")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> CancelWorkOrderAsync([FromBody]ServiceOrderCancelDto input)
            => Result(await _afterSalesOrderService.CancelWorkOrderAsync(input));

        /// <summary>
        /// 工单回访
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("followup/order")]
        [AllowAnonymous]
        public async Task<ActionResult<long[]>> ServiceOrderFollow([FromBody]ServiceOrderFollowCreationDto input)
            => CreatedResult(await _afterSalesOrderService.ServiceOrderFollow(input));

        /// <summary>
        /// 查询服务客户工单分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("order/page")]
        [AllowAnonymous]
        public async Task<ActionResult<PageModelDto<ServiceOrderDto>>> GetWorkOrderPagedAsync([FromBody]ServiceOrderSearchPagedDto search)
            => Result(await _afterSalesOrderService.GetWorkOrderPagedAsync(search));


        /// <summary>
        /// 获取服务客户工单产品明细
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="orderid">工单ID</param>
        /// <returns></returns>
        [HttpGet("order/product/list/{companyid}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceOrderProductDto>>> GetServiceOrderProductAsync([FromRoute]long companyid, [FromQuery]long orderid)
            => Result(await _afterSalesOrderService.GetServiceOrderProductAsync(companyid, orderid));

        /// <summary>
        /// 增加过程反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/process")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult<long>> CreateProcessAsync([FromBody]ServiceOrderProcessCreationDto input)
            => CreatedResult(await _afterSalesOrderService.CreateProcessAsync(input));

        /// <summary>
        /// 回复过程反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("reply/process")]
        [AdncAuthorize(PermissionConsts.AfterService.DataSet)]
        public async Task<ActionResult> ReplyProcessAsync([FromBody]ServiceOrderProcessUpdationDto input)
            => Result(await _afterSalesOrderService.ReplyProcessAsync(input));

        /// <summary>
        /// 查询服务客户工单过程信息列表
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="orderid">工单ID</param>
        /// <returns></returns>
        [HttpGet("process/list/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceOrderProcessDto>>> GetProcessListAsync([FromRoute]long companyid, [FromQuery]long orderid)
            => Result(await _afterSalesOrderService.GetProcessListAsync(companyid, orderid));



        /// <summary>
        /// 今日派工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("report/dispatch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> GetDispatchCountReport([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.GetDispatchCountReport(input));

        /// <summary>
        /// 今日完工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("report/complated")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> GetComplateCountReport([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.GetComplateCountReport(input));

        /// <summary>
        /// 未派工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("report/undispatch")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> GetUnDispatchCountReport([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.GetUnDispatchCountReport(input));

        /// <summary>
        /// 服务中统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("report/working")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> GetWorkingCountReport([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.GetWorkingCountReport(input));

        /// <summary>
        /// 回访统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("report/follow")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> GetFollowCountReport([FromQuery]FollowCountReportDto input) 
            => Result(await _afterSalesOrderService.GetFollowCountReport(input));

        /// <summary>
        /// 查询已拒绝工单列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [HttpGet("get/refuse/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<List<ServiceOrderDto>>> GetRefuseOrderList([FromRoute]long companyid)
            => Result(await _afterSalesOrderService.GetRefuseOrderList(companyid));

        /// <summary>
        /// 查询工单回访明细
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="serviceOrderId">工单号</param>
        /// <returns></returns>
        [HttpGet("get/followup/order/details/{companyid}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceOrderFollowDto>>> GetServiceOrderFollowAsync([FromRoute]long companyid, [FromQuery]string serviceOrderId) => Result(await _afterSalesOrderService.GetServiceOrderFollowAsync(companyid, serviceOrderId));


        /// <summary>
        /// 24小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("uncomplated/count24")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> Count24UncompletedWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.Count24UncompletedWorkOrder(input));

        /// <summary>
        /// 48小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("uncomplated/count48")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> Count48UncompletedWorkOrder([FromQuery] CountReportDto input)
            => Result(await _afterSalesOrderService.Count48UncompletedWorkOrder(input));

        /// <summary>
        /// 72小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("uncomplated/count72")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> Count72UncompletedWorkOrder([FromQuery] CountReportDto input)
            => Result(await _afterSalesOrderService.Count72UncompletedWorkOrder(input));


        /// <summary>
        /// 待接单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("pending/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountPendingWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountPendingWorkOrder(input));

        /// <summary>
        /// 待件工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("pending/part/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountPendingPartWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountPendingPartWorkOrder(input));

        /// <summary>
        /// 遗留工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("legacy/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountLegacyWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountLegacyWorkOrder(input));

        /// <summary>
        /// 拒绝派工工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("refuse/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountRefuseWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountRefuseWorkOrder(input));

        /// <summary>
        /// 待回访工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("followup/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountReadyToFollowWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountReadyToFollowWorkOrder(input));

        /// <summary>
        /// 待核销
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("pending/verification/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountPendingVerificationWorkOrder([FromQuery]CountReportDto input)
            => Result(await _afterSalesOrderService.CountPendingVerificationWorkOrder(input));

        /// <summary>
        /// 待结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("pending/settlement/workorder/count")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<object>> CountPendingSettlementWorkOrder([FromQuery] CountReportDto input)
            => Result(await _afterSalesOrderService.CountPendingSettlementWorkOrder(input));

        /// <summary>
        /// 预计工程师预约序号
        /// </summary>
        /// <returns></returns>
        [HttpGet("expected/engineer/appointment/number")]
        [AdncAuthorize(PermissionConsts.AfterService.DataQuery)]
        public async Task<ActionResult<int>> ExpectedEngineerAppointmentNumber([FromQuery]CountEngineerAppointmentDto input)
            => Result(await _afterSalesOrderService.ExpectedEngineerAppointmentNumber(input));

        /*public async Task<ActionResult> SendMessageTest([FromQuery] string message)
        {

        }*/

    }
}
