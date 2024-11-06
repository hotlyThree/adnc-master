namespace Fstorch.Product.Repository.Entities
{
    public class Bm_Psi_Product_Profile
    {

        /// <summary> 
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 商品类别ID
        /// </summary>
        public long ClassId { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public long SerialNumber { get; set; }

        /// <summary> 
        /// 商品名称
        /// </summary>
        public string name { get; set; }

        /// <summary> 
        /// 商品型号
        /// </summary>
        public string marque { get; set; }

        /// <summary> 
        /// 商品单位
        /// </summary>
        public string unit { get; set; }

        /// <summary> 
        /// 商品颜色
        /// </summary>
        public string colour { get; set; }

        /// <summary> 
        /// 商品体积
        /// </summary>
        public decimal volume { get; set; }

        /// <summary> 
        /// 商品重量
        /// </summary>
        public decimal weight { get; set; }

        /// <summary> 
        /// 商品产地
        /// </summary>
        public string area { get; set; }

        /// <summary> 
        /// 厂家编码
        /// </summary>
        public string factorynumber { get; set; }

        /// <summary> 
        /// 商品备注
        /// </summary>
        public string describe { get; set; }

        /// <summary> 
        /// 质保月份
        /// </summary>
        public int warrantymonths { get; set; }

        /// <summary> 
        /// 质保说明
        /// </summary>
        public string warrantymemo { get; set; }

        /// <summary> 
        /// 是否服务
        /// </summary>
        public string isservice { get; set; }

        /// <summary> 
        /// 服务商家
        /// </summary>
        public string serviceproviderid { get; set; }

        /// <summary> 
        /// 服务品牌
        /// </summary>
        public long serviceproductid { get; set; }

        /// <summary> 
        /// 提醒类型
        /// </summary>
        public string remindtype { get; set; }

        /// <summary> 
        /// 提醒周期
        /// </summary>
        public int remindnumber { get; set; }

        /// <summary> 
        /// 默认税率
        /// </summary>
        public decimal taxrate { get; set; }

        /// <summary> 
        /// 销售状态
        /// </summary>
        public string saletype { get; set; }

        /// <summary> 
        /// 商品类型
        /// </summary>
        public string producttype { get; set; }

        /// <summary> 
        /// 是否套餐
        /// </summary>
        public string ispack { get; set; }

        /// <summary> 
        /// 包含件数
        /// </summary>
        public int subamount { get; set; }
    }
}
