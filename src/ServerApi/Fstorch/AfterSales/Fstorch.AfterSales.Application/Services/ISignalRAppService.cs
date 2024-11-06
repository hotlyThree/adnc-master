namespace Fstorch.AfterSales.Application.Services
{
    public interface ISignalRAppService : IAppService
    {
        /// <summary>
        /// 绑定客户端
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="userid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        Task BindSignalRUser(long companyid, string userid, string clientid);

        /// <summary>
        /// 解绑客户端
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        Task UnBindSignalRUser(string clientid);
    }
}
