namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    public class MediaCheckRto
    {
        public MediaCheckRto(string Media_url, int Media_type, long Uid)
        => (media_url, media_type, uid) = (Media_url, Media_type, Uid);


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
        /// 用户ID
        /// </summary>
        public long uid { get; set; }
    }
}
