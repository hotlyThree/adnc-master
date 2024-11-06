namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderRec
{
    public class ServiceOrderRecCreationAndUpdationDto : InputDto
    {
        /// <summary> 
        /// 保内工厂应收
        /// </summary>
        public decimal GuaranteeDIncomeManufactor { get; set; }

        /// <summary> 
        /// 保内商家应收
        /// </summary>
        public decimal GuaranteeDIncomeBusiness { get; set; }

        /// <summary> 
        /// 保内应收1
        /// </summary>
        public decimal Income1 { get; set; }

        /// <summary> 
        /// 保内应收2
        /// </summary>
        public decimal Income2 { get; set; }

        /// <summary> 
        /// 保内应收3
        /// </summary>
        public decimal Income3 { get; set; }

        /// <summary> 
        /// 保内应收4
        /// </summary>
        public decimal Income4 { get; set; }

        /// <summary> 
        /// 保内应收5
        /// </summary>
        public decimal Income5 { get; set; }

        /// <summary> 
        /// 保内应收6
        /// </summary>
        public decimal Income6 { get; set; }

        /// <summary> 
        /// 保内应收7
        /// </summary>
        public decimal Income7 { get; set; }

        /// <summary> 
        /// 保内应收8
        /// </summary>
        public decimal Income8 { get; set; }

        /// <summary> 
        /// 保内应收9
        /// </summary>
        public decimal Income9 { get; set; }

        /// <summary> 
        /// 保内应收10
        /// </summary>
        public decimal Income10 { get; set; }

        /// <summary> 
        /// 保内应收11
        /// </summary>
        public decimal Income11 { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 优惠说明
        /// </summary>
        public string reason { get; set; }
    }
}
