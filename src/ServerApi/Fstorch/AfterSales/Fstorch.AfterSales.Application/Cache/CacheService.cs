
using Adnc.Demo.Shared.Const.Entity;
using Adnc.Infra.Redis;
using Adnc.Shared;

namespace Fstorch.AfterSales.Application.Cache
{
    public class CacheService : AbstractCacheService, ICachePreheatable
    {
        private readonly ILogger _logger;
        private Lazy<IDistributedLocker> _dictributeLocker;
        public CacheService(
            Lazy<ICacheProvider> cacheProvider, 
            Lazy<IServiceProvider> serviceProvider,
            Lazy<IDistributedLocker> dictributeLocker,
            ILogger<CacheService> logger) 
            : base(cacheProvider, serviceProvider)
        {
            _dictributeLocker = dictributeLocker;
            _logger = logger;
        }

        public override async Task PreheatAsync()
        {
            
        }

        internal async Task BindUserFromCacheAsync(long companyid, string userid, string clientid)
        {
            var cacheKey = $"{CachingConsts.SignalRClientListKey}";
            var clientCache = await CacheProvider.Value.GetAsync<List<SignalRClient>>(cacheKey);
            var list = new List<SignalRClient>();
            if (clientCache.HasValue)
                list = clientCache.Value;
            list.Add(new SignalRClient { CompanyId = companyid, UserId = userid, ClientId = clientid});
            await CacheProvider.Value.SetAsync(cacheKey, list, TimeSpan.FromSeconds(GeneralConsts.OneYear));
        }

        internal async Task UnBindUserFromCacheAsync(string clientid)
        {
            var cacheKey = $"{CachingConsts.SignalRClientListKey}";
            var clientCache = await CacheProvider.Value.GetAsync<List<SignalRClient>>(cacheKey);
            var list = new List<SignalRClient>();
            if (clientCache.HasValue)
                list = clientCache.Value;
            list.RemoveAll(x => x.ClientId.Equals(clientid));
            await CacheProvider.Value.SetAsync(cacheKey, list, TimeSpan.FromSeconds(GeneralConsts.OneYear));
        }

        internal async Task<List<SignalRClient>> GetClientsFromCacheAsync()
        {
            var cacheKey = $"{CachingConsts.SignalRClientListKey}";
            var clientCache = await CacheProvider.Value.GetAsync<List<SignalRClient>>(cacheKey);
            var list = new List<SignalRClient>();
            if (clientCache.HasValue)
                list = clientCache.Value;
            return list;
        }

        /// <summary>
        /// 售后自定义工单号获取
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<string> GenerateWorkOrderCodeForTodayAsync(long companyid)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.ServiceWorkOrderIdKeyPrefix, companyid);
            var exists = await CacheProvider.Value.ExistsAsync(cacheKey);
            if (exists) 
            {
                await Task.Delay(500);
                await GenerateWorkOrderCodeForTodayAsync(companyid);
            }
            var locker = await _dictributeLocker.Value.LockAsync(cacheKey);
            if (!locker.Success)
            {
                await Task.Delay(500);
                await GenerateWorkOrderCodeForTodayAsync(companyid);
            }
            try
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceOrderRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_Service_Order>>();
                var today = DateTime.Now.ToString("yyyyMMdd");
                var maxServiceOrderId = await serviceOrderRepository
                    .Where(x => x.CompanyID == companyid && !string.IsNullOrEmpty(x.ServiceOrderID) && x.ServiceOrderID.Contains(today) && x.ServiceOrderID.Length >= 13)
                    .MaxAsync(x => (x.ServiceOrderID.Substring(8)));
                var maxOrderId = maxServiceOrderId.IsNotNullOrWhiteSpace() ? int.Parse(maxServiceOrderId) :  0;
                // 生成新的工单号
                string newWorkOrderCode = $"{DateTime.Now.ToString("yyyyMMdd")}{(maxOrderId + 1):D5}";
                return newWorkOrderCode;
            }
            catch(Exception ex)
            {
                _logger.LogError("生成新工单号异常:{message}", ex.Message);
                return "";
            }
        }


        /// <summary>
        /// 企业信息来源
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<InformationSourceDto>> GetInfoSourceListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.InfoSourceListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var infoSourceReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Information_Source>>();
                var infoSourceList = await infoSourceReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<InformationSourceDto>>(infoSourceList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<InformationSourceDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业用户类型
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<UserTypeDto>> GetUserTypeListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.UserTypeListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var userTypeReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_User_Type>>();
                var userTypeList = await userTypeReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<UserTypeDto>>(userTypeList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<UserTypeDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务性质
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceNatureDto>> GetServiceNatureListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceNatureListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceNatureReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Nature>>();
                var serviceNatureList = await serviceNatureReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<ServiceNatureDto>>(serviceNatureList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceNatureDto>();
            }
            return cacheValue.Value;
        }


        /// <summary>
        /// 企业服务类型
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceTypeDto>> GetServiceTypeListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceTypeListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceTypeReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Type>>();
                var serviceTypeList = await serviceTypeReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<ServiceTypeDto>>(serviceTypeList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceTypeDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业销售商场
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<MarketDto>> GetMarketListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.MarketListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var marketReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Buy_Market>>();
                var marketList = await marketReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<MarketDto>>(marketList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<MarketDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务品牌
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceBrandDto>> GetServiceBrandListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceBrandListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceBrandReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Brand>>();
                var serviceBrandList = await serviceBrandReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<ServiceBrandDto>>(serviceBrandList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceBrandDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业品牌内容
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<BrandContentDto>> GetBrandContentListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.BrandContentListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var brandContentReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Brand_Content>>();
                var brandContentList = await brandContentReposity.Where(x => x.CompanyId == companyid)
                .OrderBy(x => x.ServiceBrandId)
                .OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<BrandContentDto>>(brandContentList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<BrandContentDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业结算代码
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<SettlementCodeDto>> GetSettlementListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.SettlementListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var settlementReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Settlement_Code>>();
                var settlementList = await settlementReposity.Where(x => x.CompanyId == companyid).ToListAsync();
                return Mapper.Value.Map<List<SettlementCodeDto>>(settlementList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<SettlementCodeDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务需求
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceRequestDto>> GetServiceRequestListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceRequestListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceRequestReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Request>>();
                var serviceRequestList = await serviceRequestReposity.Where(x => x.CompanyId == companyid).ToListAsync();
                return Mapper.Value.Map<List<ServiceRequestDto>>(serviceRequestList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceRequestDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业第一个服务需求
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<ServiceRequestDto> GetServiceRequestListFromCacheAsyncTest(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceRequestListCacheKeyPrefix + ":test", companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceRequestReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Request>>();
                var serviceRequestList = await serviceRequestReposity.FetchAsync(x => x, x => x.CompanyId == companyid);
                return Mapper.Value.Map<ServiceRequestDto>(serviceRequestList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new ServiceRequestDto();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务故障原因
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceCauseDto>> GetServiceCauseListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceCauseListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceCauseReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Cause>>();
                var serviceCauseList = await serviceCauseReposity.Where(x => x.CompanyId == companyid).ToListAsync();
                return Mapper.Value.Map<List<ServiceCauseDto>>(serviceCauseList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceCauseDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务回访
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceFollowDto>> GetServiceFollowListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceFollowListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceFollowReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Follow>>();
                var serviceFollowList = await serviceFollowReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<ServiceFollowDto>>(serviceFollowList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceFollowDto>();
            }
            return cacheValue.Value;
        }

        /// <summary>
        /// 企业服务标注
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        internal async Task<List<ServiceTagDto>> GetServiceTagListFromCacheAsync(long companyid)
        {
            var CacheKey = ConcatCacheKey(CachingConsts.ServiceTagListCacheKeyPrefix, companyid);
            var cacheValue = await CacheProvider.Value.GetAsync(CacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var serviceTagReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Bm_Service_Tag>>();
                var serviceTagList = await serviceTagReposity.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
                return Mapper.Value.Map<List<ServiceTagDto>>(serviceTagList);
            }, TimeSpan.FromMinutes(5));
            // 处理 cacheValue.HasValue 为 false 的情况，返回一个空列表
            if (!cacheValue.HasValue)
            {
                return new List<ServiceTagDto>();
            }
            return cacheValue.Value;
        }


    }
}
