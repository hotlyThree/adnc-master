namespace Adnc.Fstorch.User.Application.Dtos.MemberType
{
    public class MemberTypeCreationAndUpdationDto : InputDto
    {
        /// <summary>
        /// 会员名称
        /// </summary>
        public string Membername { get; set; } = string.Empty;


        /// <summary>
        /// 会员费用/年
        /// </summary>
        public decimal Memberprice { get; set; }

        /// <summary>
        /// 提成金额/年
        /// </summary>
        public decimal Shareamount { get; set; }

        /// <summary>
        /// 会员权益描述
        /// </summary>
        public string Memberdescribe { get; set; } = string.Empty;

        /// <summary>
        /// 状态（A有效 B无效）
        /// </summary>
        public string Memberstatus { get; set; } = "A";

    }
}
