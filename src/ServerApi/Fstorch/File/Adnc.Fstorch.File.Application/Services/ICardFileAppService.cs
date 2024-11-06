
namespace Adnc.Fstorch.File.Application.Services
{
    /// <summary>
    /// 名片系统文件管理
    /// </summary>
    public interface ICardFileAppService : IAppService
    {
        /// <summary>
        /// 更换文章封面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<string[]>> ChangeThumimg(ThumingUpdationDto input);

        /// <summary>
        /// 创建解决方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<string[]>> CreateSolutionAsync(FileCreationDto input);

        /// <summary>
        /// 上传头像、微信视频号二维码、抖音二维码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<string>> UploadPhotoAsync(PhotoUpdationDto input);

        /// <summary>
        /// 名片分享封面图(临时保存)
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        Task<AppSrvResult<string>> SharedImgAsync(IFormFile File);


    }
}
