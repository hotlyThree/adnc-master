namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    /// <summary>
    /// 名片转移定义
    /// </summary>
    public class CardCaseTransferDto : InputDto
    {
        /// <summary>
        /// 原账号
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 目标账号
        /// </summary>
        public long TargetAid { get; set; }

        /// <summary>
        /// 目标账号名片ID
        /// </summary>
        public long TargetUid { get; set; }
    }
}
