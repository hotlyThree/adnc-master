namespace Adnc.Fstorch.User.Application.Dtos.Account
{
    [Serializable]
    public class AccountDto : OutputDto
    {
        /// <summary>
        /// 微信OPENID
        /// </summary>
        public string Openid { get; set; } = string.Empty;


        /// <summary>
        /// 推荐人ID
        /// </summary>
        public long Referralid { get; set; }

        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string Referralname { get; set; }

        /// <summary>
        /// 推荐码
        /// </summary>
        public string Referralcode { get; set; } = string.Empty;

        /// <summary>
        /// 个性标签
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 总计金额
        /// </summary>
        public decimal Totalamount { get; set; }

        /// <summary>
        /// 已提现金额
        /// </summary>
        public decimal Cashwithdrawal { get; set; }

        /// <summary>
        /// 未提现金额
        /// </summary>
        public decimal Nowithdrawal { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long Memberid { get; set; }

        /// <summary>
        /// 会员有效期
        /// </summary>
        public DateTime? Expirationdate { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Openname { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间/注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 共享ID
        /// </summary>
        public string UnionId {get;set;} = string.Empty;
    }
}
