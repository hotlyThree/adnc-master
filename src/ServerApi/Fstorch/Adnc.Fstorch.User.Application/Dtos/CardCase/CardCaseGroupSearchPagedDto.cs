namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseGroupSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 是否查询今天 默认0 查询全部    为1查询今天
        /// </summary>
        public int Today { get; set; }
    }
}
