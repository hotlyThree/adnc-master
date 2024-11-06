namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainCopyDto : InputDto
    {
        /// <summary>
        /// 服务客户档案ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 复制次数
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 接单人ID
        /// </summary>
        public long OrderTaker { get; set; }
    }
}
