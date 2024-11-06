namespace Fstorch.AfterSales.Application.Dtos.ServiceProduct
{
    public class ServiceProductCreationDto : ServiceProductCreationAndUpdationDto
    {
        /// <summary>
        /// 服务流程
        /// </summary>
        public string ServiceProcess { get; set; } = string.Empty;

        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoID { get; set; }
    }
}
