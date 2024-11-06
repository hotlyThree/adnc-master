namespace Fstorch.AfterSales.Application.Dtos.ServiceRequest
{
    public class ServiceRequestUpdationDto : ServiceRequestCreationAndUpdationDto
    {
        /// <summary>
        /// 服务需求ID
        /// </summary>
        public long Id { get; set; }
    }
}
