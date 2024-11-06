namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderProduct
{
    public class ServiceOrderProductDto : OutputDto
    {
        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderId { get; set; }

        /// <summary> 
        /// 产品序号
        /// </summary>
        public int ProductSerial { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public long ProductId { get; set; }

        /// <summary> 
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; } = string.Empty;

        /// <summary> 
        /// 产品品牌
        /// </summary>
        public string Branding { get; set; } = string.Empty;

        /// <summary> 
        /// 品牌内容
        /// </summary>
        public string BrandedContent { get; set; } = string.Empty;

        /// <summary> 
        /// 品牌型号
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary> 
        /// 型号数量
        /// </summary>
        public int ModelNumber { get; set; }

        /// <summary> 
        /// 服务类型
        /// </summary>
        public string ServiceTypeID { get; set; } = string.Empty;

        /// <summary> 
        /// 购机日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary> 
        /// 购机单价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary> 
        /// 购机商场
        /// </summary>
        public string PurchaseStore { get; set; } = string.Empty;

        /// <summary> 
        /// 保内商家费用
        /// </summary>
        public decimal GuaranteedIncomeBusiness { get; set; }

        /// <summary> 
        /// 保内厂家费用
        /// </summary>
        public decimal GuaranteedIncomeManufactor { get; set; }

        /// <summary> 
        /// 条码1
        /// </summary>
        public string Barcode1 { get; set; } = string.Empty;

        /// <summary> 
        /// 条码2
        /// </summary>
        public string Barcode2 { get; set; } = string.Empty;
    }
}
