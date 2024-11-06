using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Bm_Public_Base : EfEntity
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 业务模块ID
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary> 
        /// 业务模板名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary> 
        /// 业务模块描述
        /// </summary>
        public string ModuleDescribe { get; set; }


    }
}
