namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    [Serializable]
    /// <summary> 
    /// 收支项目编码
    /// </summary>
    public class Bm_Financial_ProjectRto 
    {

        /// <summary> 
        /// 企业编号
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary> 
        /// 收支项目
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary> 
        /// 项目大类
        /// </summary>
        public string ProjectType { get; set; }

        /// <summary> 
        /// 项目说明
        /// </summary>
        public string ProjectMemo { get; set; }

        /// <summary> 
        /// 显示顺序
        /// </summary>
        public int DisplayNumber { get; set; } = 0;

        /// <summary> 
        /// 收支模式 A应收 B应付 C实收实付
        /// </summary>
        public string ProjectModel { get; set; }


    }
}
