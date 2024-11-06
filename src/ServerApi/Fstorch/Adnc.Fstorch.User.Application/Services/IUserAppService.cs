
namespace Adnc.Fstorch.User.Application.Services
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public interface IUserAppService : IAppService
    {
        /// <summary>
        /// 创建名片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "新增名片")]
        Task<AppSrvResult<long>> CreateAsync(UserCreation input);

        /// <summary>
        /// 修改名片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改名片")]
        Task<AppSrvResult> UpdateAsync(long id, UserUpdation input);

        /// <summary>
        /// 删除卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "删除名片")]
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 更新名片主页展示信息
        /// </summary>
        /// <param name="uid">名片ID</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeHomeInfo(long uid, HomeCreationAndUpdationDto input);


        /// <summary>
        /// 查询账号文章收藏列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<HomeDto>> HomePagedAsync(HomeSearchPagedDto input);

        /// <summary>
        /// 查询账号收藏ID列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<AppSrvResult<long[]>> FavoritesListAsync(long uid);

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="input">收藏内容</param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreatedFavoritesAsync(HomeCreationDto input);

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteFavoritesAsync(long uid, long mid);

        /// <summary>
        /// 修改名片状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改名片状态")]
        Task<AppSrvResult> ChangeStatusAsync(long id, string status);

        /// <summary>
        /// 批量修改名片状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperateLog(LogName = "批量修改名片状态")]
        Task<AppSrvResult> ChangeStatusAsync(IEnumerable<long> ids, string status);

        /// <summary>
        /// 获取名片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult<UserDto>> GetUserInfoAsync (long id);

        /// <summary>
        /// 获取名片分页列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search);

        /// <summary>
        /// 获取样式列表
        /// </summary>
        /// <returns></returns>
        [CachingAble(CacheKey = CachingConsts.StyleListCacheKey)]
        Task<List<StyleDto>> GetStyleAsync();


        /// <summary>
        /// 获取背景图列表
        /// </summary>
        /// <returns></returns>
        [CachingAble(CacheKey = CachingConsts.BackGroundListCacheKey)]
        Task<List<BackGroundDto>> GetBackGroundAsync();
    }
}
