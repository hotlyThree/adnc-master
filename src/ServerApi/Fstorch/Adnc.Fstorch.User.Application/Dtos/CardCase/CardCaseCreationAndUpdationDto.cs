namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseCreationAndUpdationDto : InputDto
    {

        /// <summary>
        /// 好友类型
        /// </summary>
        public string? Addtype { get; set; }

        /// <summary>
        /// 小程序APPID  用于通知
        /// </summary>
        public string Appid { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } = string.Empty;
    }
}
