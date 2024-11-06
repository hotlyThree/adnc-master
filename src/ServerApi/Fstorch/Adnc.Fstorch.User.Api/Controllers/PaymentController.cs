using Adnc.Fstorch.User.Application.Dtos.CashOut;

namespace Adnc.Fstorch.User.Api.Controllers
{
    [ApiController]
    [Route($"{RouteConsts.PaymentRoot}")]
    public class PaymentController : AdncControllerBase
    {
        private readonly IPaymentAppService _paymentAppService;
        public PaymentController(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }
        /// <summary>
        /// 购买会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("membershipcard")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> PurchaseMembershipCard([FromBody] PayDetailCreationDto input)
            => Result(await _paymentAppService.PurchaseMembershipCard(input));

        /// <summary>
        /// 购买/续费会员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("membership")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> PurchaseMembership([FromBody]PayDetailCreationDto input)
            => Result(await _paymentAppService.PurchaseMembership(input));

        /// <summary>
        /// 推荐码充值会员
        /// </summary>
        /// <returns></returns>
        [HttpPut("referralcode/purchase")]
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseMembershipByReferralcode([FromQuery]long uid, [FromQuery]string referralcode)
            => Result(await _paymentAppService.PurchaseMembershipByReferralcode(uid, referralcode));


        /// <summary>
        /// 支付明细查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("page")]
        [AllowAnonymous]
        public async Task<PageModelDto<PayDetailDto>> GetPagedAsync([FromQuery]PayDetailSearchPagedDto search)
            => await _paymentAppService.GetPagedAsync(search);

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        [HttpPost("withdraw/cash")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> WithdrawCashAsync([FromBody]CashOutCreationDto input)
            => CreatedResult(await _paymentAppService.WithdrawCashAsync(input));


        [HttpGet("sign")]
        [AllowAnonymous]
        public ActionResult<string> GetSignTest([FromQuery] string body)
            => _paymentAppService.GetSignTest(body);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("purchase/callback")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> PurchaseCallBackAsync()
        {
            var request = HttpContext.Request;
            string result = "";
            StreamReader streamReader = new StreamReader(request.Body, Encoding.UTF8);
            result = await streamReader.ReadToEndAsync();
            if(result.IsNullOrWhiteSpace())
                return BadRequest("参数无效");
            JObject job = (JObject)JsonConvert.DeserializeObject(result);
            JObject job_request = (JObject)job["request"];
            JObject job_head = (JObject)job_request["head"];
            JObject job_body = (JObject)job_request["body"];
            return await _paymentAppService.PurchaseCallBackAsync(job_head, job_body, job["signature"].ToString());
        }
    }
}
