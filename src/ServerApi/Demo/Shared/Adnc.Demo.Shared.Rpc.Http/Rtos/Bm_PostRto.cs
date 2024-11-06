
namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    [Serializable]
    public class Bm_PostRto
    {

        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary> 
        /// 工作职务
        /// </summary>
        public string PostName { get; set; } = string.Empty;

        /// <summary> 
        /// 显示序号
        /// </summary>
        public int Displaynumber { get; set; } = 0;


    }
}
