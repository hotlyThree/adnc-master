using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Comments
{
    [Serializable]
    public class CommentSimpleTreeListDto : CommentDto
    {
        /// <summary>
        /// 子评论（回复）
        /// </summary>
        public List<CommentSimpleTreeListDto> Children { get; set; }

    }
}
