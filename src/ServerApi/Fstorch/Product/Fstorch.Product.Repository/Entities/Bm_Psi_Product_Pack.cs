
namespace Fstorch.Product.Repository.Entities
{
    public class Bm_Psi_Product_Pack : EfEntity
    {
        /// <summary>
        /// 套餐类型
        /// </summary>
        public string PackType { get; set; }

        /// <summary> 
        /// 商品ID
        /// </summary>
        public long ProductId { get; set; }

        /// <summary> 
        /// 子商品ID
        /// </summary>
        public long subproductid { get; set; }

        /// <summary> 
        /// 商品数量
        /// </summary>
        public int amount { get; set; }
    }
}
