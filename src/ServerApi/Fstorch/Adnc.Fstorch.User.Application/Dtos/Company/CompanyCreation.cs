using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.Company
{
    public class CompanyCreation
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
        /// 创建人
        /// </summary>
        public long Creater { get; set; }

    }
}
