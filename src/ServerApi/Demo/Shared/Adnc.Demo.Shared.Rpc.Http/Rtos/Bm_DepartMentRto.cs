
namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    [Serializable]
    public class Bm_DepartMentRto 
    {

        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary> 
        /// 工作部门
        /// </summary>
        public string DepartMentName { get; set; }=string.Empty;

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; } = 0;



    }
}
