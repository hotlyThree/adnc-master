using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.CodeBase
{
    public class Bm_DepartMentDto : InputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 工作部门
        /// </summary>
        public string DepartMentName { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; }

        /// <summary> 
        /// 是否可用
        /// </summary>
        public string IsValid { get; set; }


    }
}
