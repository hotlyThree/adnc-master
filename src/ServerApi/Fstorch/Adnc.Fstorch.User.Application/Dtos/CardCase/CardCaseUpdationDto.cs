namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseUpdationDto : CardCaseCreationAndUpdationDto
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } = string.Empty;
    }
}
