using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseDto : OutputDto
    {
        /// <summary>
        /// 账号ID
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
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Cname { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Createtime { get; set; }

        /// <summary>
        /// 好友类型 (A待确认 B已同意 C拒绝)
        /// </summary>
        public string? Addtype { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get
            {
                string result = "";
                if (!string.IsNullOrWhiteSpace(Addtype))
                {
                    result = Addtype switch
                    {
                        "A" => "待确认",
                        "B" => "已同意",
                        "C" => "拒绝",
                        _ => ""
                    };
                }
                return result;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creater { get; set; }
    }
}
