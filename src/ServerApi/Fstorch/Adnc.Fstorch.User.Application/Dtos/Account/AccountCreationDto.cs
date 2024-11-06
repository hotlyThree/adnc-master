namespace Adnc.Fstorch.User.Application.Dtos.Account
{
    public class AccountCreationDto : InputDto
    {
        /// <summary>
        /// 微信OPENID
        /// </summary>
        public string Openid { get; set; } = string.Empty;

        /// <summary>
        /// 推荐码
        /// </summary>
        public string Referralcode { get;set; } = string.Empty;

        /// <summary>
        /// 推荐人ID
        /// </summary>
        public long Referralid { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long Memberid { get; set; }

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
    }
}
