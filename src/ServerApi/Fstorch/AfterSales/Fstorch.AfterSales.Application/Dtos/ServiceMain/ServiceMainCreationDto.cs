
namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainCreationDto : ServiceMainCreationAndUpdationDto
    {

        /// <summary>
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary>
        /// 接单人员
        /// </summary>
        public string OrderTaker { get; set; } = string.Empty;

        /// <summary>
        /// 服务客户产品
        /// </summary>
        public List<ServiceProductCreationDto> Products { get; set; } = new List<ServiceProductCreationDto>();
    }
}
