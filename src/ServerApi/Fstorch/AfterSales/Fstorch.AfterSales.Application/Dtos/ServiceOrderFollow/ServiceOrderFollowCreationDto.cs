namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderFollow
{
    public class ServiceOrderFollowCreationDto : InputDto
    {
        /// <summary> 
        /// 回访编码ID列表
        /// </summary>
        public IEnumerable<long> FollowupIds { get; set; }

        /// <summary>
        /// 回访人员
        /// </summary>
        public long Creater { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderId { get; set; }

        /// <summary> 
        /// 回访次数
        /// </summary>
        public int FollowNumber { get; set; }

        /// <summary> 
        /// 回访结果列表
        /// </summary>
        public IEnumerable<string> FollowResults { get; set; }

        /// <summary>
        /// 最终结果
        /// </summary>
        public string FinalResult { get; set; }
    }
}
