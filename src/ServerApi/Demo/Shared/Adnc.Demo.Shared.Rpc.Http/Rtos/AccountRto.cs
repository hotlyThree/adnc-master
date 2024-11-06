namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    public class AccountRto
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 微信OPENID
        /// </summary>
        public string Openid { get; set; } = string.Empty;

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Openname { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// 微信视频号图片
        /// </summary>
        public string Wechatvideo { get; set; } = string.Empty;

        /// <summary>
        /// 抖音视频号图片
        /// </summary>
        public string Tiktokvideo { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间/注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 共享ID
        /// </summary>
        public string UnionId { get; set; } = string.Empty;
    }
}
