using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseGroupDto : OutputDto
    {

        public new long Id = new long();

        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 账号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 是否已包含名片
        /// </summary>
        public bool InClude { get; set; }
    }
}
