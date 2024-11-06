namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务回访档案
    /// </summary>
    public class Da_Service_Followup : EfEntity
    {

        /// <summary> 
        /// 回访编码ID
        /// </summary>
        public long FollowupId { get; set; }

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
        /// 回访结果
        /// </summary>
        public string FollowResult { get; set; }
    }
}
