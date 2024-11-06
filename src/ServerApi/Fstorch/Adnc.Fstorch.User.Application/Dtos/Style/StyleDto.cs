﻿namespace Adnc.Fstorch.User.Application.Dtos.Style
{
    [Serializable]
    public class StyleDto : OutputDto
    {

        /// <summary>
        /// 样式图片
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 样式名称
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 样式css
        /// </summary>
        public string Modelclass { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public string Checked { get; set; } = "false";

        /// <summary>
        /// 样式明细
        /// </summary>
        public string StyleObj { get; set; } = string.Empty;
    }
}
