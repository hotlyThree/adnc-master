using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.User
{
    public class UserUpdation : UserCreationAndUpdationDto
    {


        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// 关于
        /// </summary>
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 公司ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 样式ID
        /// </summary>
        public int Styleid { get; set; }

        /// <summary>
        /// 背景ID
        /// </summary>
        public int Bgid { get; set; }
    }
}
