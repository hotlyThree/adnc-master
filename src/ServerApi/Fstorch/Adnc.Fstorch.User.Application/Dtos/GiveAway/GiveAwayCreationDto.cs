namespace Adnc.Fstorch.User.Application.Dtos.GiveAway
{
    public class GiveAwayCreationDto : InputDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long Memberid { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Expirationdate { get; set; }
    }
}
