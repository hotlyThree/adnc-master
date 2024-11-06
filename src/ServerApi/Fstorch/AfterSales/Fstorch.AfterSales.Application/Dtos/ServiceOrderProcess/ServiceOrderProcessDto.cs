﻿namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderProcess
{
    public class ServiceOrderProcessDto : OutputDto
    {
        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoId { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string? ServiceOrderId { get; set; }

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
        public int SerialNumber { get; set; }

        /// <summary> 
        /// 过程类型
        /// </summary>
        public string? ProcessType { get; set; }

        /// <summary> 
        /// 过程描述
        /// </summary>
        public string? ProcessReasons { get; set; }

        /// <summary> 
        /// 操作时间
        /// </summary>
        public DateTime? OperationTime { get; set; }

        /// <summary> 
        /// 操作人员
        /// </summary>
        public string? OperationPersonnel { get; set; }

        /// <summary> 
        /// 操作人员姓名
        /// </summary>
        public string? OperationPersonnelName { get; set; }

        /// <summary> 
        /// 是否回复
        /// </summary>
        public string? IsReply { get; set; }

        /// <summary> 
        /// 回复时间
        /// </summary>
        public DateTime? ReplyTime { get; set; }

        /// <summary> 
        /// 回复人员
        /// </summary>
        public string? ReplyPersonnel { get; set; }

        /// <summary> 
        /// 回复人员姓名
        /// </summary>
        public string? ReplyPersonnelName { get; set; }

        /// <summary> 
        /// 回复结果
        /// </summary>
        public string? ReplyResult { get; set; }
    }
}
