using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Account
{
    public class AccountPhotoUpdationDto : InputDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 类型 photo 头像     wechat 微信视频号二维码    tiktok 抖音二维码  默认photo 不传时默认上传头像
        /// </summary>
        public string Type { get; set; } = "photo";

        /// <summary>
        /// 用户头像、微信视频号、抖音视频号
        /// </summary>
        public string Photo { get; set; } = string.Empty;
    }
}
