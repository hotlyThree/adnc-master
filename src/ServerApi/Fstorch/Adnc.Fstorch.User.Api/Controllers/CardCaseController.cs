
namespace Adnc.Fstorch.User.Api.Controllers
{
    /// <summary>
    /// 名片夹管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.CardRoot}")]
    public class CardCaseController : AdncControllerBase
    {
        private ICardCaseAppService _cardCaseAppService;
        public CardCaseController(ICardCaseAppService cardCaseAppService)
        {
            _cardCaseAppService = cardCaseAppService;
        }

        /// <summary>
        /// 名片夹新增名片(自己的账号ID)、回递名片(传入对方的账号ID)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ins")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreateAsync([FromBody]CardCaseCreationDto input)
            => CreatedResult(await _cardCaseAppService.CreateAsync(input));

        /// <summary>
        /// 互换名片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ins/batch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<long[]>> CreateBothAsync([FromBody]List<CardCaseCreationDto> input)
            => CreatedResult(await _cardCaseAppService.CreateBothAsync(input));

        /// <summary>
        /// 我的请求
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [HttpGet("task/{aid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> TaskCountAsync([FromRoute]long aid)
            => Result(await _cardCaseAppService.TaskCountAsync(aid));


        /// <summary>
        /// 交换请求
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [HttpGet("exchange/{aid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> ExchangeCountAsync([FromRoute]long aid)
            => Result(await _cardCaseAppService.ExchangeCountAsync(aid));

        /// <summary>
        /// 更新名片夹好友类型
        /// </summary>
        /// <param name="id">当前名片夹id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("upd/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateAsync([FromRoute]long id, [FromBody]CardCaseUpdationDto input)
            => Result(await _cardCaseAppService.UpdateAsync(id, input));

        /// <summary>
        /// 删除名片夹名片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteAsync([FromRoute]long id)
            => Result(await _cardCaseAppService.DeleteAsync(id));

        /// <summary>
        /// 转移名片夹
        /// </summary>
        /// <returns></returns>
        [HttpPost("transfer")]
        [AllowAnonymous]
        public async Task<ActionResult> TransferCardCaseAsync([FromBody]CardCaseTransferDto input)
            => Result(await _cardCaseAppService.TransferCardCaseAsync(input));

        /// <summary>
        /// 获取名片夹名片分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<PageModelDto<CardCaseDto>> GetPagedAsync([FromQuery]CardCaseSearchPaged input)
            => await _cardCaseAppService.GetPagedAsync(input);

        /// <summary>
        /// 名片总访问量（持有人账号）
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [HttpGet("count/{aid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> CardCaseCountAsync([FromRoute]long aid)
            => await _cardCaseAppService.CardCaseCountAsync(aid);


        /// <summary>
        /// 今日新增访问（持有人账号）
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [HttpGet("count/today/{aid}")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> CardCaseCountTodayAsync([FromRoute]long aid)
            => await _cardCaseAppService.CardCaseCountTodayAsync(aid);


        /// <summary>
        /// 名片访问分组明细（持有人账号所有名片的访问明细）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page/group")]
        [AllowAnonymous]
        public async Task<PageModelDto<CardCaseGroupDto>> CardCaseGroupAsync([FromQuery]CardCaseGroupSearchPagedDto input)
            => await _cardCaseAppService.CardCaseGroupAsync(input);



    }
}
