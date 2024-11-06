using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Comments
{
    [Serializable]
    public class CommentDto : OutputDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 发布者ID
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long Reviewer { get; set; }

        /// <summary>
        /// 评论者账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 评论者微信昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 评论者头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 评论所属ID（回复）
        /// </summary>
        public long Parentid { get; set; }

        /*
         * 前端判断，因为要从缓存中取，由于每次读者不同，从缓存中取到Owner数据是写入缓存时就固定了的，存在数据不一致的情况
         * /// <summary>
        /// 是否为阅读者自己的评论或管理员
        /// </summary>
        public bool Owner { get;set; }*/

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime? Createtime { get; set; }

    }
}
