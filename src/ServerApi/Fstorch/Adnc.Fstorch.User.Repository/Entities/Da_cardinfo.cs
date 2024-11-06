namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Da_cardinfo : EfEntity
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 名片ID（对方）
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 交换名片ID（个人）
        /// </summary>
        public long CardExchangeId { get;set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;


        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creater { get; set; }

        /// <summary>
        /// 好友类型
        /// </summary>
        public string? Addtype { get; set; }
    }
}
