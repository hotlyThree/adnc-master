namespace Adnc.Fstorch.File.Application.Dtos.File
{
    public class FilePublicCreationDto : InputDto
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// 企业ID
        /// </summary>
        public string Cid { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 业务单号
        /// </summary>
        public string Orderid { get; set; } = string.Empty;

        /// <summary>
        /// 文件描述
        /// </summary>
        public string Filememo { get; set; } = string.Empty;


        /// <summary>
        /// 文件类型（解决方案、图片、视频...）
        /// </summary>
        public string Filetype { get; set; } = string.Empty;

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; } = string.Empty;

        /// <summary>
        /// 图片或视频
        /// </summary>
        [AllowedExtensionsAndFileSize(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".mp4", ".mov", ".wmv"}, 1024 * 1024 * 2 , 1024 * 1024 * 100)]
        public IFormFile File { get; set; }
    }
}
