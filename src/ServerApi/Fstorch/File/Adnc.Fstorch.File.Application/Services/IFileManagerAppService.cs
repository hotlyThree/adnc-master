using Newtonsoft.Json.Linq;

namespace Adnc.Fstorch.File.Application.Services
{
    /// <summary>
    /// 通用文件管理
    /// </summary>
    public interface IFileManagerAppService : IAppService
    {
        /// <summary>
        /// 通用上传公司、个人视频或图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> UpLoadPublicFileAsync(FilePublicCreationDto input);


        /// <summary>
        /// 上传视频封面图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AppSrvResult> UploadMediaThumimgAsync(long id, IFormFile file);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        Task<AppSrvResult> DeletePublicFileAsync(long id);

        /// <summary>
        /// 批量删除图片
        /// </summary>
        /// <param name="ids">文件ID列表</param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteRangePublicFileAsync(IEnumerable<long> ids);

        /// <summary>
        /// 查询文件分页
        /// </summary>
        /// <returns></returns>
        Task<PageModelDto<FileDto>> PagedAsync(FileSearchPagedDto search);

        /// <summary>
        /// 微信消息通知-请求校验（确认请求来自微信服务器）
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        Task<string> MediaCheckCallBackAsync(string signature, string timestamp, string nonce, string echostr);

        /// <summary>
        /// 微信消息通知-接收微信服务器推送信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> MediaCheckCallBackPostAsync(JObject input);
    }
}
