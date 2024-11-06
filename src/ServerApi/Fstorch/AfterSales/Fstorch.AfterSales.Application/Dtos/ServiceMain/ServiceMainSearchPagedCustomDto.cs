namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainSearchPagedCustomDto : SearchPagedDto
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
    }
}
