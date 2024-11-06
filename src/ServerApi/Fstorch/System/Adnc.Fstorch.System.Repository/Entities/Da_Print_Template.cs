using Adnc.Infra.Entities;

namespace Adnc.Fstorch.System.Repository.Entities
{
    public class Da_Print_Template : EfEntity
    {
        /// <summary> 
        /// 模板ID
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 业务ID
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary> 
        /// 模板ID
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary> 
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary> 
        /// 模板条件
        /// </summary>
        public string TemplateCondition { get; set; }


    }
}
