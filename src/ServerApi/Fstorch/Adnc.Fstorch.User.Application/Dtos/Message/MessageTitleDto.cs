using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageTitleDto : OutputDto
    {
        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string Msgtype { get; set; }


        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        public string MsgtypeName
        {
            get
            {
                var result = "";
                result = Msgtype switch
                {
                    "A" => "文章",
                    "B" => "解决方案",
                    _ => ""
                };
                return result;
            }
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string? Thumimg { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? Releasetime { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int Readnum { get; set; }

        /// <summary>
        /// 发布人员id
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 发布人员姓名
        /// </summary>
        public string ReleaseName { get; set; }


        /// <summary>
        /// 企业ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string Cname { get; set; }
    }
}
