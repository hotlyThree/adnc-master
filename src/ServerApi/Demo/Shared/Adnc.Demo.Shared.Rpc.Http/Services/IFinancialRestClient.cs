using Adnc.Shared.Rpc.Http;

namespace Adnc.Demo.Shared.Rpc.Http.Services
{
    public interface IFinancialRestClient : IRestClient
    {
        /// <summary>
        /// 获取财务资金库编码表
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/Financial/CodeBase/GetBmFinancialAccountAsync")]
        Task<ApiResponse<List<Bm_Financial_AccountRto>>> GetBmFinancialAccountAsync(long CompanyID);



        /// <summary>
        /// 获取财务收支项目编码表
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        [Post("/Financial/CodeBase/GetBmFinancialProjectAsync")]
        Task<ApiResponse<List<Bm_Financial_ProjectRto>>> GetBmFinancialProjectAsync(long CompanyID);


        /// <summary>
        /// 审核财务订单-写入收支明细表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Post("/Financial/Order/AcceptOrderIDAsync")]
        Task<ApiResponse<long>> AcceptOrderIDAsync(long ID);


        /// <summary>
        /// 写入收支明细表
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <param name="AccountNumber">往来账号</param>
        /// <param name="CustomID">单位ID</param>
        /// <param name="CustomType">单位类型</param>
        /// <param name="BusinessType">业务类型</param>
        /// <param name="BusinessNumber">业务单号</param>
        /// <param name="inputDto">往来明细</param>
        /// <returns></returns>
        [Post("/Financial/Order/AcceptOrderDetailAsync")]
        Task<ApiResponse<long>> AcceptOrderDetailAsync(long CompanyID, long AccountNumber, long CustomID, string CustomType,
            string BusinessType, long BusinessNumber, List<Da_Financial_ReconciliOrderRto> inputDto);



    }
}
