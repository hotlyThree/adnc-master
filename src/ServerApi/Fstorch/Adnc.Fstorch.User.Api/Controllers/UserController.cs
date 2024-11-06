

using Newtonsoft.Json;
using Polly.Caching;

namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 名片管理
    /// </summary>
    [Route($"{RouteConsts.UserRoot}")]
    [ApiController]
    public class UserController : AdncControllerBase
    {

        private readonly IUserAppService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IUserAppService userService, IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 新增名片
        /// </summary>
        /// <param name="input">用户信息</param>
        /// <returns></returns>
        [HttpPost("ins")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> CreateAsync([FromBody] UserCreation input) 
            => CreatedResult(await _userService.CreateAsync(input));

        /// <summary>
        /// 修改名片
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="input">用户信息</param>
        /// <returns></returns>
        [HttpPut("upd/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserUpdation input)
            => Result(await _userService.UpdateAsync(id, input));

        /// <summary>
        /// 删除名片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAsync([FromRoute]long id)
            => Result(await _userService.DeleteAsync(id));


        /// <summary>
        /// 更改名片状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{id}/status")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ChangeStatusAsync([FromRoute] long id, [FromBody] SimpleDto<string> status)
            => Result(await _userService.ChangeStatusAsync(id, status.Value));


        /// <summary>
        /// 批量更改名片状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("batch/status")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ChangeStatusAsync([FromBody] UserChangeStatusDto input)
            => Result(await _userService.ChangeStatusAsync(input.Ids, input.Status));


        /// <summary>
        /// 获取名片信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("get/{Id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetUserInfoAsync([FromRoute]long Id)
            => Result(await _userService.GetUserInfoAsync(Id));


        /// <summary>
        /// 获取名片分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PageModelDto<UserDto>> GetPagedAsync([FromQuery] UserSearchPagedDto input)
            => await _userService.GetPagedAsync(input);


        /// <summary>
        /// 更新名片主页展示信息
        /// </summary>
        /// <param name="uid">名片ID或账号ID</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("change/home/{uid}")]
        [AllowAnonymous]
        public async Task<ActionResult> ChangeHomeInfo([FromRoute]long uid, [FromBody]HomeCreationAndUpdationDto input)
            => Result(await _userService.ChangeHomeInfo(uid, input));



        /// <summary>
        /// 查询账号文章收藏列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("home/page")]
        [AllowAnonymous]
        public async Task<PageModelDto<HomeDto>> HomePagedAsync([FromQuery] HomeSearchPagedDto input)
            => await _userService.HomePagedAsync(input);


        /// <summary>
        /// 查询账号收藏ID列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet("home/list/{uid}")]
        [AllowAnonymous]
        public async Task<ActionResult<long[]>> FavoritesListAsync([FromRoute]long uid)
            => CreatedResult(await _userService.FavoritesListAsync(uid));

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="input">收藏内容</param>
        /// <returns></returns>
        [HttpPost("home/favorites")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreatedFavoritesAsync([FromBody]HomeCreationDto input)
            => Result(await _userService.CreatedFavoritesAsync(input));

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpDelete("home/cancel/{uid}/{mid}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteFavoritesAsync([FromRoute]long uid, [FromRoute]long mid)
            => Result(await _userService.DeleteFavoritesAsync(uid, mid));



        /// <summary>
        /// 获取样式列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("style")]
        [AllowAnonymous]
        public async Task<ActionResult<List<StyleDto>>> GetStyleAsync()
            => await _userService.GetStyleAsync();


        /// <summary>
        /// 获取背景列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("back")]
        [AllowAnonymous]
        public async Task<ActionResult<List<BackGroundDto>>> GetBackGroundAsync()
            => await _userService.GetBackGroundAsync();


        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetToken()
        {
            using var request = _httpClientFactory.CreateClient();
            string token_request_uri = $"https://wx.fstorch.com/classservice.asmx/weixin_Refresh_Token";
            //var parm = new { wxid = "36" };
            var dic = new Dictionary<string, string>
            {
                {"wxid", "36" }
            };
            var formdata = new FormUrlEncodedContent(dic);
            //var content = new StringContent(JsonConvert.SerializeObject(parm));
            var token_response = await request.PostAsync(token_request_uri, formdata);
            token_response.EnsureSuccessStatusCode();
            var token_responseString = await token_response.Content.ReadAsStringAsync();
            string token = "";
            TimeSpan total = new();
            if(token_responseString.IsNotNullOrWhiteSpace() && token_responseString.Contains("success"))
            {
                var arr = token_responseString.Split("|");
                var createtime = DateTime.Parse(arr[1]);
                token = arr[2].Replace("</string>","");
                total = DateTime.Now - createtime;
            }
            return token +">>>" +total.TotalMinutes;
        }
    }
}
