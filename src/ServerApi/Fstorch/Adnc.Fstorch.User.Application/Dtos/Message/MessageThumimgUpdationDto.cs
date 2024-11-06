using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageThumimgUpdationDto : InputDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
