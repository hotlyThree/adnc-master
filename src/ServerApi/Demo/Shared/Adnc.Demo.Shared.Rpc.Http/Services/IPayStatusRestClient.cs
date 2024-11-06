using Adnc.Shared.Rpc.Http;

namespace Adnc.Demo.Shared.Rpc.Http.Services
{
    public interface IPayStatusRestClient : IRestClient
    {
        /// <summary>
        /// 第二次主动推送支付状态信息
        /// </summary>
        /// <returns></returns>
        [Post("/Pay/TimeTask_TwoSendAsync")]
        Task<ApiResponse<string>> TimeTask_TwoSendAsync();


        /// <summary>
        /// 第三次主动推送支付状态信息
        /// </summary>
        /// <returns></returns>
        [Post("/Pay/TimeTask_ThreeSendAsync")]
        Task<ApiResponse<string>> TimeTask_ThreeSendAsync();
    }
}
