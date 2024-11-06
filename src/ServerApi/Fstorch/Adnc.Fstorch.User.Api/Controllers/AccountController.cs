

namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 账户管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.AuthRoot}")]
    public class AccountController : AdncControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ins")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> CreateAsync([FromBody] AccountCreationDto input)
            => CreatedResult(await _accountAppService.CreateAsync(input));

        /// <summary>
        /// 注销账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<AppSrvResult>> DeleteAsync([FromRoute]long id)
            => Result(await _accountAppService.DeleteAsync(id));


        /// <summary>
        /// 更新账号基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<AppSrvResult>> UpdateAsync([FromRoute]long id, [FromBody]AccountUpdationDto input)
            => Result(await _accountAppService.UpdateAsync(id, input));


        /// <summary>
        /// 更新账号头像、微信视频号、抖音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("change/photo")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> ChangePhotoAsync(AccountPhotoUpdationDto input)
            => Result(await _accountAppService.ChangePhotoAsync(input));


        /// <summary>
        /// 获取账号基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("get/{openid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AccountDto>> GetAccountAsync([FromRoute]string openid)
            => Result(await _accountAppService.GetAccountAsync(openid));

        /// <summary>
        /// 获取账号信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("account/list")]
        [AllowAnonymous]
        public async Task<ActionResult<List<AccountDto>>> GetAccountList()
            => Result(await _accountAppService.GetAccountList());


        /// <summary>
        /// 获取openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get/openid")]
        [AllowAnonymous]
        public async Task<ActionResult<Dictionary<string, object>>> GetAccountOpenIdAsync([FromQuery]AccountOpenIdDto input)
            => Result(await _accountAppService.GetAccountOpenIdAsync(input));
    }
}
