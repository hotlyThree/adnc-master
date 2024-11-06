namespace Fstorch.AfterSales.Repository.Entities
{
    public class Da_Service_Eng_Grant : EfEntity
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 结算月份
        /// </summary>
        public string SettlementMonth { get; set; }

        /// <summary>
        /// 工程师ID
        /// </summary>
        public long EngineerId { get; set; }

        /// <summary>
        /// 结算周次
        /// </summary>
        public int SettlementWeek { get; set; }

        /// <summary>
        /// 提成合计
        /// </summary>
        public decimal TakesSum { get; set; }

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
        /// 发放批次 1已发 0待发
        /// </summary>
        public string GrantStatus { get; set; }

        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? GrantTime { get; set; }

    }
}
