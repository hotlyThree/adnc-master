using Adnc.Fstorch.File.Application.FileConfig;
using Adnc.Shared.Application.Contracts.Dtos;
using Adnc.Shared.Application.Contracts.ResultModels;
using Newtonsoft.Json.Linq;

namespace Adnc.Fstorch.File.Api.Controllers
{
    /// <summary>
    /// 通用文件管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.CardFileRoot}")]
    public class FileManagerController : AdncControllerBase
    {
        private readonly IFileManagerAppService _fileManagerAppService;
        public FileManagerController(IFileManagerAppService fileManagerAppService)
        {
            _fileManagerAppService = fileManagerAppService;
        }

        /// <summary>
        /// 通用上传公司、个人视频或图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> UpLoadPublicFileAsync([FromForm]FilePublicCreationDto input)
            => Result(await _fileManagerAppService.UpLoadPublicFileAsync(input));

        /// <summary>
        /// 上传视频封面图
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <param name="file">封面图</param>
        /// <returns></returns>
        [HttpPost("upload/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadMediaThumimgAsync([FromRoute]long id, [MaxFileSize(1024 * 1024 * 2)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" })][FromForm]IFormFile file)
            => Result(await _fileManagerAppService.UploadMediaThumimgAsync(id, file));

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeletePublicFileAsync([FromRoute]long id)
            => Result(await _fileManagerAppService.DeletePublicFileAsync(id));

        /// <summary>
        /// 批量删除图片
        /// </summary>
        /// <param name="ids">文件ID列表</param>
        /// <returns></returns>
        [HttpDelete("delete/range")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteRangePublicFileAsync([FromBody] IEnumerable<long> ids)
            => Result(await _fileManagerAppService.DeleteRangePublicFileAsync(ids));

        /// <summary>
        /// 查询企业、个人文件分页
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        [AllowAnonymous]
        public async Task<PageModelDto<FileDto>> PagedAsync([FromQuery]FileSearchPagedDto search)
            => await _fileManagerAppService.PagedAsync(search);


        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param
        /// <returns></returns>
        [HttpGet("media/check/callback")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> MediaCheckCallBackAsync([FromQuery]string signature, [FromQuery]string timestamp, [FromQuery] string nonce, [FromQuery] string echostr)
            => await _fileManagerAppService.MediaCheckCallBackAsync(signature, timestamp, nonce, echostr);

        /// <summary>
        /// 接收微信服务器推送信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("media/check/callback")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> MediaCheckCallBackPostAsync([FromBody]JObject input)
            => await _fileManagerAppService.MediaCheckCallBackPostAsync(input);
    }
}
