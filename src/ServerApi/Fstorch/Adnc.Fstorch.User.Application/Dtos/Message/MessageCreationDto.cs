using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageCreationDto : MessageCreationAndUpdationDto
    {
        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        /// 发布人员id
        /// </summary>
        public long Releaseid { get; set; }

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
