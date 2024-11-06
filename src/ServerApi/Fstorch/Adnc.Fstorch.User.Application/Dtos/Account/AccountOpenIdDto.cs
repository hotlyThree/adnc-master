namespace Adnc.Fstorch.User.Application.Dtos.Account
{
    public class AccountOpenIdDto : InputDto
    {
        /// <summary>
        /// 程序appid
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 登录code
        /// </summary>
        public string Code { get; set; }
    }
}
