namespace Adnc.Fstorch.User.Application.Dtos.PayDetail
{
    public class PayDetailDto : OutputDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public long Memberid { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string Membername { get; set; } = string.Empty;

        /// <summary>
        /// 会员类型（A个人 B企业）
        /// </summary>
        public string Membertype { get; set; } = string.Empty;

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

        /// <summary>
        /// 支付状态名称
        /// </summary>
        public string Statuslabel
        {
            get
            {
                string result = "";
                result = Status switch
                {
                    "A" => "待支付",
                    "B" => "付款成功",
                    "C" => "付款失败",
                    "D" => "取消支付"
                };
                return result;
            }
        }
    }
}
