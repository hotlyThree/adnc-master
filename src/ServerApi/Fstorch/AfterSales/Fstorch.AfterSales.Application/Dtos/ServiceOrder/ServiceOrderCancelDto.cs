namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderCancelDto : InputDto 
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 作废原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public long Operator { get; set; }
    }
}
