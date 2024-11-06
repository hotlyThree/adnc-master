using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Home
{
    public class HomeCreationDto : InputDto
    {
        /// <summary>
        /// 账号ID或名片ID
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 文章ID或解决方案ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 排序  默认0
        /// </summary>
        public int Sort { get; set; } = 0;
    }
}
