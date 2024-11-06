using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadDetailSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long id { get;set; }
    }
}
