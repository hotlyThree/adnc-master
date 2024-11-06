namespace Fstorch.AfterSales.Application.Dtos.Public
{
    /// <summary>
    /// 工程师预约预计序号
    /// </summary>
    public class CountEngineerAppointmentDto : InputDto
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 工程师ID
        /// </summary>
        public long StaffId { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime AppointmentDate { get; set; }


        /// <summary>
        /// 预约时段
        /// </summary>
        public string Appointmentperiod { get; set; }
    }
}
