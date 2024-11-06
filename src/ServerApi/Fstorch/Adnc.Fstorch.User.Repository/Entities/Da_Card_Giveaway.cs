namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Card_Giveaway : EfEntity
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
        /// 推荐码（随机，不重复）
        /// </summary>
        public string Referralcode { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Expirationdate { get; set; }

        /// <summary>
        /// 使用用户ID
        /// </summary>
        public long Usecode { get; set; }

        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime? Usedate { get; set; }
    }
}
