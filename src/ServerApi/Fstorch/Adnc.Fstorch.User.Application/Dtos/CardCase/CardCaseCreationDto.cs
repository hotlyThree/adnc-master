using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseCreationDto : CardCaseCreationAndUpdationDto
    {
        /// <summary>
        /// 账号ID(正常添加名片时，账号ID是自己的，回递名片时，账号ID是对方的)
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 名片ID
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 交换名片ID（个人）
        /// </summary>
        public long CardExchangeId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creater { get; set; }
    }
}
