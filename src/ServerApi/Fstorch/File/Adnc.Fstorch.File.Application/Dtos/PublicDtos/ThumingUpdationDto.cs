namespace Adnc.Fstorch.File.Application.Dtos.PublicDtos
{
    public class ThumingUpdationDto : InputDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long id {  get; set; }

        /// <summary>
        /// 发布者ID
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 封面图路径
        /// </summary>
        public string? Thumimg { get; set; } = string.Empty;

        /// <summary>
        /// 缩略图
        /// </summary>
        [Required]
        [MaxFileSize(1024 * 1024 * 2)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" })]
        public IFormFile file { get; set; }
    }
}
