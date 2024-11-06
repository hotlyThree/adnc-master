
namespace Fstorch.AfterSales.Application.Services
{
    /// <summary>
    /// 售后工单管理
    /// </summary>
    public interface IAfterSalesMainService : IAppService
    {
        /// <summary>
        /// 创建客户服务档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateAsync(ServiceMainCreationDto input);

        /// <summary>
        /// 批量创建客户服务档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> CreateBatchAsync(ServiceMainCreationBatchDto input);

        /// <summary>
        /// 复制客户服务档案
        /// </summary>
        /// <param name="mainid"></param>
        /// <param name="num"></param>
        /// <param name="ordertaker"></param>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> CopyServiceMainBatch(long mainid, int num, long ordertaker);

        /// <summary>
        /// 更新客户服务档案信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateAsync(ServiceMainUpdationDto input);


        /// <summary>
        /// 更改客户服务档案状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeStatus(long id, string status);

        /// <summary>
        /// 批量更改客户服务档案状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeStatusBatch(IEnumerable<long> ids, string status);

        /// <summary>
        /// 增加服务客户产品明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateServiceProductAsync(ServiceProductCreationDto input);

        /// <summary>
        /// 更新客户服务产品信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateServiceProductAsync(ServiceProductUpdationDto input);

        /// <summary>
        /// 删除客户服务产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteServiceProductAsync(long id);


        /// <summary>
        /// 获取服务客户档案分页列表
        /// </summary>
        /// <returns></returns>
        Task<PageModelDto<ServiceMainDto>> GetPagedAsync(ServiceMainSearchPagedDto input);

        /// <summary>
        /// 获取客户档案分页列表 自定义查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<AppSrvResult<PageModelDto<ServiceMainDto>>> GetPagedCustomAsync(ServiceMainSearchPagedCustomDto search);

        /// <summary>
        /// 获取服务客户产品明细
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="mainid"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceProductDto>>> GetServiceProductAsync(long companyid, long mainid);

        /// <summary>
        /// 获取编码是已使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<bool>> ServiceMainIsExistsAsnyc(ServiceMainSearchExistsDto input);

        /// <summary>
        /// 临近客户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<List<ServiceMainDto>>> AdjacentCustomer(ServiceMainAdjacentSearchDto input);
    }
}
