using Adnc.Fstorch.File.Application.FileConfig;

namespace Adnc.Fstorch.File.Api.Controllers
{
    /// <summary>
    /// 名片系统文件管理
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.CardFileRoot}")]
    public class CardFileController : AdncControllerBase
    {
        private readonly ICardFileAppService _cardFileAppservice;

        public CardFileController(ICardFileAppService cardFileAppService)
        {
            _cardFileAppservice = cardFileAppService;
        }


        /// <summary>
        /// 上传文章封面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("changethumimg")]
        [AllowAnonymous]
        public async Task<ActionResult<string[]>> ChangeThumimg([FromForm]ThumingUpdationDto input)
            => Result(await _cardFileAppservice.ChangeThumimg(input));


        /// <summary>
        /// 创建解决方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("createsolution")]
        [AllowAnonymous]
        public async Task<ActionResult<string[]>> CreateSolutionAsync([FromForm]FileCreationDto input)
            => Result(await _cardFileAppservice.CreateSolutionAsync(input));


        /// <summary>
        /// 上传账号头像、微信视频号、抖音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("change/photo")]
        [AllowAnonymous]
        public  async Task<ActionResult<string>> UploadPhotoAsync([FromForm]PhotoUpdationDto input)
            => CreatedResult(await _cardFileAppservice.UploadPhotoAsync(input));


        /// <summary>
        /// 名片分享封面图(临时保存)
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost("shared")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> SharedImgAsync([FromForm][MaxFileSize(1024 * 1024 * 1)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" })]IFormFile File)
            => Result(await _cardFileAppservice.SharedImgAsync(File));
    }
}
