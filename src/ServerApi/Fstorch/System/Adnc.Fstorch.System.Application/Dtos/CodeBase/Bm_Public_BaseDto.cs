using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.CodeBase
{
    public class Bm_Public_BaseDto : InputDto
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
