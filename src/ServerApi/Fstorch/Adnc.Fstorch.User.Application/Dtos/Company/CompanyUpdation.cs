namespace Adnc.Fstorch.User.Application.Dtos.Company
{
    public class CompanyUpdation
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
        public string Phone { get; set; } = string.Empty;

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
        /// 有效止日
        /// </summary>
        public DateTime? Valid_time { get; set; }

        /// <summary>
        /// 管理员手机
        /// </summary>
        public string Managerphone { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creater { get; set; }
    }
}
