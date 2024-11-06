namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainCreationBatchDto : InputDto
    {
        /// <summary>
        /// 服务客户档案列表
        /// </summary>
        public List<ServiceMainCreationDto> ServiceMains { get; set; }
    }
}
