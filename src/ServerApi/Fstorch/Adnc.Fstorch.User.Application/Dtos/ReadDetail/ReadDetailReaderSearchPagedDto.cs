using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadDetailReaderSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 发布者ID
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public long MsgId { get; set; }

        /// <summary>
        /// 读者ID
        /// </summary>
        public long Reader { get;set; }

        /// <summary>
        /// 是否查询今日 默认0 查询全部   为1查今日
        /// </summary>
        public int Today { get; set; } = 0;
    }
}
