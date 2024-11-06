using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.Other
{
    public class Da_Print_TemplateDto:InputDto
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
