namespace Adnc.Fstorch.User.Application.Services
{
    /// <summary>
    /// 评论管理
    /// </summary>
    public interface ICommentAppService : IAppService
    {
        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="msgid"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.CommentsTreeListKeyPrefix)]
        public Task<AppSrvResult<long>> CreateAsync([CachingParam]long msgid, CommentCreationDto input);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="msgid">文章ID</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.CommentsTreeListKeyPrefix)]
        public Task<AppSrvResult> DeleteAsync([CachingParam]long msgid, long id);

        /// <summary>
        /// 查询评论列表
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="msgid"></param>
        /// <returns></returns>
        [OperateLog(LogName = "查询文章评论列表")]
        [CachingAble(CacheKeyPrefix = CachingConsts.CommentsTreeListKeyPrefix, Expiration = GeneralConsts.OneHour)]
        public Task<List<CommentSimpleTreeListDto>> GetListAsync(long reader, [CachingParam]long msgid);
    }

}
