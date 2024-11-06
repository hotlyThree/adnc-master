namespace Fstorch.AfterSales.Application.Dtos.Public
{
    public class CountReportDto : InputDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Companyid { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 品牌内容
        /// </summary>
        public string BrandContent { get; set; } = string.Empty;

        /// <summary>
        /// 是否分组 0 否 1是
        /// </summary>
        public int IsGroupBy { get; set; }

        /// <summary>
        /// 工程师ID
        /// </summary>
        public string? ServiceStaffId { get; set; }

        /// <summary>
        /// 预约起日
        /// </summary>
        public DateTime? AppointmentDateS { get; set; }

        /// <summary>
        /// 预约止日
        /// </summary>
        public DateTime? AppointmentDateE { get; set; }
    }
}
