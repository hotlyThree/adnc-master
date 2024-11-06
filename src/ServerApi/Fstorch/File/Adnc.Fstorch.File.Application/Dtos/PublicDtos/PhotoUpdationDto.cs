using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.File.Application.Dtos.PublicDtos
{
    public class PhotoUpdationDto : InputDto
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 上传类型 photo 头像     wechat 微信视频号二维码    tiktok 抖音二维码  默认photo 不传时默认上传头像
        /// </summary>
        public string Type { get; set; } = "photo";

        [MaxFileSize(1024 * 1024 * 5)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" })]
        public IFormFile File { get; set; }
    }
}
