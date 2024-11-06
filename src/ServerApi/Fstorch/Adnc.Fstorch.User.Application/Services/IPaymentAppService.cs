using Adnc.Fstorch.User.Application.Dtos.CashOut;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Adnc.Fstorch.User.Application.Services
{
    /// <summary>
    /// 支付管理
    /// </summary>
    public interface IPaymentAppService : IAppService
    {
        /// <summary>
        /// 购买会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> PurchaseMembershipCard(PayDetailCreationDto input);

        /// <summary>
        /// 购买/续费会员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<object>> PurchaseMembership(PayDetailCreationDto input);

        /// <summary>
        /// 推荐码充值会员
        /// </summary>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult> PurchaseMembershipByReferralcode(long uid, string referralcode);

        /// <summary>
        /// 支付明细查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<PayDetailDto>> GetPagedAsync(PayDetailSearchPagedDto search);

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult<long>> WithdrawCashAsync(CashOutCreationDto input);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="head">请求头</param>
        /// <param name="body">请求体</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult<object>> PurchaseCallBackAsync(JObject head, JObject body, string sign);

        string GetSignTest(string body);
    }
}
