using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_msg : EfEntity
    {
        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string? Thumimg { get; set; } = string.Empty;

        /// <summary>
        /// 主页展示（1是，0否）
        /// </summary>
        public int Ishome { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容（主页展示）
        /// </summary>
        public string? Memo { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Releasetime { get; set; }

        /// <summary>
        /// 发布人员id
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int Readnum { get; set; }

        /// <summary>
        /// 引用次数
        /// </summary>
        public int Quotenum { get; set; }

        /// <summary>
        /// 引用ID
        /// </summary>
        public long Quoteid { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public long Cid { get; set; }
    }
}
