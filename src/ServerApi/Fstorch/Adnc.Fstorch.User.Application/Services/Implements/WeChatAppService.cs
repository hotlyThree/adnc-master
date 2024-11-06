using Adnc.Fstorch.User.Application.Dtos.WeChat;
using Newtonsoft.Json;
using Polly;
using System.Text;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class WeChatAppService : AbstractAppService, IWeChatAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CacheService _cacheService;
        public WeChatAppService(IHttpClientFactory httpClientFactory,CacheService cacheService)
        {
            _httpClientFactory = httpClientFactory;
            _cacheService = cacheService;
        }


        public async Task<AppSrvResult<byte[]>> GetQrCodeAsync(string appid, string path, string scene)
        {
            #region 获取access_token
            string access_token = await _cacheService.GetAccessTokenAsync(appid);
                if (access_token.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.NotFound, $"获取access_token失败");
            #endregion
            #region 获取二维码
            string barcode_request_uri = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={access_token}";
            var barcode_request = _httpClientFactory.CreateClient();
            object parms = new
            {
                page = path,
                scene = scene,
            };
            var content = new StringContent(JsonConvert.SerializeObject(parms), Encoding.UTF8, "application/json");
            barcode_request.DefaultRequestHeaders.Add("responseType", "arraybuffer");// 注意一定要加 不然返回的Buffer流会乱码 导致无法转base64
            var barcode_response = await barcode_request.PostAsync(barcode_request_uri, content);
            barcode_response.EnsureSuccessStatusCode();
            var barcode_responseByte = await barcode_response.Content.ReadAsByteArrayAsync();
            #endregion
            return barcode_responseByte;
        }

        public async Task<Dictionary<string, object>> MsgSecCheck(string content)
        {
            string access_token = await _cacheService.GetAccessTokenAsync("wx4d4ced5119f4d768");
            string uri = $"https://api.weixin.qq.com/wxa/msg_sec_check?access_token={access_token}";
            var json = new {content = content};
            var sContent = new StringContent(JsonConvert.SerializeObject (json), Encoding.UTF8, "application/json");
            var request = _httpClientFactory.CreateClient();
            var response = await request.PostAsync(uri, sContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
            return responseObject;
        }


        public async Task<object> CommentCheck(ContentCreationDto input)
        {
            string access_token = await _cacheService.GetAccessTokenAsync("wx4d4ced5119f4d768");
            string uri = $"https://api.weixin.qq.com/wxa/msg_sec_check?access_token={access_token}";
            var sContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var request = _httpClientFactory.CreateClient();
            var response = await request.PostAsync(uri, sContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<object>(responseJson);
            return responseObject;
        }

        public async Task<string> SendMsgByModelAsync(SendMsgCreationDto input)
        {
            input.TrimStringFields();
            string access_token = await _cacheService.GetAccessTokenAsync();
            string uri = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={access_token}";
            var json = new
            {
                touser = input.Touser,
                template_id = input.Template_id,
                miniprogram = input.MiniPro,
                data = input.Data
            };
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            var send_request = _httpClientFactory.CreateClient();
            var send_response = await send_request.PostAsync(uri, content);
            send_response.EnsureSuccessStatusCode();
            var send_responseJson = await send_response.Content.ReadAsStringAsync();
            var send_responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(send_responseJson);
            string result = send_responseObject["errmsg"].ToString();
            string error = send_responseObject["errcode"].ToString();
            if (error.Equals("40001"))
            {
                await _cacheService.GetAccessTokenAsync(true);
                await SendMsgByModelAsync(input);
            }
            return result;
        }

        public async Task<object> MediaCheckAsync(MediaCheckDto input)
        {
            input.TrimStringFields();
            string access_token = await _cacheService.GetAccessTokenAsync("wx4d4ced5119f4d768");
            string uri = $"https://api.weixin.qq.com/wxa/media_check_async?access_token={access_token}";
            var user = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input.uid);
            if(user == null)
                return new Dictionary<string, object>();
            var json = new { input.media_url, input.media_type, input.version, input.scene, openid = user.Openid};
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            var request = _httpClientFactory.CreateClient();
            var response = await request.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
            return responseObject;
        }
    }
}
