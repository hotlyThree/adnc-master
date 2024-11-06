namespace Fstorch.AfterSales.Repository.Entities
{
    /// <summary> 
    /// 服务客户工单
    /// </summary>
    public class Da_Service_Order : EfEntity
    {

        /// <summary> 
        /// 服务ID
        /// </summary>
        public long ServiceInfoID { get; set; }

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 服务工单号
        /// </summary>
        public string ServiceOrderID { get; set; } = string.Empty;

        /// <summary> 
        /// 服务流程编号
        /// </summary>
        public string ServiceprocessID { get; set; } = string.Empty;

        /// <summary>
        /// 服务标注
        /// </summary>
        public string ServiceTag { get; set; } = string.Empty;

        /// <summary> 
        /// 服务商家ID
        /// </summary>
        public long ServiceProviderID { get; set; }

        /// <summary> 
        /// 服务人员ID
        /// </summary>
        public string ServicestaffID { get; set; } = string.Empty;

        /// <summary> 
        /// 服务人员姓名
        /// </summary>
        public string ServicestaffName { get; set; } = string.Empty;

        /// <summary> 
        /// 公司派工日期
        /// </summary>
        public DateTime? CompanyDispatchTime { get; set; }

        /// <summary> 
        /// 公司派工人员
        /// </summary>
        public long CompanyDispatchPersonnel { get; set; }

        /// <summary> 
        /// 服务商派工日期
        /// </summary>
        public DateTime? ProviderDispatchTime { get; set; }

        /// <summary> 
        /// 服务商派工人员
        /// </summary>
        public long ProviderDispatchPersonnel { get; set; }

        /// <summary> 
        /// 服务请求
        /// </summary>
        public string ServiceRequest { get; set; } = string.Empty;

        /// <summary> 
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary> 
        /// 预约时段
        /// </summary>
        public string Appointmentperiod { get; set; } = string.Empty;

        /// <summary> 
        /// 预约人员
        /// </summary>
        public long AppointmentPersonnel { get; set; }

        /// <summary> 
        /// 服务序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 工程师阅读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary> 
        /// 到场时间
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary> 
        /// 报价时间
        /// </summary>
        public DateTime? QuotationTime { get; set; }

        /// <summary> 
        /// 报价金额
        /// </summary>
        public decimal QuotationAmount { get; set; }

        /// <summary> 
        /// 报价人员
        /// </summary>
        public long QuotationPersonnel { get; set; }

        /// <summary> 
        /// 故障原因
        /// </summary>
        public string FailureCause { get; set; } = string.Empty;

        /// <summary> 
        /// 服务措施
        /// </summary>
        public string ServiceMeasures { get; set; } = string.Empty;

        /// <summary> 
        /// 服务结果
        /// </summary>
        public string ServiceResult { get; set; } = string.Empty;

        /// <summary> 
        /// 遗留类型
        /// </summary>
        public string LegacyType { get; set; } = string.Empty;

        /// <summary> 
        /// 遗留描述
        /// </summary>
        public string LegacyReasons { get; set; } = string.Empty;

        /// <summary> 
        /// 处理开始时间
        /// </summary>
        public DateTime? ProcessingStartTime { get; set; }

        /// <summary> 
        /// 处理结束时间
        /// </summary>
        public DateTime? ProcessingEndTime { get; set; }

        /// <summary> 
        /// 是否交单
        /// </summary>
        public int IsSubmit { get; set; }

        /// <summary> 
        /// 交单时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }

        /// <summary> 
        /// 交单人员
        /// </summary>
        public long SubmitPersonnel { get; set; }

        /// <summary> 
        /// 保内收入厂家
        /// </summary>
        public decimal GuaranteedIncomeManufactor { get; set; }

        /// <summary> 
        /// 保内收入商家
        /// </summary>
        public decimal GuaranteedIncomeBusiness { get; set; }

        /// <summary> 
        /// 保外收入
        /// </summary>
        public decimal Offbailincome { get; set; }

        /// <summary> 
        /// 费用支出
        /// </summary>
        public decimal Disbursement { get; set; }

        /// <summary> 
        /// 保内材料成本
        /// </summary>
        public decimal GuarantMaterialcost { get; set; }

        /// <summary> 
        /// 保外材料成本
        /// </summary>
        public decimal OffbailMaterialcost { get; set; }

        /// <summary> 
        /// 服务提成
        /// </summary>
        public decimal Servicecommission { get; set; }

        /// <summary> 
        /// 服务利润
        /// </summary>
        public decimal ServiceProfit { get; set; }

        /// <summary> 
        /// 公司完工时间
        /// </summary>
        public DateTime? CompanyEndTime { get; set; }

        /// <summary> 
        /// 公司完工人员
        /// </summary>
        public long CompanyEndPersonnel { get; set; }

        /// <summary> 
        /// 是否结算
        /// </summary>
        public int IsSettlement { get; set; }

        /// <summary> 
        /// 结算代码
        /// </summary>
        public string SettlementCode { get; set; } = string.Empty;

        /// <summary> 
        /// 结算人员
        /// </summary>
        public long SettlementPersonnel { get; set; }

        /// <summary> 
        /// 结算时间
        /// </summary>
        public DateTime? SettlementTime { get; set; }

        /// <summary> 
        /// 是否材料核销
        /// </summary>
        public int IsMaterialVer { get; set; }

        /// <summary> 
        /// 材料核销人员
        /// </summary>
        public long MaterialVerPersonnel { get; set; }

        /// <summary> 
        /// 材料核销时间
        /// </summary>
        public DateTime? MaterialVerTime { get; set; }

        /// <summary> 
        /// 是否提成
        /// </summary>
        public int IsCommission { get; set; }

        /// <summary> 
        /// 提成计提人员
        /// </summary>
        public long CommissionPersonnel { get; set; }

        /// <summary> 
        /// 提成计提时间
        /// </summary>
        public DateTime? CommissionTime { get; set; }

        /// <summary> 
        /// 提成发放时间
        /// </summary>
        public DateTime? CommissionDistributionTime { get; set; }

        /// <summary> 
        /// 提成发放人员
        /// </summary>
        public long CommissionDistributionPersonnel { get; set; }

        /// <summary> 
        /// 提成发放月份
        /// </summary>
        public string CommissionDistributionMonth { get; set; } = string.Empty;

        /// <summary>
        /// 结算周次
        /// </summary>
        public int SettlementWeek { get; set; }

        /// <summary> 
        /// 是否保内对账
        /// </summary>
        public int IsGuaranteedReconciliation { get; set; }

        /// <summary> 
        /// 保内对账时间
        /// </summary>
        public DateTime? GuaranteedReconciliationTime { get; set; }

        /// <summary> 
        /// 保内对账人员
        /// </summary>
        public long GuaranteedReconciliationPersonnel { get; set; }

        /// <summary> 
        /// 保内开票时间
        /// </summary>
        public DateTime? GuaranteedInvoicingTime { get; set; }

        /// <summary> 
        /// 保内开票人员
        /// </summary>
        public long GuaranteedInvoicingPersonnel { get; set; }

        /// <summary> 
        /// 保内开票单号
        /// </summary>
        public string GuaranteedInvoicingNo { get; set; } = string.Empty;

        /// <summary> 
        /// 回访时间1
        /// </summary>
        public DateTime? FollowUpTime1 { get; set; }

        /// <summary> 
        /// 回访人员1
        /// </summary>
        public long FollowUpPersonnel1 { get; set; }

        /// <summary> 
        /// 回访结果1
        /// </summary>
        public string FollowUpResult1 { get; set; } = string.Empty;

        /// <summary> 
        /// 回访时间2
        /// </summary>
        public DateTime? FollowUpTime2 { get; set; }

        /// <summary> 
        /// 回访人员2
        /// </summary>
        public long FollowUpPersonnel2 { get; set; }

        /// <summary> 
        /// 回访结果3
        /// </summary>
        public string FollowUpResult2 { get; set; } = string.Empty;

        /// <summary> 
        /// 回访时间3
        /// </summary>
        public DateTime? FollowUpTime3 { get; set; }

        /// <summary> 
        /// 回访人员3
        /// </summary>
        public long FollowUpPersonnel3 { get; set; }

        /// <summary> 
        /// 回访结果3
        /// </summary>
        public string FollowUpResult3 { get; set; } = string.Empty;

        /// <summary> 
        /// 工单备注
        /// </summary>
        public string ServiceOrderNotes { get; set; } = string.Empty;

        /// <summary> 
        /// 工单状态
        /// </summary>
        public string ServiceOrderStatus { get; set; } = string.Empty;

        /// <summary> 
        /// 是否封单
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary> 
        /// 分享次数
        /// </summary>
        public int ShareNumbers { get; set; }

        /// <summary> 
        /// 分享人员
        /// </summary>
        public long SharePersonnel { get; set; }

        /// <summary> 
        /// 打印次数
        /// </summary>
        public int PrintNumber { get; set; }

        /// <summary> 
        /// 打印人员
        /// </summary>
        public long PrintPersonnel { get; set; }

        /// <summary> 
        /// 打印时间
        /// </summary>
        public DateTime? PrintTime { get; set; }

        /// <summary>
        /// 工程师拒单时间
        /// </summary>
        public DateTime? EngineerCacelTime { get; set; }

        /// <summary>
        /// 工程师拒单原因
        /// </summary>
        public string EngineerCacelReason { get; set; } = string.Empty;

        /// <summary>
        /// 商家拒单时间
        /// </summary>
        public DateTime? ProviderCacelTime { get; set; }

        /// <summary>
        /// 商家拒单原因
        /// </summary>
        public string ProviderCacleReason { get; set; } = string.Empty;

        /// <summary>
        /// 产品明细
        /// </summary>
        public string Details { get; set; } = string.Empty;
    }
}
