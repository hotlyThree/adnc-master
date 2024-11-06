namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderCreationDto : ServiceOrderCreationAndUpdationDto
    {
        /// <summary> 
        /// 服务客户档案ID
        /// </summary>
        public long ServiceInfoID { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary>
        /// 服务客户产品序号列表
        /// </summary>
        public IEnumerable<int> ProductSerial { get; set; }
/*
        /// <summary> 
        /// 服务工单号
        /// </summary>
        public long ServiceOrderID { get; set; }

*/
        /// <summary>
        /// 服务规范ID
        /// </summary>
        public string ServiceSpecificationId { get; set; }

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string ServiceprocessID { get; set; }

        /// <summary> 
        /// 服务商家ID
        /// </summary>
        public long ServiceProviderID { get; set; }

        /// <summary> 
        /// 服务人员ID
        /// </summary>
        public string ServicestaffID { get; set; }

        /// <summary> 
        /// 服务人员姓名
        /// </summary>
        public string ServicestaffName { get; set; }

        /// <summary> 
        /// 公司派工人员
        /// </summary>
        public long CompanyDispatchPersonnel { get; set; }

        /// <summary> 
        /// 服务商派工人员
        /// </summary>
        public long ProviderDispatchPersonnel { get; set; }

        /// <summary> 
        /// 服务请求
        /// </summary>
        public string ServiceRequest { get; set; }

        /// <summary>
        /// 产品明细
        /// </summary>
        public string Details { get; set; } = string.Empty;
    }
}
