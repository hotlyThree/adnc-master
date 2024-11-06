



namespace Fstorch.AfterSales.Application.Services
{
    public interface IAfterSalesOrderService : IAppService
    {
        /// <summary>
        /// 派工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> DispatchWorkOrderAsync(ServiceOrderCreationDto input);

        /// <summary>
        /// 更新工单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateOrderAsync(ServiceOrderUpdationDto input);

        /// <summary>
        /// 批量派工
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> DispatchWorkOrderBatchAsync(ServiceOrderDispatchBatchDto input);


        Task<AppSrvResult<string>> GetNewWorkOrder(long companyid);


        /// <summary>
        /// 改派
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ReassignmentWorkOrderAsync(ServiceOrderReassignmentDto input);

        /// <summary>
        /// 完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ComplatedWorkOrderAsync(ServiceOrderComplateDto input);

        /// <summary>
        /// 作废工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> CancelWorkOrderAsync(ServiceOrderCancelDto input);

        /// <summary>
        /// 工单回访
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> ServiceOrderFollow(ServiceOrderFollowCreationDto input);

        /// <summary>
        /// 工单回访明细
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderFollowDto>>> GetServiceOrderFollowAsync(long companyid, string serviceOrderId);

        /// <summary>
        /// 查询服务客户工单分页列表
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult<PageModelDto<ServiceOrderDto>>> GetWorkOrderPagedAsync(ServiceOrderSearchPagedDto search);

        /// <summary>
        /// 获取服务客户工单产品明细
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderProductDto>>> GetServiceOrderProductAsync(long companyid, long orderid);

        /// <summary>
        /// 增加过程反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateProcessAsync(ServiceOrderProcessCreationDto input);

        /// <summary>
        /// 回复过程反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ReplyProcessAsync(ServiceOrderProcessUpdationDto input);

        /// <summary>
        /// 查询服务客户工单过程信息列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderProcessDto>>> GetProcessListAsync(long companyid, long orderid);


        /// <summary>
        /// 今日派工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> GetDispatchCountReport(CountReportDto input);

        /// <summary>
        /// 今日完工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> GetComplateCountReport(CountReportDto input);

        /// <summary>
        /// 未派工统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> GetUnDispatchCountReport(CountReportDto input);

        /// <summary>
        /// 服务中统计,包含工程师工单统计、今日预约、明日预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> GetWorkingCountReport(CountReportDto input);

        /// <summary>
        /// 回访统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> GetFollowCountReport(FollowCountReportDto input);

        /// <summary>
        /// 24小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> Count24UncompletedWorkOrder(CountReportDto input);

        /// <summary>
        /// 48小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> Count48UncompletedWorkOrder(CountReportDto input);

        /// <summary>
        /// 72小时未完工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> Count72UncompletedWorkOrder(CountReportDto input);

        /// <summary>
        /// 待接单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountPendingWorkOrder(CountReportDto input);

        /// <summary>
        /// 待件工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountPendingPartWorkOrder(CountReportDto input);

        /// <summary>
        /// 遗留工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountLegacyWorkOrder(CountReportDto input);

        /// <summary>
        /// 拒绝派工工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountRefuseWorkOrder(CountReportDto input);

        /// <summary>
        /// 待回访工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountReadyToFollowWorkOrder(CountReportDto input);

        /// <summary>
        /// 待核销
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountPendingVerificationWorkOrder(CountReportDto input);

        /// <summary>
        /// 待结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> CountPendingSettlementWorkOrder(CountReportDto input);

        /// <summary>
        /// 预计工程师预约序号
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult<int>> ExpectedEngineerAppointmentNumber(CountEngineerAppointmentDto input);

        /// <summary>
        /// 查询已拒绝工单列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderDto>>> GetRefuseOrderList(long companyid);
    }
}
