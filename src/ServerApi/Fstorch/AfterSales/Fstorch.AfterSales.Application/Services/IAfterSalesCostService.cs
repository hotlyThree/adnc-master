namespace Fstorch.AfterSales.Application.Services
{
    public interface IAfterSalesCostService : IAppService
    {
        /// <summary>
        /// 更新应付信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> SetCopeInfo(ServiceOrderCopeUpdationDto input);


        /// <summary>
        /// 获取应付信息
        /// </summary>
        /// <param name="serviceInfoId"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        Task<AppSrvResult<ServiceOrderCopeDto>> GetCopeInfo(long serviceInfoId, string serviceOrderId);

        /// <summary>
        /// 更新应收信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> SetRecInfo(ServiceOrderRecUpdationDto input);

        /// <summary>
        /// 获取应收信息
        /// </summary>
        /// <param name="serviceInfoId"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        Task<AppSrvResult<ServiceOrderRecDto>> GetRecInfo(long serviceInfoId, string serviceOrderId);

        /// <summary>
        /// 获取所有应收待审核
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderRecDto>>> GetUnRecInfo(long companyid);


        /// <summary>
        /// 更新工程师提成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> SetEngineerInfo(ServiceOrderEngineerUpdationDto input);

        /// <summary>
        /// 获取工人提成信息
        /// </summary>
        /// <param name="serviceInfoId"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderEngineerDto>>> GetEngineerInfo(long serviceInfoId, string serviceOrderId);

        /// <summary>
        /// 审核应收优惠
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewer"></param>
        /// <returns></returns>
        Task<AppSrvResult> ReviewRec(long id, long reviewer);

        /// <summary>
        /// 设置工资
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult> SetPayrollRecords(GeneratePayrollDto input);

        /// <summary>
        /// 生成工资
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult> GeneratePayrollRecords(long companyid, string setmonth);

        /// <summary>
        /// 查询工资
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceOrderEngineerGrantDto>>> GetPayrollRecordsList(long companyid, string setmonth);

        /// <summary>
        /// 标记工资是否发放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> MarkedWagePayment(ServiceOrderEngineerGrantUpdationDto input);

    }
}
