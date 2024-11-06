using Adnc.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Bm_DepartMent : EfEntity 
    {
        
        [NotMapped]
        public new long Id { get; set; }
        /// <summary> 
        /// 企业编号
        /// </summary>
        [Key]
        public long CompanyID { get; set; }

        /// <summary> 
        /// 工作部门
        /// </summary>
        /// 
        [Key]
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
