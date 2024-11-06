namespace Adnc.Fstorch.User.Application.Dtos.MemberType
{
    public class MemberTypeCreationDto : MemberTypeCreationAndUpdationDto
    {
        /// <summary>
        /// 会员类型（A个人 B企业）
        /// </summary>
        public string Membertype { get; set; }
    }
}
