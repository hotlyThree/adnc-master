namespace Fstorch.AfterSales.Application.Dtos.Public
{
    public class FollowCountReportDto : CountReportDto
    {
        /// <summary>
        /// 回访次数 1:一次回访   2:二次回访   3:三次回访
        /// </summary>
        public int Timer { get; set; }
    }
}
