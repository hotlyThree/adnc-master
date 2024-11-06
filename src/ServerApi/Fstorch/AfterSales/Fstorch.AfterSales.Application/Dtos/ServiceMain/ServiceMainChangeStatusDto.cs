namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainChangeStatusDto : InputDto
    {
        /// <summary>
        /// 服务客户档案ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 服务客户档案状态
        /// </summary>
        public string Status { get; set; }
    }

    public class ServiceMainChangeStatusBatchDto : InputDto
    {
        /// <summary>
        /// 服务客户档案ID列表
        /// </summary>
        public IEnumerable<long> Ids { get; set; }

        /// <summary>
        /// 服务客户档案状态
        /// </summary>
        public string Status { get; set; }
    }
}
