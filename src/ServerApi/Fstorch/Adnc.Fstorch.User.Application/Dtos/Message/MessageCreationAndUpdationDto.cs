namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageCreationAndUpdationDto : InputDto
    {
        /// <summary>
        /// 主页展示（1是，0否）
        /// </summary>
        public int Ishome { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容（主页展示）
        /// </summary>
        public string? Memo { get; set; } = string.Empty;

        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumimg { get; set; } = string.Empty;
    }
}
