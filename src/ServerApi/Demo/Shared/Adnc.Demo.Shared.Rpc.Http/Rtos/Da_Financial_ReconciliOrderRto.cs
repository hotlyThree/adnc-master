
namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    /// <summary> 
    /// 往来对账业务明细表
    /// </summary>
    public class Da_Financial_ReconciliOrderRto
    {
        /// <summary> 
        /// 商品ID
        /// </summary>
        public long ProductCode { get; set; }

        /// <summary> 
        /// 业务描述
        /// </summary>
        public string Description { get; set; }

        /// <summary> 
        /// 预付款金额
        /// </summary>
        public decimal AdvanceMoney { get; set; } = 0;

        /// <summary> 
        /// 返利款金额
        /// </summary>
        public decimal RebateMoney { get; set; } = 0;

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderMoney { get; set; } = 0;


    }
}
