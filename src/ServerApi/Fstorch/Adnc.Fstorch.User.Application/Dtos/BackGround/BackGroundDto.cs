namespace Adnc.Fstorch.User.Application.Dtos.BackGround
{
    [Serializable]
    public class BackGroundDto : OutputDto
    {
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
