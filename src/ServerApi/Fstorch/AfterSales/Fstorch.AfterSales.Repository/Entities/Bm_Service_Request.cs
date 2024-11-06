namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务需求编码
    /// </summary>
    public class Bm_Service_Request : EfEntity
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 产品品牌
        /// </summary>
        public string Branding { get; set; }

        /// <summary> 
        /// 品牌内容
        /// </summary>
        public string BrandingContent { get; set; }

        /// <summary> 
        /// 需求描述
        /// </summary>
        public string RequestDescribe { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
