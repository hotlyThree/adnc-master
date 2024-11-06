namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderReassignmentDto : InputDto
    {
        /// <summary>
        /// 服务工单ID
        /// </summary>
        public long Id { get; set; }


        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 公司派工人员
        /// </summary>
        public long CompanyDispatchPersonnel { get; set; }

        /// <summary> 
        /// 服务商派工人员
        /// </summary>
        public long? ProviderDispatchPersonnel { get; set; }

        /// <summary>
        /// 服务规范ID
        /// </summary>
        public string? ServiceSpecificationId { get; set; } = string.Empty;

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string ServiceprocessID { get; set; }

        /// <summary> 
        /// 服务人员ID
        /// </summary>
        public string ServicestaffID { get; set; }

        /// <summary> 
        /// 服务人员姓名
        /// </summary>
        public string ServicestaffName { get; set; }
    }
}
