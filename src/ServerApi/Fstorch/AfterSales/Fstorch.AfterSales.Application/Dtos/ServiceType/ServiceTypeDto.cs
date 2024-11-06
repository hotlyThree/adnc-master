namespace Fstorch.AfterSales.Application.Dtos.ServiceType
{
    [Serializable]
    public class ServiceTypeDto : OutputDto
    {

        /// <summary> 
        /// 类型名称
        /// </summary>
        public string ServiceTypeName { get; set; }

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
