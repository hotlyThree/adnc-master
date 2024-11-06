namespace Adnc.Demo.Shared.Rpc.Http.Rtos;


public class Bm_Service_SpecificationsRto 
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }
    /// <summary> 
    /// 服务规范编码
    /// </summary>
    public string ServiceSpecificationID { get; set; } = string.Empty;

    /// <summary> 
    /// 服务规范名称
    /// </summary>
    public string ServiceSpecificationName { get; set; } = string.Empty;

    /// <summary> 
    /// 服务流程编码
    /// </summary>
    public string ServiceProcessID { get; set; } = string.Empty;

    /// <summary> 
    /// 服务规范描述
    /// </summary>
    public string ServiceSpecificationDescribe { get; set; } = string.Empty;

    /// <summary> 
    /// 图片模板
    /// </summary>
    public string ImageTemplate { get; set; } = string.Empty;

    /// <summary> 
    /// 附件模板
    /// </summary>
    public string AttachmentTemplate { get; set; } = string.Empty;

    /// <summary> 
    /// 考核时长(分钟)
    /// </summary>
    public int CheckDuration { get; set; } = 0;

    /// <summary> 
    /// 必须完成
    /// </summary>
    public string MustCompleted { get; set; } = string.Empty;

    /// <summary> 
    /// 规范完成验证
    /// </summary>
    public string SpecificationEndVerification { get; set; } = string.Empty;

    /// <summary> 
    /// 使用对象
    /// </summary>
    public string UsingObject { get; set; } = string.Empty;

    /// <summary> 
    /// 是否审核
    /// </summary>
    public string ToReview { get; set; } = string.Empty;

    /// <summary> 
    /// 审核结果
    /// </summary>
    public string ReviewResult { get; set; } = string.Empty;

    /// <summary> 
    /// 审核对象
    /// </summary>
    public string ReviewObject { get; set; } = string.Empty;

    /// <summary> 
    /// 其它操作
    /// </summary>
    public string OtherOperations { get; set; } = string.Empty;

    /// <summary> 
    /// 显示序号
    /// </summary>
    public int Displaynumber { get; set; } = 0;

    /// <summary> 
    /// 是否最终规范
    /// </summary>
    public string IsEndSpecification { get; set; } = string.Empty;


}
