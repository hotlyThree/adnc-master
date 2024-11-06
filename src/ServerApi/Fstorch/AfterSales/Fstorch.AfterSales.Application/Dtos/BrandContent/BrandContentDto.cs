namespace Fstorch.AfterSales.Application.Dtos.BrandContent
{
    [Serializable]
    public class BrandContentDto : OutputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 品牌内容名称
        /// </summary>
        public string BrandContentName { get; set; }

        /// <summary> 
        /// 服务品牌编码
        /// </summary>
        public string ServiceBrandId { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int DisplayNumber { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
