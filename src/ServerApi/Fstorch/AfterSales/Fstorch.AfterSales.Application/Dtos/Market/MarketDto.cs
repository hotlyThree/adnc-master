namespace Fstorch.AfterSales.Application.Dtos.Market
{
    [Serializable]
    public class MarketDto : OutputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 购机商场
        /// </summary>
        public string BuyMarketName { get; set; }

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
