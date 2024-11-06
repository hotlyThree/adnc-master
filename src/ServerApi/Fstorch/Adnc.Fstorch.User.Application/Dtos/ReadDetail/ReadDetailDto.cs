using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadDetailDto : OutputDto
    {
        /// <summary>
        /// 阅读者ID(当前阅读人员ID)
        /// </summary>
        public long Reader { get; set; }

        /// <summary>
        /// 阅读者姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 阅读者头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 微信视频号
        /// </summary>
        public string WeChatVideo { get; set; }

        /// <summary>
        /// 阅读者微信昵称
        /// </summary>
        public string Readopenname { get; set; } = string.Empty;

        /// <summary>
        /// 阅读者微信ID
        /// </summary>
        public string Readopenwxid { get; set; } = string.Empty;

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 阅读时长
        /// </summary>
        public int Duration { get; set; }
    }
}
