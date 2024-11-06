using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.User
{
   // [Serializable]
    public class UserCreationAndUpdationDto
    {


        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 是否当前默认 true false
        /// </summary>
        public string Default { get; set;}

        /// <summary>
        /// 简介
        /// </summary>
        public string Usertitle { get; set; } = string.Empty;

        /// <summary>
        /// 微信号
        /// </summary>
        public string Wxid { get; set; } = string.Empty;
    }
}
