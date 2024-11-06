namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderUpdationDto : ServiceOrderCreationAndUpdationDto
    {
        /// <summary>
        /// 服务客户工单ID
        /// </summary>
        public long Id { get; set; }
    }
}
