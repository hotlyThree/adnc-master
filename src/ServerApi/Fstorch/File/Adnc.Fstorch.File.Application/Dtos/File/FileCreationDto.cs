namespace Adnc.Fstorch.File.Application.Dtos.File
{
    public class FileCreationDto : InputDto
    {
        /// <summary>
        /// 文件类型（解决方案、图片、视频...）
        /// </summary>
        public string Filetype { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string Filememo { get; set; } = string.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// 企业ID
        /// </summary>
        public string Cid { get; set; } = string.Empty;

        /// <summary>
        /// 业务单号
        /// </summary>
        public string Orderid { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 文件
        /// </summary>
        [AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile File { get; set; }
    }
}
