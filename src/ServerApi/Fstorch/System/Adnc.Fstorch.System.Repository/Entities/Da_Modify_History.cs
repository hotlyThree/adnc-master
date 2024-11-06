
using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Da_Modify_History : EfEntity
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 业务表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary> 
        /// 信息ID
        /// </summary>
        public long InfoID { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary> 
        /// 修改内容
        /// </summary>
        public string ModifyDescribe { get; set; }

        /// <summary> 
        /// 修改人员
        /// </summary>
        public string ModifyPersonnel { get; set; }
    }
}
