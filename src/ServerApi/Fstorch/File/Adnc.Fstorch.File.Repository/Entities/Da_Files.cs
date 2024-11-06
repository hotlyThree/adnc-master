

namespace Adnc.Fstorch.File.Repository.Entities
{
    public class Da_Files : EfEntity
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Filepath { get; set; }

        /// <summary>
        /// 媒体文件封面图
        /// </summary>
        public string MediaPath { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型（解决方案、图片、视频...）
        /// </summary>
        public string Filetype { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string Filememo { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Filesize { get; set; }

        /// <summary>
        /// 是否合规（图片文件安全检测）
        /// </summary>
        public int Security { get; set; } = 0;

        /// <summary>
        /// 存储类型（A自建B阿里C华为D西部数码）
        /// </summary>
        public string Storagetype { get; set; } = "A";

        /// <summary>
        /// 写入时间
        /// </summary>
        public DateTime? Inserttime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// 企业ID
        /// </summary>
        public string Cid { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 业务单号
        /// </summary>
        public string Orderid { get; set; } = string.Empty;

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; } = string.Empty;
    }
}
