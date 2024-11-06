namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 故障原因编码
    /// </summary>
    public class Bm_Service_Cause : EfEntity
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
        /// 故障原因
        /// </summary>
        public string CausemalFunction { get; set; }

        /// <summary> 
        /// 需求标签
        /// </summary>
        public string RequestLabel { get; set; }

        /// <summary> 
        /// 解决方案文章ID
        /// </summary>
        public string SolutionArticle { get; set; }

        /// <summary> 
        /// 解决方案附件ID
        /// </summary>
        public string SolutionAttachment { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
