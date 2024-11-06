namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderFollow
{
    public class ServiceOrderFollowDto : OutputDto
    {
        /// <summary> 
        /// 回访编码ID
        /// </summary>
        public long FollowupId { get; set; }

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
