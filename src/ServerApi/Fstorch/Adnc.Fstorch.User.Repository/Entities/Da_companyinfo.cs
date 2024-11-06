using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_companyinfo : EfEntity
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 经营范围
        /// </summary>
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// 公司Logo    Base64
        /// </summary>
        public string Logo { get; set; } = string.Empty;

        /// <summary>
        /// 公司官网
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 服务热线
        /// </summary>
        public string Phone { get;set; } = string.Empty;

        /// <summary>
        /// 省市区
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 公司状态(A有效 B编辑中 C注销)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? Reg_time { get; set; }

        /// <summary>
        /// 有效止日
        /// </summary>
        public DateTime? Valid_time { get; set; }

        /// <summary>
        /// 管理员手机
        /// </summary>
        public string Managerphone { get; set; } = string.Empty;

        /// <summary>
        /// 内部人员
        /// </summary>
        public string Insider { get; set; } = string.Empty;

        /// <summary>
        /// 申请人员
        /// </summary>
        public string Applicant { get; set; } = string.Empty;

        /// <summary>
        /// 申请人员申请时名片列表
        /// </summary>
        public string CardApplicant { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creater { get; set; }
    }
}
