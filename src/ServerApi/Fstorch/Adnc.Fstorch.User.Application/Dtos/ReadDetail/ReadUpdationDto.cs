using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadUpdationDto : InputDto
    {
        /// <summary>
        /// 阅读ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 阅读结束时间
        /// </summary>
        public DateTime? Endtime { get; set; }
    }
}
