

namespace Adnc.Fstorch.User.Application.Services
{
    /// <summary>
    /// 账号管理
    /// </summary>
    public interface IAccountAppService : IAppService
    {
        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult<long>> CreateAsync(AccountCreationDto input);

        /// <summary>
        /// 注销账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult> DeleteAsync([CachingParam]long id);

        /// <summary>
        /// 更新账号信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult> UpdateAsync([CachingParam]long id, AccountUpdationDto input);

        /// <summary>
        /// 更新账号头像、微信视频号、抖音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult<string>> ChangePhotoAsync(AccountPhotoUpdationDto input);

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        //[CachingAble(CacheKeyPrefix = CachingConsts.AccountInfoCacheKey)]
        Task<AppSrvResult<AccountDto>> GetAccountAsync(string openid);

        /// <summary>
        /// 获取账号信息列表
        /// </summary>
        /// <returns></returns>
        Task<AppSrvResult<List<AccountDto>>> GetAccountList();

        /// <summary>
        /// 获取openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<Dictionary<string, object>>> GetAccountOpenIdAsync(AccountOpenIdDto input);
    }
}
