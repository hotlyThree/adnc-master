namespace Adnc.Fstorch.User.Application.Dtos.User
{
    public class UserSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 账号id
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set;}

        /// <summary>
        /// 公司ID
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set;}

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set;}

        /// <summary>
        /// 状态
        /// </summary>
        public string? Status { get; set;}

        /// <summary>
        /// 是否默认
        /// </summary>
        public string? Default { get; set; }
    }
}
