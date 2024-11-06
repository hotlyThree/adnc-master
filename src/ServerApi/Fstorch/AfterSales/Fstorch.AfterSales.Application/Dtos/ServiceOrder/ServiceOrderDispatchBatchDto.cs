namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderDispatchBatchDto : InputDto
    {
        /// <summary>
        /// 客户ID列表
        /// </summary>
        public long[] Ids { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

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
    }
}
