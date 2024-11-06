namespace Fstorch.AfterSales.Application.Dtos.ServiceCause
{
    public class ServiceCauseUpdationDto : ServiceCauseCreationAndUpdationDto
    {
        /// <summary>
        /// 故障原因ID
        /// </summary>
        public long Id { get; set; }
    }
}
