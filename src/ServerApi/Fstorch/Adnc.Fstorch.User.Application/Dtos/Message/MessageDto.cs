using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Message
{
    public class MessageDto : OutputDto
    {
        /// <summary>
        /// 消息类型（A文章、B解决方案）
        /// </summary>
        public string Msgtype { get; set; }

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
        /// 主页展示（1是，0否）
        /// </summary>
        public int Ishome { get; set; }

        /// <summary>
        /// 是否主页展示
        /// </summary>
        public string IshomeName
        {
            get
            {
                var result = "";
                result = Ishome switch
                {
                    1 => "是",
                    0 => "否",
                    _ => "否"
                };
                return result;
            }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容（主页展示）
        /// </summary>
        public string? Memo { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? Releasetime { get; set; }

        /// <summary>
        /// 发布人员id
        /// </summary>
        public long Releaseid { get; set; }

        /// <summary>
        /// 发布人员姓名
        /// </summary>
        public string ReleaseName { get; set; }

        /// <summary>
        /// 发布人员头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 发布人员个性签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int Readnum { get; set; }

        /// <summary>
        /// 引用次数
        /// </summary>
        public int Quotenum { get; set; }

        /// <summary>
        /// 引用ID
        /// </summary>
        public long Quoteid { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string Cname { get; set; }

        /// <summary>
        /// 阅读ID
        /// </summary>
        public long Rid { get; set; }


        /// <summary>
        /// 文章评论列表
        /// </summary>
        public List<CommentSimpleTreeListDto> CommentSimpleTreeList { get; set; } = new List<CommentSimpleTreeListDto>();
    }
}
