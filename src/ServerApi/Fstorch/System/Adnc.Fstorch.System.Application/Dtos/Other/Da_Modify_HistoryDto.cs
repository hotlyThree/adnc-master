
using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.Other
{
    public class Da_Modify_HistoryDto : InputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long companyid { get; set; }

        /// <summary> 
        /// 业务表名
        /// </summary>
        public string tablename { get; set; }

        /// <summary> 
        /// 信息ID
        /// </summary>
        public long infoid { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime modifytime { get; set; }

        /// <summary> 
        /// 修改内容
        /// </summary>
        public string modifydescribe { get; set; }

        /// <summary> 
        /// 修改人员
        /// </summary>
        public string modifypersonnel { get; set; }
    }
}
