namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务工程师
    /// </summary>
    public class Da_Service_Order_Engineer : EfEntity
    {

        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoId { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderId { get; set; }

        /// <summary> 
        /// 工程师ID
        /// </summary>
        public long EngineerId { get; set; }

        /// <summary> 
        /// 工程师姓名
        /// </summary>
        public string EngineerName { get; set; }

        /// <summary> 
        /// 工作部门
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary> 
        /// 占比
        /// </summary>
        public decimal Proportion { get; set; }

        /// <summary> 
        /// 提成合计
        /// </summary>
        public decimal Takessum { get; set; }

        /// <summary> 
        /// 提成1
        /// </summary>
        public decimal Takes1 { get; set; }

        /// <summary> 
        /// 提成2
        /// </summary>
        public decimal Takes2 { get; set; }

        /// <summary> 
        /// 提成3
        /// </summary>
        public decimal Takes3 { get; set; }

        /// <summary> 
        /// 提成4
        /// </summary>
        public decimal Takes4 { get; set; }

        /// <summary> 
        /// 提成5
        /// </summary>
        public decimal Takes5 { get; set; }

        /// <summary> 
        /// 提成6
        /// </summary>
        public decimal Takes6 { get; set; }

        /// <summary> 
        /// 提成7
        /// </summary>
        public decimal Takes7 { get; set; }

        /// <summary> 
        /// 提成8
        /// </summary>
        public decimal Takes8 { get; set; }

        /// <summary> 
        /// 提成9
        /// </summary>
        public decimal Takes9 { get; set; }

        /// <summary> 
        /// 提成10
        /// </summary>
        public decimal Takes10 { get; set; }

        /// <summary> 
        /// 提成11
        /// </summary>
        public decimal Takes11 { get; set; }

        /// <summary>
        /// 工程师确认方式  A自动确认 B工程师确认
        /// </summary>
        public string EngAcceptType { get; set; } = string.Empty;

        /// <summary>
        /// 工程师确认结果 A同意 B异议
        /// </summary>
        public string EngAcceptResult { get; set; } = string.Empty;

        /// <summary>
        /// 工程师申诉情况
        /// </summary>
        public string EngAcceptDescribe { get; set; } = string.Empty;

        /// <summary>
        /// 工程师确认时间
        /// </summary>
        public DateTime? EngAcceptTime { get; set; }
    }
}
