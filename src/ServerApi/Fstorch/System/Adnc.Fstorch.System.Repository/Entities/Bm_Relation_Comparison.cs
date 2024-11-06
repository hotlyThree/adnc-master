using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Bm_Relation_Comparison : EfEntity
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 类型编码
        /// </summary>
        public string ServicetypeID { get; set; }

        /// <summary> 
        /// 服务品牌编码
        /// </summary>
        public string ServiceBrandID { get; set; }

        /// <summary> 
        /// 品牌内容名称
        /// </summary>
        public string BrandContentName { get; set; }

        /// <summary> 
        /// 服务流程编码
        /// </summary>
        public string ServiceProcessID { get; set; }


    }
}
