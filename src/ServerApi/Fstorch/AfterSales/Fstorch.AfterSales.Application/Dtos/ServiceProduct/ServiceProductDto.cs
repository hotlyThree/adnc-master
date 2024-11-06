namespace Fstorch.AfterSales.Application.Dtos.ServiceProduct
{
    [Serializable]
    public class ServiceProductDto : OutputDto
    {

        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoID { get; set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        public long? OrderId { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 产品序号
        /// </summary>
        public int ProductSerial { get; set; }

        /// <summary>
        /// 服务流程编号
        /// </summary>
        public string ServiceProcess { get; set; } = string.Empty;

        /// <summary>
        /// 工单状态
        /// </summary>
        public string ServiceOrderStatus { get; set; }

        /// <summary>
        /// 处理开始时间
        /// </summary>
        public DateTime? ProcessingStartTime { get; set; }

        /// <summary>
        /// 处理结束时间
        /// </summary>
        public DateTime? ProcessingEndTime { get; set; }


        /// <summary>
        /// 工程师姓名列表
        /// </summary>
        public string? StaffNames { get; set; }

        /// <summary> 
        /// 产品类型
        /// </summary>
        public string? ProductType { get; set; }

        /// <summary> 
        /// 产品品牌
        /// </summary>
        public string? Branding { get; set; }

        /// <summary> 
        /// 品牌内容
        /// </summary>
        public string? BrandedContent { get; set; }

        /// <summary> 
        /// 品牌型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary> 
        /// 型号数量
        /// </summary>
        public int ModelNumber { get; set; }

        /// <summary> 
        /// 服务类型
        /// </summary>
        public string? ServiceTypeID { get; set; }

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
        public string? PurchaseStore { get; set; }

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
        public string? Barcode1 { get; set; }

        /// <summary> 
        /// 条码2
        /// </summary>
        public string? Barcode2 { get; set; }
    }
}
