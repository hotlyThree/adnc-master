using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.CodeBase
{
    public class Bm_Service_SpecificationsDto : InputDto
    {
        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 服务规范编码
        /// </summary>
        public string ServiceSpecificationID { get; set; }

        /// <summary> 
        /// 服务规范名称
        /// </summary>
        public string ServiceSpecificationName { get; set; }

        /// <summary> 
        /// 服务流程编码
        /// </summary>
        public string ServiceProcessID { get; set; }

        /// <summary> 
        /// 服务规范描述
        /// </summary>
        public string ServiceSpecificationDescribe { get; set; }

        /// <summary> 
        /// 图片模板
        /// </summary>
        public string ImageTemplate { get; set; }

        /// <summary> 
        /// 附件模板
        /// </summary>
        public string AttachmentTemplate { get; set; }

        /// <summary> 
        /// 考核时长(分钟)
        /// </summary>
        public int CheckDuration { get; set; }

        /// <summary> 
        /// 必须完成
        /// </summary>
        public string MustCompleted { get; set; }

        /// <summary> 
        /// 规范完成验证
        /// </summary>
        public string SpecificationEndVerification { get; set; }

        /// <summary> 
        /// 使用对象
        /// </summary>
        public string UsingObject { get; set; }

        /// <summary> 
        /// 是否审核
        /// </summary>
        public string ToReview { get; set; }

        /// <summary> 
        /// 审核结果
        /// </summary>
        public string ReviewResult { get; set; }

        /// <summary> 
        /// 审核对象
        /// </summary>
        public string ReviewObject { get; set; }

        /// <summary> 
        /// 其它操作
        /// </summary>
        public string OtherOperations { get; set; }

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; }

        /// <summary> 
        /// 是否最终规范
        /// </summary>
        public string IsEndSpecification { get; set; }


    }
}
