
namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_userinfo : EfEntity
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
        /// 简介
        /// </summary>
        public string Usertitle { get; set; } = string.Empty;

        /// <summary>
        /// 微信号
        /// </summary>
        public string Wxid { get; set; } = string.Empty;

        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get;set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        public string Country { get;set;} = string.Empty;

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set;} = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// 关于
        /// </summary>
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? Reg_time { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Audit_time { get; set;}

        /// <summary>
        /// 状态(A有效，B编辑中，C注销)
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 是否默认 true false
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 样式ID
        /// </summary>
        public int Styleid { get; set; }

        /// <summary>
        /// 背景ID
        /// </summary>
        public int Bgid { get; set; }
    }
}
