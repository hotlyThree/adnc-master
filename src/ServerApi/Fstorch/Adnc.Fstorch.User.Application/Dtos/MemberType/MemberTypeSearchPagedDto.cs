namespace Adnc.Fstorch.User.Application.Dtos.MemberType
{
    public class MemberTypeSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string Membername { get; set; } = string.Empty;


        /// <summary>
        /// 会员权益描述
        /// </summary>
        public string Memberdescribe { get; set; } = string.Empty;

        /// <summary>
        /// 会员类型（A个人 B企业）
        /// </summary>
        public string Membertype { get; set; } = "A";

        /// <summary>
        /// 状态（A有效 B无效）默认有效
        /// </summary>
        public string Memberstatus { get; set; } = "A";
    }
}
