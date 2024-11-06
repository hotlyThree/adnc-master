namespace Adnc.Fstorch.File.Application.Dtos.File
{
    public class FileSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// 企业ID
        /// </summary>
        public string Cid { get; set; } = string.Empty;

        /// <summary>
        /// 上传时间
        /// </summary>
        public string Inserttime { get; set; } = string.Empty;

        /// <summary>
        /// 文件描述
        /// </summary>
        public string Filememo { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Filetype { get; set; } = string.Empty;

        /// <summary>
        /// 业务单号
        /// </summary>
        public string Orderid { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
        /// </summary>
        public string Ordertype { get; set; } = string.Empty;
    }
}
