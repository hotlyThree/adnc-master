namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Sys_BackGround : EfEntity
    {
        [NotMapped]
        public new long Id { get; set; }

        /// <summary>
        /// 背景ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// 是否选中
        /// </summary>
        public string Checked { get; set; } = "false";
    }
}
