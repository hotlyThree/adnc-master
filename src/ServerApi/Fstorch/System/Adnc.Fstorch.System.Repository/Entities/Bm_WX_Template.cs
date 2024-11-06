using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Bm_WX_Template : EfEntity
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 类型编码
        /// </summary>
        public string WxTemplateID { get; set; }

        /// <summary> 
        /// 类型名称
        /// </summary>
        public string WxTemplateName { get; set; }

        /// <summary> 
        /// 模板内容
        /// </summary>
        public string WxTemplateMemo { get; set; }

        /// <summary> 
        /// 模板类型
        /// </summary>
        public string WxTemplatetype { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; }


    }
}
