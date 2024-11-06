namespace Adnc.Fstorch.User.Application.Dtos.PayDetail
{
    public class PayDetailCreationDto : InputDto
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
    }
}
