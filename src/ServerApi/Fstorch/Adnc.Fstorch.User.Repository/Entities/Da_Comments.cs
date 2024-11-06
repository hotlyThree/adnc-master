using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_Comments : EfEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long Reviewer { get;set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论所属ID（回复）
        /// </summary>
        public long Parentid { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime? Createtime { get; set; }
    }
}
