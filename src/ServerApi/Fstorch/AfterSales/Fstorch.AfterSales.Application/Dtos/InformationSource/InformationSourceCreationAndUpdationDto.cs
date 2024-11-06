namespace Fstorch.AfterSales.Application.Dtos.InformationSource
{
    public class InformationSourceCreationAndUpdationDto : InputDto
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 来源名称
        /// </summary>
        public string InfoSourceName { get; set; }

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
