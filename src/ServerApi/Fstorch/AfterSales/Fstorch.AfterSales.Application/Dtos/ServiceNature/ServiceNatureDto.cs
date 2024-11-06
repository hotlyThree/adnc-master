﻿
namespace Fstorch.AfterSales.Application.Dtos.ServiceNature
{
    [Serializable]
    public class ServiceNatureDto : OutputDto
    {


        /// <summary> 
        /// 类型名称
        /// </summary>
        public string ServiceNatureName { get; set; }

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
