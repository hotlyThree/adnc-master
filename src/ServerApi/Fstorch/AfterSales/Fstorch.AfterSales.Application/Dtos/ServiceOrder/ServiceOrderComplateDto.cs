

namespace Fstorch.AfterSales.Application.Dtos.ServiceOrder
{
    public class ServiceOrderComplateDto : InputDto
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 服务规范ID
        /// </summary>
        public string ServiceSpecificationId { get; set; } = string.Empty;

        /// <summary> 
        /// 服务措施
        /// </summary>
        public string ServiceMeasures { get; set; } = string.Empty;

        /// <summary> 
        /// 服务结果
        /// </summary>
        public string ServiceResult { get; set; } = string.Empty;

        /// <summary> 
        /// 公司完工人员
        /// </summary>
        public long CompanyEndPersonnel { get; set; }

        /// <summary>
        /// 应付信息
        /// </summary>
        public ServiceOrderCopeCreationDto Cope { get; set; }

        /// <summary>
        /// 应收信息
        /// </summary>
        public ServiceOrderRecCreationDto Rec { get; set; }
    }
}
