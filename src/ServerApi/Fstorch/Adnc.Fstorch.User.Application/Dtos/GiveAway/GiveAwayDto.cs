namespace Adnc.Fstorch.User.Application.Dtos.GiveAway
{
    public class GiveAwayDto : OutputDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 推荐码（随机，不重复）
        /// </summary>
        public string Referralcode { get; set; }

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
        public string Membertype { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Expirationdate { get; set; }

        /// <summary>
        /// 使用用户ID
        /// </summary>
        public long Usecode { get; set; }

        /// <summary>
        /// 使用用户名称
        /// </summary>
        public string Usename { get; set; }

        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime? Usedate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get
            {
                string result = "";
                if (Usecode == 0 && Expirationdate > DateTime.Now)
                    result = "未使用";
                else if (Usecode == 0 && Expirationdate < DateTime.Now)
                    result = "已到期";
                else if (Usecode > 0)
                    result = "已使用";
                return result;
            }
        }
    }
}
