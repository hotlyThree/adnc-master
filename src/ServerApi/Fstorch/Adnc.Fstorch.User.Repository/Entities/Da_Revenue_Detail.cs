namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Revenue_Detail : EfEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 推荐用户ID
        /// </summary>
        public long Recommendid { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Describe { get; set; } = string.Empty;

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime Createtime { get; set; }
    }
}
