namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Subscribe_Detail : EfEntity
    {

        /// <summary>
        /// 公众号用户OPENID
        /// </summary>
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// 共享ID
        /// </summary>
        public string UnionId { get; set; } = string.Empty;

        /// <summary>
        /// 是否关注  0 未关注    1已关注
        /// </summary>
        public int SubScribe { get; set; } = 1;

    }
}
