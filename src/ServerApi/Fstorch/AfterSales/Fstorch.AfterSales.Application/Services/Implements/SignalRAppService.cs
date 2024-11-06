namespace Fstorch.AfterSales.Application.Services.Implements
{
    public class SignalRAppService : AbstractAppService, ISignalRAppService
    {
        private readonly CacheService _cacheService;

        public SignalRAppService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task BindSignalRUser(long companyid, string userid, string clientid)
        {
            await _cacheService.BindUserFromCacheAsync(companyid, userid, clientid);
        }

        public async Task UnBindSignalRUser(string clientid)
        {
            await _cacheService.UnBindUserFromCacheAsync(clientid);
        }
    }
}
