namespace Fstorch.AfterSales.Application.Dtos.ServiceMain
{
    public class ServiceMainSearchPagedDto : SearchPagedDto
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary>
        /// 分公司编号
        /// </summary>
        public long? BranchId { get; set; }

        /// <summary>
        /// 服务商编号
        /// </summary>
        public long? ProviderId { get; set; }

        /// <summary>
        /// 接单商家ID
        /// </summary>
        public string ReceivingProviderID { get; set; } = string.Empty;

        /// <summary>
        /// 会员编码
        /// </summary>
        public string MemberCode { get; set; } = string.Empty;

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 联系人员
        /// </summary>
        public string ContactPerson { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; } = string.Empty;

        /// <summary>
        /// 区域编码
        /// </summary>
        public string RegionCode { get; set; } = string.Empty;

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市区
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 	街道
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// 小区
        /// </summary>
        public string Village { get; set; } = string.Empty;

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 用户类型
        /// </summary>
        public IEnumerable<string> UserType { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// 业务类型
        /// </summary>
        public IEnumerable<string> BusinessType { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// 服务请求
        /// </summary>
        public string ServiceRequest { get; set; } = string.Empty;

        /// <summary>
        /// 接单日期起
        /// </summary>
        public DateTime? AcceptanceDateS { get; set; }

        /// <summary>
        /// 接单日期止
        /// </summary>
        public DateTime? AcceptanceDateE { get; set; }

        /// <summary>
        /// 接单人员
        /// </summary>
        public string OrderTaker { get; set; } = string.Empty;

        /// <summary>
        /// 预约日期起
        /// </summary>
        public DateTime? AppointmentDateS { get; set; }


        /// <summary>
        /// 预约日期止
        /// </summary>
        public DateTime? AppointmentDateE { get; set; }

        /// <summary>
        /// 预约时段
        /// </summary>
        public string AppointmentPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesNO { get; set; } = string.Empty;

        /// <summary>
        /// 工厂单号
        /// </summary>
        public string FactoryNO { get; set; } = string.Empty;

        /// <summary>
        /// 服务状态
        /// </summary>
        public IEnumerable<string> ServiceStatus { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// 服务流程
        /// </summary>
        public IEnumerable<string> ServiceProcess { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 产品类型
        /// </summary>
        public IEnumerable<string> ProductType { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 产品品牌
        /// </summary>
        public IEnumerable<string> Branding { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 品牌内容
        /// </summary>
        public IEnumerable<string> BrandedContent { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 品牌型号
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// 用户来源
        /// </summary>
        public string UserSource { get; set; } = string.Empty;


        /// <summary> 
        /// 服务类型
        /// </summary>
        public IEnumerable<string> ServiceTypeID { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 购机商场
        /// </summary>
        public IEnumerable<string> PurchaseStore { get; set; } = Enumerable.Empty<string>();

        /// <summary> 
        /// 购机日期起
        /// </summary>
        public DateTime? PurchaseDateS { get; set; }

        /// <summary> 
        /// 购机日期止
        /// </summary>
        public DateTime? PurchaseDateE { get; set; }



        /// <summary> 
        /// 条码1
        /// </summary>
        public string Barcode1 { get; set; } = string.Empty;

        /// <summary> 
        /// 条码2
        /// </summary>
        public string Barcode2 { get; set; } = string.Empty;

        /// <summary>
        /// 服务备注
        /// </summary>
        public string Servicenotes { get; set; } = string.Empty;

        /// <summary>
        /// 是否封单
        /// </summary>
        public int IsFinish { get; set; }
    }
}
