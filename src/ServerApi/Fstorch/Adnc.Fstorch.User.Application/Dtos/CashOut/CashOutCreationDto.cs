namespace Adnc.Fstorch.User.Application.Dtos.CashOut
{
    public class CashOutCreationDto : InputDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Outmoney { get; set; }
    }
}
