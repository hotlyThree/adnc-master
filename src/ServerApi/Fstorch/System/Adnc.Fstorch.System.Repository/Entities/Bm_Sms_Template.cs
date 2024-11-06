using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Bm_Sms_Template : EfEntity
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 类型编码
        /// </summary>
        public string SmsTemplateID { get; set; }

        /// <summary> 
        /// 类型名称
        /// </summary>
        public string SmsTemplateName { get; set; }

        /// <summary> 
        /// 模板内容
        /// </summary>
        public string SmsTemplateMemo { get; set; }

        /// <summary> 
        /// 模板类型
        /// </summary>
        public string SmsTemplatetype { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; }


    }
}
