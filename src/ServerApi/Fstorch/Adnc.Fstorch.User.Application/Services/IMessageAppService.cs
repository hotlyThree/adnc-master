
using Adnc.Fstorch.User.Application.Dtos.ReadDetail;

namespace Adnc.Fstorch.User.Application.Services
{
    public interface IMessageAppService : IAppService
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateAsync(MessageCreationDto input);


        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateAsync(long id, MessageUpdationDto input);

        /// <summary>
        /// 更新文章缩略图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeThumimg(MessageThumimgUpdationDto input);

        /// <summary>
        /// 获取文章详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<MessageDto>> GetInfoAsync(ReadCreationDto input);

        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeDurationAsync(ReadUpdationDto input);

        /// <summary>
        /// 总阅读量（发布人所有创作）
        /// </summary>
        /// <param name="releaseid">发布者ID</param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ReadCountKeyPrefix)]
        Task<int> ReadCountByReleaseidAsync([CachingParam]long releaseid);

        /// <summary>
        /// 总阅读分组明细（发布人所有创作）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<ReadDetailDto>> ReadCountGroupByAsync(ReadDetailGroupSearchPagedDto input);

        /// <summary>
        /// 阅读人员明细（通用）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<ReadDetailReaderDto>> ReadDetailPagedAsync(ReadDetailReaderSearchPagedDto input);

        /// <summary>
        /// 今日总阅读量（发布人所有创作）
        /// </summary>
        /// <param name="releaseid">发布者ID</param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ReadCountTodayKeyPrefix)]
        Task<int> ReadCountTodaytReleaseidAsync([CachingParam] long releaseid);

        /// <summary>
        /// 获取阅读者列表(单文章汇总)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<ReadDetailDto>> GetPageAsync(ReadDetailSearchPagedDto input);


        /// <summary>
        /// 查询文章分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<MessageTitleDto>> GetPagedAsync(MessageSearchPagedDto input);
    }
}
