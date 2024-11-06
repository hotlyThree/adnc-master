namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string? SqlWhere { get; set; }

        /// <summary>
        /// 排序规则
        /// </summary>
        public string? OrderBy { get; set; }

        /// <summary>
        /// 是否展示工人提成是否存在异议
        /// </summary>
        public int? IsShowEngineerAcceptResult { get; set; }
    }
}
