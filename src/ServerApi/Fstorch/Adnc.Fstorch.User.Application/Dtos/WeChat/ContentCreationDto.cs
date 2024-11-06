namespace Adnc.Fstorch.User.Application.Dtos.WeChat
{
    public class ContentCreationDto
    {
        /// <summary>
        /// 用户的openid（用户需在近两小时访问过小程序）
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 需检测的文本内容，文本字数的上限为2500字，需使用UTF-8编码
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 接口版本号，2.0版本为固定值2
        /// </summary>
        public int version { get; set; } = 2;

        /// <summary>
        /// 场景枚举值（1 资料；2 评论；3 论坛；4 社交日志）
        /// </summary>
        public int scene { get; set; } = 2;
    }
}
