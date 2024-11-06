namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderRec
{
    public class ServiceOrderRecReviewDto : InputDto
    {
        /// <summary>
        /// 应收ID
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// 审核人员
        /// </summary>
        public long Reviewer { get; set; }
    }
}
