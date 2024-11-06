namespace Fstorch.AfterSales.Application.Dtos.ServiceNature
{
    public class ServiceNatureCreationAndUpdationDto : InputDto
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }


        /// <summary> 
        /// 服务性质名称
        /// </summary>
        public string ServiceNatureName { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int DisplayNumber { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
