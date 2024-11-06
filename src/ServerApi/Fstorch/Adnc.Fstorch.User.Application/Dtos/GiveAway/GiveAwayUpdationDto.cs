namespace Adnc.Fstorch.User.Application.Dtos.GiveAway
{
    public class GiveAwayUpdationDto : InputDto
    {
        /// <summary>
        /// 赠送ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 使用用户ID
        /// </summary>
        public long Usecode { get; set; }
    }
}
