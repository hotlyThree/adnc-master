namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Cash_Out : EfEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Outmoney { get; set; }

        /// <summary>
        /// 提现税金
        /// </summary>
        public decimal Taxes { get; set; }

        /// <summary>
        /// 提现日期
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 发放日期
        /// </summary>
        public DateTime? Accepttime { get; set; }

        /// <summary>
        /// 支付状态（A待付 B付款成功 C付款失败 D拒绝支付）
        /// </summary>
        public string Status { get; set; } = "A";
    }
}
