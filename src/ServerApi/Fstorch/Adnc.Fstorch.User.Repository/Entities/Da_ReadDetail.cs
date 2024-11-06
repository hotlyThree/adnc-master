using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_ReadDetail : EfEntity
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 阅读者ID(当前阅读人员ID)
        /// </summary>
        public long Reader { get;set; }

        /// <summary>
        /// 阅读者微信昵称
        /// </summary>
        public string Readopenname { get; set; }

        /// <summary>
        /// 阅读者微信ID
        /// </summary>
        public string Readopenwxid { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? Readtime { get; set; }

        /// <summary>
        /// 阅读结束时间
        /// </summary>
        public DateTime? Endtime { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 分享者ID（分享人员UID）
        /// </summary>
        public long Shareid { get; set; }

        /// <summary>
        /// 分享层级
        /// </summary>
        public int Sharelevel { get; set; }

        /// <summary>
        /// 引用ID（原创消息ID）
        /// </summary>
        public long Quoteid { get; set; }

        /// <summary>
        /// 消息通知ID（发布微信通知后写入）
        /// </summary>
        public string? Notifyid { get; set; } = string.Empty;
    }
}
