namespace Adnc.Fstorch.User.Application.Dtos.CardCase
{
    public class CardCaseSearchPaged : SearchPagedDto
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 名片ID
        /// </summary>
        public long Uid { get; set; }


        /// <summary>
        /// 姓名/公司名称/职位
        /// </summary>
        public string? SearchInput { get; set; } = string.Empty;

        /// <summary>
        /// 是否自己发起  1是   0否
        /// </summary>
        public string Created { get; set; } = string.Empty;

        /// <summary>
        /// 类型
        /// </summary>
        public string? Addtype { get; set; } = string.Empty;
    }
}
