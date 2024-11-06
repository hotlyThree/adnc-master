namespace Fstorch.AfterSales.Application.Dtos.Public
{
    public class GeneratePayrollDto : InputDto
    {
        /// <summary>
        /// 工单ID列表
        /// </summary>
        public IEnumerable<long> Ids { get; set; }

        /// <summary>
        /// 设置月份
        /// </summary>
        public string SetMonth { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public long Operator { get;set; }
    }
}
