namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务结算代码
    /// </summary>
    public class Bm_Settlement_Code : EfEntity
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务商ID
        /// </summary>
        public string ServiceProviderId { get; set; }

        /// <summary> 
        /// 提成公式描述
        /// </summary>
        public string SettlementDescribe { get; set; }

        /// <summary> 
        /// 提成公式
        /// </summary>
        public string SettlementFormula { get; set; }

        /// <summary> 
        /// 结算条件
        /// </summary>
        public string SettlementCondition { get; set; }

        /// <summary> 
        /// 结算条件描述
        /// </summary>
        public string SettlementConditionDescribe { get; set; }

        /// <summary> 
        /// 服务商提成
        /// </summary>
        public decimal ProviderCommission { get; set; }

        /// <summary> 
        /// 工程师提成
        /// </summary>
        public decimal EngineerCommission { get; set; }

        /// <summary> 
        /// 保内应收
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
