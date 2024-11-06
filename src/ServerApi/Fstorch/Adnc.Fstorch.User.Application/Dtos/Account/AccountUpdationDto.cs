namespace Adnc.Fstorch.User.Application.Dtos.Account
{
    public class AccountUpdationDto : InputDto
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 个性标签
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 共享ID
        /// </summary>
        public string UnionId { get; set; } = string.Empty;
    }
}
