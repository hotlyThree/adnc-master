namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务过程日志
    /// </summary>
    public class Da_Service_Order_Logs : EfEntity
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
        public long ServiceOrderId { get; set; }

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string ServiceProcessId { get; set; }

        /// <summary> 
        /// 服务规范ID
        /// </summary>
        public string ServiceSpecificationId { get; set; }

        /// <summary> 
        /// 日志序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary> 
        /// 日志类型
        /// </summary>
        public string LogsType { get; set; }

        /// <summary> 
        /// 今日内容
        /// </summary>
        public string TodayDescribe { get; set; }

        /// <summary> 
        /// 明日计划
        /// </summary>
        public string TomorrowDescribe { get; set; }

        /// <summary> 
        /// 操作时间
        /// </summary>
        public DateTime? OperationTime { get; set; }

        /// <summary> 
        /// 操作人员
        /// </summary>
        public string OperationPersonnel { get; set; }

        /// <summary> 
        /// 是否回复
        /// </summary>
        public string IsReviewer { get; set; }

        /// <summary> 
        /// 回复时间
        /// </summary>
        public DateTime? ReviewerTime { get; set; }

        /// <summary> 
        /// 回复人员
        /// </summary>
        public string ReviewerPersonnel { get; set; }

        /// <summary> 
        /// 回复结果
        /// </summary>
        public string ReviewerResult { get; set; }

    }
}
