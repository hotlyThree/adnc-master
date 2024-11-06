namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Pay_Detail : EfEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long Memberid { get; set; }

        /// <summary>
        /// 会员单价
        /// </summary>
        public decimal Memberprice { get; set; }

        /// <summary>
        /// 购买年数
        /// </summary>
        public decimal Memberamount { get; set; }

        /// <summary>
        /// 购买金额
        /// </summary>
        public decimal Membermoney { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime Expirationdate { get; set; }

        /// <summary>
        /// 支付状态（A待付 B付款成功 C付款失败 D取消支付）
        /// </summary>
        public string Status { get; set; } = "A";
    }
}
