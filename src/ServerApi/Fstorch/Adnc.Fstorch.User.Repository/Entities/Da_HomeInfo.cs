using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_HomeInfo : EfEntity
    {
        /// <summary>
        /// 名片ID或账号ID
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
