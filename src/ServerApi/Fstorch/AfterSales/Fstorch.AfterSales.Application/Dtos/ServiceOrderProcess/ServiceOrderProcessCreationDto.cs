namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderProcess
{
    public class ServiceOrderProcessCreationDto : InputDto
    {
        /// <summary> 
        /// 服务工单ID
        /// </summary>
        public long ServiceInfoId { get; set; }


        /// <summary> 
        /// 是否回复
        /// </summary>
        public string IsReply { get; set; } = string.Empty;

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderId { get; set; }

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string? ServiceProcessId { get; set; }

        /// <summary> 
        /// 服务规范ID
        /// </summary>
        public string? ServiceSpecificationId { get; set; }

        /// <summary> 
        /// 过程序号
        /// </summary>
        public int? SerialNumber { get; set; }

        /// <summary>
        /// 过程类型，不传则默认为过程反馈
        /// </summary>
        public string? ProcessType { get; set; }

        /// <summary> 
        /// 过程描述
        /// </summary>
        public string ProcessReasons { get; set; }


        /// <summary> 
        /// 操作人员
        /// </summary>
        public string OperationPersonnel { get; set; }
    }
}
