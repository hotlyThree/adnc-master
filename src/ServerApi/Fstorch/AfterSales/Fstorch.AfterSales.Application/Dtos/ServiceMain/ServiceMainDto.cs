namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    [Serializable]
    public class ServiceMainDto : OutputDto
    {

        /// <summary>
        /// 业务编号
        /// </summary>
        public string? DocumentNumber { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }


        /// <summary>
        /// 分公司编号
        /// </summary>
        public long? BranchID { get; set; }

        /// <summary>
        /// 服务商编号	
        /// </summary>
        public long? ProviderID { get; set; }

        /// <summary>
        /// 接单商家ID
        /// </summary>
        public string? ReceivingProviderID { get; set; }

        /// <summary>
        /// 会员编码
        /// </summary>
        public string? MemberCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public string? UserSource { get; set; }

        /// <summary>
        /// 联系人员
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string? RegionCode { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 市区
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 	街道
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// 小区
        /// </summary>
        public string? Village { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string? UserType { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string? BusinessType { get; set; }

        /// <summary>
        /// 服务请求
        /// </summary>
        public string? ServiceRequest { get; set; }

        /// <summary>
        /// 接单日期
        /// </summary>
        public DateTime AcceptanceDate { get; set; }

        /// <summary>
        /// 接单人员
        /// </summary>
        public string? OrderTaker { get; set; }

        /// <summary>
        /// 接单人员姓名
        /// </summary>
        public string? OrderTakerName { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// 预约时段
        /// </summary>
        public string? AppointmentPeriod { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string? ContractNo { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string? SalesNO { get; set; }

        /// <summary>
        /// 工厂单号
        /// </summary>
        public string? FactoryNO { get; set; }

        /// <summary>
        /// 服务状态
        /// </summary>
        public string? ServiceStatus { get; set; }

        /// <summary>
        /// 服务流程
        /// </summary>
        public string? Serviceprocess { get; set; }

        /// <summary>
        /// 服务备注
        /// </summary>
        public string? Servicenotes { get; set; }

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
        public decimal OffbailIncome { get; set; }

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
        public decimal ServiceCommission { get; set; }

        /// <summary>
        /// 服务利润
        /// </summary>
        public decimal ServiceProfit { get; set; }

        /// <summary>
        /// 是否封单
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 产品明细
        /// </summary>
        public string details { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }

    }
}
