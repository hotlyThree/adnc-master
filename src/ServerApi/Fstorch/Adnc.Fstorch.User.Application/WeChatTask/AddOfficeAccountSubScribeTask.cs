using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Adnc.Fstorch.User.Application.WeChatTask
{
    /// <summary>
    /// 微信公众号用户列表回写任务
    /// </summary>
    public class AddOfficeAccountSubScribeTask : BackgroundService
    {
        private readonly TimeSpan timeSpan = TimeSpan.FromMinutes(30);
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AddOfficeAccountSubScribeTask> _logger;
        private readonly Lazy<IServiceProvider> _serviceProvider;
        private readonly CacheService _cacheService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddOfficeAccountSubScribeTask(IHttpClientFactory httpClientFactory, ILogger<AddOfficeAccountSubScribeTask> logger, Lazy<IServiceProvider> serviceProvider,
            CacheService cacheService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _cacheService = cacheService;
        }

        /// <summary>
        /// 启动事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await CreateSubScribe();
        }

        /// <summary>
        /// 停止事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 计划任务
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CreateSubScribe();
                await Task.Delay(timeSpan, stoppingToken);
            }
        }

        private async Task CreateSubScribe()
        {
            using (var request = _httpClientFactory.CreateClient())
            {
                var access_token = await _cacheService.GetAccessTokenAsync();
                if (access_token.IsNotNullOrWhiteSpace())
                {
                    await GetUsersAsync(request, access_token);
                }
            }
        }

        private async Task GetUsersAsync(HttpClient request, string access_token)
        {
            string next_openid = string.Empty;
            using var scope = _serviceProvider.Value.CreateScope();
            var _subRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_SubScribe>>();
            var _subDtRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_Subscribe_Detail>>();
            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var subscribe = await _subRepository.GetAll().ToListAsync();
            if (subscribe.Count > 0)
            {
                next_openid = subscribe.OrderByDescending(x => x.Id).FirstOrDefault().OpenId;
            }
            string user_request_uri = $"https://api.weixin.qq.com/cgi-bin/user/get?access_token={access_token}&next_openid={next_openid}";
            var user_response = await request.GetAsync(user_request_uri);
            user_response.EnsureSuccessStatusCode();
            var user_responseJson = await user_response.Content.ReadAsStringAsync();
            var user_responseObject = JsonConvert.DeserializeObject<JObject>(user_responseJson);
            //token被解忧服务器覆盖，重试一次刷新
            if (user_responseObject["errcode"] != null && user_responseObject["errcode"].ToString().Equals("40001"))
            {
                access_token = await _cacheService.GetAccessTokenAsync(true);
                await GetUsersAsync(request, access_token);
                return;
            }
            try
            {
                _unitOfWork.BeginTransaction();
                int count = int.Parse(user_responseObject["count"].ToString());
                if(count > 0)
                {
                    var openids = (JArray)user_responseObject["data"]["openid"];
                    List<Da_SubScribe> subs = new();
                    //所有用户列表
                    foreach (var openid in openids)
                    {
                        var sub = new Da_SubScribe
                        {
                            Id = IdGenerater.GetNextId(),
                            CreateTime = DateTime.Now,
                            OpenId = openid.ToString()
                        };
                        subs.Add(sub);
                    }
                    if (subs.Count > 0)
                    {
                        await _subRepository.InsertRangeAsync(subs);
                    }
                    if (openids.Count > 0)
                    {
                        int totalItems = openids.Count;
                        int itemsPerBatch = 100;
                        int batches = totalItems / itemsPerBatch + (totalItems % itemsPerBatch > 0 ? 1 : 0);
                        //按批次处理  一次处理微信接口支持最大值100
                        string user_info_request_uri = $"https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={access_token}";
                        for (int i = 0; i < batches; i++)
                        {
                            var startIndex = i * itemsPerBatch;
                            var batch = new JArray(openids.Skip(startIndex).Take(itemsPerBatch));
                            var list = new List<object>();
                            foreach (var item in batch)
                                list.Add(new { openid = item.ToString() });
                            var dic = new Dictionary<string, object>
                        {
                            { "user_list", list }
                        };
                            var content = new StringContent(JsonConvert.SerializeObject(dic));
                            var user_info_response = await request.PostAsync(user_info_request_uri, content);
                            user_info_response.EnsureSuccessStatusCode();
                            var user_info_responseJson = await user_info_response.Content.ReadAsStringAsync();
                            var user_info_responseObject = JsonConvert.DeserializeObject<JObject>(user_info_responseJson);
                            var userInfoList = (JArray)user_info_responseObject["user_info_list"];
                            List<Da_Subscribe_Detail> subDts = new();
                            foreach (var userinfo in userInfoList)
                            {
                                var subDt = new Da_Subscribe_Detail
                                {
                                    Id = IdGenerater.GetNextId(),
                                    OpenId = userinfo["openid"].ToString(),
                                    UnionId = userinfo["unionid"].ToString(),
                                    SubScribe = int.Parse(userinfo["subscribe"].ToString())
                                };
                                subDts.Add(subDt);
                            }
                            if (subDts.Count > 0)
                                await _subDtRepository.InsertRangeAsync(subDts);
                        }
                    }
                }
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"获取公众号用户列表>>>{user_responseJson}");
                await _unitOfWork.RollbackAsync();
            }
            finally { _unitOfWork.Dispose(); }

        }
    }
}
