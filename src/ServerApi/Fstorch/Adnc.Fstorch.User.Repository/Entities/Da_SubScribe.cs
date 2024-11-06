namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_SubScribe : EfEntity
    {
        /// <summary>
        /// 公众号OPENID
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
