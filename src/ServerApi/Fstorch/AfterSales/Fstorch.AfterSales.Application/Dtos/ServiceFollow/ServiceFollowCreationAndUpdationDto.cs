namespace Fstorch.AfterSales.Application.Dtos.ServiceFollow
{
    public class ServiceFollowCreationAndUpdationDto : InputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 回访次数
        /// </summary>
        public int FollowNumber { get; set; }

        /// <summary> 
        /// 回访名称
        /// </summary>
        public string FollowName { get; set; }

        /// <summary> 
        /// 回访类型
        /// </summary>
        public string FollowType { get; set; }

        /// <summary> 
        /// 服务类型编码
        /// </summary>
        public string ServiceTypeId { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int DisplayNumber { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }
    }
}
