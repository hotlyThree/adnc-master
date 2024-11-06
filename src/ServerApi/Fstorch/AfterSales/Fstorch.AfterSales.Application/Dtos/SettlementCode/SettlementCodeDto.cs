namespace Fstorch.AfterSales.Application.Dtos.SettlementCode
{
    [Serializable]
    public class SettlementCodeDto : OutputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long companyid { get; set; }

        /// <summary> 
        /// 服务商ID
        /// </summary>
        public string serviceproviderid { get; set; }

        /// <summary> 
        /// 提成公式描述
        /// </summary>
        public string settlementdescribe { get; set; }

        /// <summary> 
        /// 提成公式
        /// </summary>
        public string settlementformula { get; set; }

        /// <summary> 
        /// 结算条件
        /// </summary>
        public string settlementcondition { get; set; }

        /// <summary> 
        /// 结算条件描述
        /// </summary>
        public string settlementconditiondescribe { get; set; }

        /// <summary> 
        /// 服务商提成
        /// </summary>
        public decimal providercommission { get; set; }

        /// <summary> 
        /// 工程师提成
        /// </summary>
        public decimal engineercommission { get; set; }

        /// <summary> 
        /// 保内应收
        /// </summary>
        public decimal receivable { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string isvalid { get; set; }
    }
}
