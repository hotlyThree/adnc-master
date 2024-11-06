namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        /// 消息标题 / 发布人 / 公司名称
        /// </summary>
        public string SearchInput { get; set; } = string.Empty;
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public List<DateTime>? Timestamp { get; set; } = new List<DateTime>();
    }
}
