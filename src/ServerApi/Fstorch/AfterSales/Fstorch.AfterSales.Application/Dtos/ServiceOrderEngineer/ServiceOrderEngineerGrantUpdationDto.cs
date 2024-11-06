namespace Fstorch.AfterSales.Application.Dtos.ServiceOrderEngineer
{
    public class ServiceOrderEngineerGrantUpdationDto : InputDto
    {
        /// <summary>
        /// 工资ID列表
        /// </summary>
        public IEnumerable<long> Ids { get; set; }


        /// <summary>
        /// 发放批次 1已发 0待发
        /// </summary>
        public string GrantStatus { get; set; }
    }
}
