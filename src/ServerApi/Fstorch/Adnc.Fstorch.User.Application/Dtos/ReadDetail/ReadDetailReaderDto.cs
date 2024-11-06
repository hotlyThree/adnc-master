using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.ReadDetail
{
    public class ReadDetailReaderDto : OutputDto
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long Msgid { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MsgTitle { get; set; }


        /// <summary>
        /// 文章类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string Label
        {
            get
            {
                string result = "";
                result = MsgType switch
                {
                    "A" => "文章",
                    "B" => "解决方案",
                    _ => "其他"
                };
                return result;
            }
        }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? Readtime { get; set; }

        /// <summary>
        /// 阅读结束时间
        /// </summary>
        public DateTime? Endtime { get; set; }

        /// <summary>
        /// 阅读时长
        /// </summary>
        public int Duration { get; set; }
    }
}
