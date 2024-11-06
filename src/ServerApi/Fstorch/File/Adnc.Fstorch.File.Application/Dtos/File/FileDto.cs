namespace Adnc.Fstorch.File.Application.Dtos.File
{
    public class FileDto : OutputDto
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
        /// 写入时间
        /// </summary>
        public DateTime? Inserttime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 是否安全
        /// </summary>
        public int Security { get; set; }

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
    }
}
