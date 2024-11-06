using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Fstorch.System.Application.Dtos.Other
{
    public class Da_StaffDto:InputDto
    {
        /// <summary> 
        /// 员工ID
        /// </summary>
        public long StaffID { get; set; }

        /// <summary> 
        /// 姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary> 
        /// 姓名（别名）
        /// </summary>
        public string StaffName_Alias { get; set; }

        /// <summary> 
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary> 
        /// 证件类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary> 
        /// 证件号码
        /// </summary>
        public string CardID { get; set; }

        /// <summary> 
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary> 
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary> 
        /// 身高（厘米）
        /// </summary>
        public int Stature { get; set; }

        /// <summary> 
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary> 
        /// 政治面貌
        /// </summary>
        public string Political { get; set; }

        /// <summary> 
        /// 户口性质
        /// </summary>
        public string AccountNature { get; set; }

        /// <summary> 
        /// 户口地址
        /// </summary>
        public string AccountAddress { get; set; }

        /// <summary> 
        /// 学历
        /// </summary>
        public string Educational { get; set; }

        /// <summary> 
        /// 毕业院校
        /// </summary>
        public string Schoolname { get; set; }

        /// <summary> 
        /// 职称
        /// </summary>
        public string Professional { get; set; }

        /// <summary> 
        /// 专业
        /// </summary>
        public string Specialized { get; set; }

        /// <summary> 
        /// 毕业时间
        /// </summary>
        public DateTime GraduationTime { get; set; }

        /// <summary> 
        /// 籍贯省
        /// </summary>
        public string Origin_Visit { get; set; }

        /// <summary> 
        /// 籍贯市
        /// </summary>
        public string Origin_City { get; set; }

        /// <summary> 
        /// 籍贯县
        /// </summary>
        public string Origin_County { get; set; }

        /// <summary> 
        /// 籍贯街道
        /// </summary>
        public string Origin_Street { get; set; }

        /// <summary> 
        /// 居住地址
        /// </summary>
        public string ResidentialAddress { get; set; }

        /// <summary> 
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary> 
        /// 家庭电话
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary> 
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary> 
        /// 紧急联系人
        /// </summary>
        public string EmergencyName { get; set; }

        /// <summary> 
        /// 紧急联系方式
        /// </summary>
        public string EmergencyPhone { get; set; }

        /// <summary> 
        /// 退伍证
        /// </summary>
        public string DischargeCertificate { get; set; }

        /// <summary> 
        /// 失业证
        /// </summary>
        public string UnemploymentCertificate { get; set; }

        /// <summary> 
        /// 毕业证
        /// </summary>
        public string DiplomaCertificate { get; set; }

        /// <summary> 
        /// 残疾证
        /// </summary>
        public string DisabilityCertificate { get; set; }

        /// <summary> 
        /// 是否购买保险
        /// </summary>
        public string Insure { get; set; }

        /// <summary> 
        /// 购买地
        /// </summary>
        public string InsureAddress { get; set; }

        /// <summary> 
        /// 社保编号
        /// </summary>
        public string InsureNumber { get; set; }

        /// <summary> 
        /// 是否孕期
        /// </summary>
        public string Ispregnancy { get; set; }

        /// <summary> 
        /// 是否医疗
        /// </summary>
        public string Istreatment { get; set; }

        /// <summary> 
        /// 是否兵役
        /// </summary>
        public string Ismilitary { get; set; }

        /// <summary> 
        /// 个人特长
        /// </summary>
        public string Specialty { get; set; }

        /// <summary> 
        /// 业余爱好
        /// </summary>
        public string Avocation { get; set; }

        /// <summary> 
        /// 择业优势
        /// </summary>
        public string Superiority { get; set; }

        /// <summary> 
        /// 用人部门意见
        /// </summary>
        public string DepartMentopinions { get; set; }

        /// <summary> 
        /// 公司意见
        /// </summary>
        public string CompanyOpinions { get; set; }

        /// <summary> 
        /// 工作状态
        /// </summary>
        public string StaffStatus { get; set; }

        /// <summary> 
        /// 部门
        /// </summary>
        public string DepartMent { get; set; }

        /// <summary> 
        /// 岗位
        /// </summary>
        public string PostName { get; set; }

        /// <summary> 
        /// 填表时间
        /// </summary>
        public DateTime RegTime { get; set; }

        /// <summary> 
        /// 上岗时间
        /// </summary>
        public DateTime WorkTime { get; set; }

        /// <summary> 
        /// 离职时间
        /// </summary>
        public DateTime WorkOffTime { get; set; }

        /// <summary> 
        /// 开始上岗(0否1是)
        /// </summary>
        public string Iswork { get; set; }

        /// <summary> 
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary> 
        /// 暂扣金额
        /// </summary>
        public decimal DurationaMount { get; set; }

        /// <summary> 
        /// 临时用工
        /// </summary>
        public string Istemp { get; set; }

        /// <summary> 
        /// 招聘ID
        /// </summary>
        public string RecruitMent_Tasks { get; set; }

        /// <summary> 
        /// 商家ID
        /// </summary>
        public string ProviderID { get; set; }

        /// <summary> 
        /// 公司ID
        /// </summary>
        public long CompanyID { get; set; }


    }
}
