namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderProcess
{
    public class ServiceOrderProcessUpdationDto : InputDto
    {
        /// <summary>
        /// 过程ID
        /// </summary>
        public long Id { get; set; }

        /// <summary> 
        /// 回复人员
        /// </summary>
        public string ReplyPersonnel { get; set; }

        /// <summary> 
        /// 回复结果
        /// </summary>
        public string ReplyResult { get; set; }
    }
}
