namespace Adnc.Demo.Shared.Rpc.Http.Rtos;

   
public class Bm_Service_ProcessRto
{

    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }
    /// <summary> 
    /// 服务流程编码
    /// </summary>
    public string ServiceProcessID { get; set; } = string.Empty;

    /// <summary> 
    /// 服务流程名称
    /// </summary>
    public string ServiceProcessName { get; set; } = string.Empty;

    /// <summary> 
    /// 图片模板
    /// </summary>
    public string ImageTemplate { get; set; } = string.Empty;

    /// <summary> 
    /// 附件模板
    /// </summary>
    public string AttachmentTemplate { get; set; } = string.Empty;

    /// <summary> 
    /// 财务验证
    /// </summary>
    public string FinancialVerification { get; set; } = string.Empty;

    /// <summary> 
    /// 订单验证
    /// </summary>
    public string OrderVerification { get; set; } = string.Empty;

    /// <summary> 
    /// 必须完成
    /// </summary>
    public string MustCompleted { get; set; } = string.Empty;

    /// <summary> 
    /// 流程结束验证
    /// </summary>
    public string ProcessEndVerification { get; set; } = string.Empty;

    /// <summary> 
    /// 使用对象
    /// </summary>
    public string UsingObject { get; set; } = string.Empty;

    /// <summary> 
    /// 是否审核
    /// </summary>
    public string ToReview { get; set; } = string.Empty;

    /// <summary> 
    /// 审核对象
    /// </summary>
    public string ReviewObject { get; set; } = string.Empty;

    /// <summary> 
    /// 审核结果
    /// </summary>
    public string ReviewResult { get; set; } = string.Empty;

    /// <summary> 
    /// 其它操作
    /// </summary>
    public string OtherOperations { get; set; } = string.Empty;

    /// <summary> 
    /// 是否最终流程
    /// </summary>
    public string IsEndProcess { get; set; } = string.Empty;


}
