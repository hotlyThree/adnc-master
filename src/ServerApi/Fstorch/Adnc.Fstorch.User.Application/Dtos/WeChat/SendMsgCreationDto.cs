namespace Adnc.Fstorch.User.Application.Dtos.WeChat
{
    public class SendMsgCreationDto
    {
        /// <summary>
        /// 接收者（用户）的 openid
        /// </summary>
        public string Touser { get; set; }

        /// <summary>
        /// 所需下发的订阅模板id
        /// </summary>
        public string Template_id { get; set; }


        /// <summary>
        /// 模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }的object
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 小程序跳转
        /// </summary>
        public MiniProgram MiniPro { get; set; }
    }

    public class MiniProgram
    {
        /// <summary>
        /// 小程序Appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 跳转路径
        /// </summary>
        public string pagepath { get; set; } = "pages/index/index";
    }
}
