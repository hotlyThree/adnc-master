using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.Setting
{
    public class SystemCompanyInfoDto:InputDto
    {
        /// <summary> 
        /// 公司ID
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 商家类型
        /// </summary>
        public string ProviderType { get; set; }

        /// <summary> 
        /// 商家ID
        /// </summary>
        public string ProviderID { get; set; }

        /// <summary> 
        /// 商家名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary> 
        /// 商家地址
        /// </summary>
        public string ProviderAddress { get; set; }

        /// <summary> 
        /// 联系方式
        /// </summary>
        public string ProviderPhone { get; set; }

        /// <summary> 
        /// 所属商家ID
        /// </summary>
        public string ProviderParentID { get; set; }

        /// <summary> 
        /// 支付商户号
        /// </summary>
        public string PayMchID { get; set; }

        /// <summary> 
        /// 商家状态
        /// </summary>
        public string ProviderStatus { get; set; }

        /// <summary> 
        /// 纬度
        /// </summary>
        public string Lat { get; set; }

        /// <summary> 
        /// 经度
        /// </summary>
        public string Lng { get; set; }

        /// <summary> 
        /// 服务产品
        /// </summary>
        public string ServiceProduct { get; set; }

        /// <summary> 
        /// 服务区域
        /// </summary>
        public string ServiceArea { get; set; }


    }
}
