namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainAdjacentSearchDto : InputDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        
        /// <summary>
        /// 距离
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
    }
}
