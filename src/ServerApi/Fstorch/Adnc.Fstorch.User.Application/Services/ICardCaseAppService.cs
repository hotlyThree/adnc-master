
using Adnc.Fstorch.User.Application.Dtos.CardCase;

namespace Adnc.Fstorch.User.Application.Services
{
    public interface ICardCaseAppService : IAppService
    {
        /// <summary>
        /// 名片夹新增名片(自己的账号ID)、回递名片(传入对方的账号ID)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateAsync(CardCaseCreationDto input);

        /// <summary>
        /// 互换名片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> CreateBothAsync(List<CardCaseCreationDto> input);

        /// <summary>
        /// 更新名片夹好友类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateAsync(long id,CardCaseUpdationDto input);


        /// <summary>
        /// 名片访问量（持有人账号）
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.CardCaseCountKeyPrefix)]
        Task<int> CardCaseCountAsync([CachingParam] long aid);

        /// <summary>
        /// 转移名片夹
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult> TransferCardCaseAsync(CardCaseTransferDto input);

        /// <summary>
        /// 今日新增访问（持有人账号）
        /// </summary>
        /// <param name="aid">账号ID</param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.CardCaseCountTodayKeyPrefix)]
        Task<int> CardCaseCountTodayAsync([CachingParam] long aid);

        /// <summary>
        /// 名片访问分组明细（持有人账号所有名片的访问明细）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<CardCaseGroupDto>> CardCaseGroupAsync(CardCaseGroupSearchPagedDto input);

        /// <summary>
        /// 我的请求
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        Task<AppSrvResult<int>> TaskCountAsync(long  aid);


        /// <summary>
        /// 交换请求
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        Task<AppSrvResult<int>> ExchangeCountAsync(long aid);

        /// <summary>
        /// 删除名片夹名片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 获取名片夹名片分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<CardCaseDto>> GetPagedAsync(CardCaseSearchPaged input);


        /// <summary>
        /// 获取指定公司内部人员名片夹名片分页列表
        /// </summary>
        /// <returns></returns>
        Task<PageModelDto<CardCaseDto>> GetPagedByCompanyAsync();
    }
}
