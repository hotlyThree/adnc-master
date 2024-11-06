
namespace Adnc.Fstorch.User.Application.Services
{
    /// <summary>
    /// 会员卡管理
    /// </summary>
    public interface ICustomerAppService : IAppService
    {
        /// <summary>
        /// 创建会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateAsync(MemberTypeCreationDto input);

        /// <summary>
        /// 更新会员卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateAsync(MemberTypeUpdationDto input);

        /// <summary>
        /// 删除会员卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 查询会员卡分页列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<MemberTypeDto>> GetPagedAsync(MemberTypeSearchPagedDto search);

        /// <summary>
        /// 查询个人已购会员卡分页列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<GiveAwayDto>> GetOwnerPagedAsync(GiveAwaySearchPagedDto search);


        /// <summary>
        /// 根据推荐码获取有效会员卡信息
        /// </summary>
        /// <param name="referralcode"></param>
        /// <returns></returns>
        Task<AppSrvResult<GiveAwayDto>> GetGiveAwayInfo(string referralcode);

        /// <summary>
        ///创建卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> GivingPresentsAsync(GiveAwayCreationDto input);

        /// <summary>
        /// 使用卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKey = CachingConsts.AccountListCacheKey)]
        Task<AppSrvResult> UseCardAsync(GiveAwayUpdationDto input);
    }
}
