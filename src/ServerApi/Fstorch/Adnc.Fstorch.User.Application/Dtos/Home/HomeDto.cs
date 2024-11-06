using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Home
{
    public class HomeDto : OutputDto
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
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string MsgType { get; set; } = string.Empty;

        /// <summary>
        /// 消息类型名称
        /// </summary>
        public string MsgTypeLabel
        {
            get
            {
                string result;
                result = MsgType switch
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
        public string Thumimg { get; set; } = string.Empty;

        /// <summary>
        /// 发布人员id
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 发布人员姓名
        /// </summary>
        public string ReleaseName { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? Releasetime { get; set; }

        /// <summary>
        /// 发布人员头像
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
