﻿namespace Fstorch.AfterSales.Application.Dtos.ServiceTag
{
    [Serializable]
    public class ServiceTagDto : OutputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary> 
        /// 系统标记
        /// </summary>
        public string ServiceTag { get; set; }

        /// <summary> 
        /// 服务状态编码
        /// </summary>
        public string ServiceStatusId { get; set; }

        /// <summary> 
        /// 服务类型编码
        /// </summary>
        public string ServiceTypeId { get; set; }

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