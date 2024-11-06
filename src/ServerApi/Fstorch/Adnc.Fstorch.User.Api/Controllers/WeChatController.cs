
using Adnc.Fstorch.User.Application.Dtos.WeChat;

namespace Adnc.Fstorch.User.Api.Controllers
{
    [ApiController]
    [Route($"{RouteConsts.UserRoot}")]
    public class WeChatController : AdncControllerBase
    {
        private readonly IWeChatAppService _weChatAppService;
        public WeChatController(IWeChatAppService weChatAppService)
        {
            _weChatAppService = weChatAppService;
        }

        /// <summary>
        /// 获取不限制的小程序码
        /// </summary>
        /// <param name="appid">小程序appid</param>
        /// <param name="path">跳转路径</param>
        /// <param name="scene">跳转参数</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("qrcode")]
        public async Task<ActionResult<byte[]>> GetQrCodeAsync([FromQuery]string appid, [FromQuery] string path, [FromQuery] string scene)
            => Result(await _weChatAppService.GetQrCodeAsync(appid, path, scene));


        /// <summary>
        /// 音视频内容安全识别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("media/check")]
        public async Task<ActionResult<object>> MediaCheckAsync([FromBody]MediaCheckDto input)
            => await _weChatAppService.MediaCheckAsync(input);


        /*[HttpPost("send/{id}")]
        [AllowAnonymous]
        public async Task SendMessageToAll([FromRoute] string id)
        {
            var buffer = Encoding.UTF8.GetBytes(id);

            foreach (var socket in CustomerWebSocketManager.ConnectedSockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }*/
    }
}
