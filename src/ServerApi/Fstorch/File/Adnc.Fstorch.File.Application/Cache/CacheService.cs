using Adnc.Infra.Redis.Caching;
using Adnc.Shared.Application.Caching;
using Microsoft.Extensions.Logging;

namespace Adnc.Fstorch.File.Application.Cache
{
    public sealed class CacheService : AbstractCacheService, ICachePreheatable
    {

        private readonly ILogger _logger;
        public CacheService(
            Lazy<ICacheProvider> cacheProvider,
            Lazy<IServiceProvider> serviceProvider,
            ILogger<CacheService> logger)
            : base(cacheProvider, serviceProvider)
        {
            _logger = logger;
        }

        public override async Task PreheatAsync() => await Task.CompletedTask;

        internal async Task SetMediaCheckIdToCacheAsync(string traceid, long fileId)
        {
            var cacheKey = ConcatCacheKey("adnc:fstorch:file:check", traceid);
            await CacheProvider.Value.SetAsync(cacheKey, fileId, TimeSpan.FromMinutes(30));
        }

        internal async Task<long> GetMediaCheckIdToCacheAsync(string traceid)
        {
            var cacheKey = ConcatCacheKey("adnc:fstorch:file:check", traceid);
            var cacheValue = await CacheProvider.Value.GetAsync<long>(cacheKey);
            return cacheValue.Value;
        }
    }
}
