using Adnc.Fstorch.User.Application.Dtos.WeChat;

namespace Adnc.Fstorch.User.Application.Services
{
    public interface IWeChatAppService : IAppService
    {
        /// <summary>
        /// 获取不限制的小程序码
        /// </summary>
        /// <param name="appid">小程序appid</param>
        /// <param name="path">跳转路径</param>
        /// <param name="scene">跳转参数</param>
        /// <returns></returns>
        Task<AppSrvResult<byte[]>> GetQrCodeAsync(string appid, string path, string scene);

        /// <summary>
        /// 模板订阅消息发送
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> SendMsgByModelAsync(SendMsgCreationDto input);

        /// <summary>
        /// 媒体新闻类用户发表文章内容检测   V1.0
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, object>> MsgSecCheck(string content);

        /// <summary>
        /// 评论内容检测  V2.0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<object> CommentCheck(ContentCreationDto input);

        /// <summary>
        /// 音视频内容安全识别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<object> MediaCheckAsync(MediaCheckDto input);
    }
}
