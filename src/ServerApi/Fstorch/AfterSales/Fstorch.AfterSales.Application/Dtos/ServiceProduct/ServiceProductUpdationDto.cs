namespace Fstorch.AfterSales.Application.Dtos.ServiceProduct
{
    public class ServiceProductUpdationDto : ServiceProductCreationAndUpdationDto
    {
        /// <summary>
        /// 服务产品ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 原流程
        /// </summary>
        public string? ServiceProcessIds { get;set; }
    }
}
