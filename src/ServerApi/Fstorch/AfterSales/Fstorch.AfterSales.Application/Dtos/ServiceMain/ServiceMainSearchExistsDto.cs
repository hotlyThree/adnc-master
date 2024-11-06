namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainSearchExistsDto : InputDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Companyid { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 服务流程
        /// </summary>
        public string Serviceprocess { get; set; } = string.Empty;

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
        /// 服务类型
        /// </summary>
        public string ServiceTypeID { get; set; } = string.Empty;

        /// <summary> 
        /// 购机商场
        /// </summary>
        public string PurchaseStore { get; set; } = string.Empty;
    }
}
