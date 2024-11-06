
namespace Adnc.Demo.Shared.Rpc.Http.Rtos;

    [Serializable]
    /// <summary> 
    /// 资金库编码表
    /// </summary>
    public class Bm_Financial_AccountRto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 账号名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary> 
        /// 账号类型
        /// </summary>
        public long AccountType { get; set; }

        /// <summary> 
        /// 账号状态
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary> 
        /// 服务商ID
        /// </summary>
        public string ProviderID { get; set; }

        /// <summary> 
        /// 适合卖场
        /// </summary>
        public string SalesMallID { get; set; }

        /// <summary> 
        /// 适合服务
        /// </summary>
        public string ServiceID { get; set; }


    }

