namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务工单产品
    /// </summary>
    public class Da_Service_Order_Produc : EfEntity
    {

        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoId { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderId { get; set; }

        /// <summary> 
        /// 产品序号
        /// </summary>
        public int ProductSerial { get; set; }

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string ServiceProcessId { get; set; } = string.Empty;

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }
    }
}
