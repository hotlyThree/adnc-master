﻿namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务性质
    /// </summary>
    public class Bm_Service_Nature : EfEntity
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyId { get; set; }


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