namespace Adnc.Fstorch.User.Api.Controllers
{
    [ApiController]
    [Route($"{RouteConsts.CustomerRoot}")]
    public class CustomerController : AdncControllerBase
    {
        private readonly ICustomerAppService _customerAppService;
        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }
        /// <summary>
        /// 创建会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreateAsync([FromBody]MemberTypeCreationDto input)
            =>CreatedResult(await _customerAppService.CreateAsync(input));

        /// <summary>
        /// 更新会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromBody]MemberTypeUpdationDto input)
            => Result(await _customerAppService.UpdateAsync(input));

        /// <summary>
        /// 删除会员卡
        /// </summary>
        /// <param name="id">会员卡ID</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteAsync([FromRoute]long id)
            =>Result(await _customerAppService.DeleteAsync(id));

        /// <summary>
        /// 查询会员卡分页列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("page")]
        [AllowAnonymous]
        public async Task<PageModelDto<MemberTypeDto>> GetPagedAsync([FromQuery]MemberTypeSearchPagedDto search)
            => await _customerAppService.GetPagedAsync(search);


        /// <summary>
        /// 查询个人已购会员卡分页列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("owner/page")]
        [AllowAnonymous]
        public async Task<PageModelDto<GiveAwayDto>> GetOwnerPagedAsync([FromQuery] GiveAwaySearchPagedDto search)
            => await _customerAppService.GetOwnerPagedAsync(search);


        /// <summary>
        /// 根据推荐码获取有效会员卡信息
        /// </summary>
        /// <param name="referralcode">推荐码</param>
        /// <returns></returns>
        [HttpGet("info")]
        [AllowAnonymous]
        public async Task<ActionResult<GiveAwayDto>> GetGiveAwayInfo([FromQuery]string referralcode)
            => Result(await _customerAppService.GetGiveAwayInfo(referralcode));

        /// <summary>
        ///创建卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("giving")]
        [AllowAnonymous]
        //[NonAction]
        public async Task<ActionResult<long>> GivingPresentsAsync([FromBody]GiveAwayCreationDto input)
            => CreatedResult(await _customerAppService.GivingPresentsAsync(input));

        /// <summary>
        /// 使用卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("use")]
        [AllowAnonymous]
        public async Task<ActionResult> UseCardAsync([FromBody]GiveAwayUpdationDto input)
            => Result(await _customerAppService.UseCardAsync(input));
    }
}
