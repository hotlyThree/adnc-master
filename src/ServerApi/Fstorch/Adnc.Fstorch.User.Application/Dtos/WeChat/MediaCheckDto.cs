namespace Adnc.Fstorch.User.Application.Dtos.WeChat
{
    /// <summary>
    /// 本接口用于异步校验图片/音频是否含有违法违规内容。
    ///1.0 版本异步接口文档【点击查看】， 1.0 版本同步接口文档【点击查看】，1.0版本在2021年9月1日停止更新，请尽快更新至2.0
    ///应用场景举例：
    ///语音风险识别：社交类用户发表的语音内容检测；
    ///图片智能鉴黄：涉及拍照的工具类应用(如美拍，识图类应用)用户拍照上传检测；电商类商品上架图片检测；媒体类用户文章里的图片检测等；
    ///敏感人脸识别：用户头像；媒体类用户文章里的图片检测；社交类用户上传的图片检测等。 频率限制：单个 appId 调用上限为 2000 次/分钟，200,000 次/天；文件大小限制：单个文件大小不超过10M
    /// </summary>
    public class MediaCheckDto
    {
        /// <summary>
        /// 要检测的图片或音频的url，支持图片格式包括jpg, jepg, png, bmp, gif（取首帧），支持的音频格式包括mp3, aac, ac3, wma, flac, vorbis, opus, wav
        /// </summary>
        public string media_url { get; set; }

        /// <summary>
        /// 1:音频;2:图片
        /// </summary>
        public int media_type { get; set; }

        /// <summary>
        /// 接口版本号，2.0版本为固定值2
        /// </summary>
        public int version { get; set; } = 2;

        /// <summary>
        /// 场景枚举值（1 资料；2 评论；3 论坛；4 社交日志）
        /// </summary>
        public int scene { get; set; } = 3;

        /// <summary>
        /// 用户id
        /// </summary>
        public long uid { get; set; }
    }
}
