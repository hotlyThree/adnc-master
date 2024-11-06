using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadCreationDto : InputDto
    {


        /// <summary>
        /// 读者ID
        /// </summary>
        public long Reader { get;set; }

        /// <summary>
        /// 小程序APPID(用于通知)
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long Msgid { get;set; }

        /// <summary>
        /// 分享者ID（分享人员UID）
        /// </summary>
        public long Shareid { get; set; }

        /// <summary>
        /// 分享层级
        /// </summary>
        public int Sharelevel { get; set; }
    }
}
