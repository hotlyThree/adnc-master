using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Cache
{
    public sealed class CacheService : AbstractCacheService, ICachePreheatable
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public CacheService(
            Lazy<ICacheProvider> cacheProvider,
            Lazy<IServiceProvider> serviceProvider,
            IHttpClientFactory httpClientFactory,
            ILogger<CacheService> logger)
            : base(cacheProvider, serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public override async Task PreheatAsync()
        {
            await GetAllAccountFromCacheAsync();
            await GetAllCompanysFromCacheAsync();
            await GetStyleListFromCacheAsync();
            await GetBackGroundListFromCacheAsync();
        }


        internal async Task<string?> GetAccessTokenAsync(string appid)
        {
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.MiniProgramAccessTokenCacheKey, async () =>
            {
                //名片小程序
                using var scope = ServiceProvider.Value.CreateScope();
                var _programRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Sys_Program>>();
                var appSecret = await _programRepository.FetchAsync(x => x.AppSecret, x => x.Appid.Equals(appid));
                using var request = _httpClientFactory.CreateClient();
                string token_request_uri = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={appSecret}";
                var token_response = await request.GetAsync(token_request_uri);
                token_response.EnsureSuccessStatusCode();
                var token_responseJson = await token_response.Content.ReadAsStringAsync();
                var token_responseObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(token_responseJson);
                try
                {
                    string? access_token = token_responseObject["access_token"].ToString();
                    if (access_token.IsNullOrWhiteSpace())
                        access_token = "";
                    return access_token;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"获取小程序Access_Token>>>{token_responseJson}");
                    return "";
                }
            }, TimeSpan.FromMinutes(20));
            return cacheValue.Value;
        }

        internal async Task<string?> GetAccessTokenAsync(bool refresh = false)
        {
            if (refresh)
                await CacheProvider.Value.RemoveAsync(CachingConsts.OfficeAccountAccessTokenCacheKey);
            TimeSpan total = TimeSpan.FromMinutes(10);
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.OfficeAccountAccessTokenCacheKey, async () =>
            {
                using var request = _httpClientFactory.CreateClient();
                string token_request_uri = $"https://wx.fstorch.com/classservice.asmx/weixin_Refresh_Token";
                var dic = new Dictionary<string, string>
                {
                    {"wxid", "36" }
                };
                var formdata = new FormUrlEncodedContent(dic);
                //var content = new StringContent(JsonConvert.SerializeObject(parm));
                var token_response = await request.PostAsync(token_request_uri, formdata);
                token_response.EnsureSuccessStatusCode();
                var token_responseString = await token_response.Content.ReadAsStringAsync();
                string token = "";
                if (token_responseString.IsNotNullOrWhiteSpace() && token_responseString.Contains("success"))
                {
                    var arr = token_responseString.Split("|");
                    var createtime = DateTime.Parse(arr[1]);
                    token = arr[2].Replace("</string>", "");
                    total = DateTime.Now - createtime;
                }
                try
                {
                    //string? access_token = token_responseObject["access_token"].ToString();
                    if (token.IsNullOrWhiteSpace())
                        token = "";
                    return token;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"获取公众号Access_Token>>>{token_responseString}");
                    return "";
                }
            }, total);
            return cacheValue.Value;
        }

        /*internal async Task<string?> GetAccessTokenAsync(bool refresh = false)
        {
            if (refresh)
                await CacheProvider.Value.RemoveAsync(CachingConsts.OfficeAccountAccessTokenCacheKey);
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.OfficeAccountAccessTokenCacheKey, async () =>
            {
                //正火科技公众号
                string appid = "wx7a22be959799c6bf";
                string appSecret = "d65e1d8311f08c70b08578f6a2adb6e1";
                using var request = _httpClientFactory.CreateClient();
                string token_request_uri = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={appSecret}";
                var token_response = await request.GetAsync(token_request_uri);
                token_response.EnsureSuccessStatusCode();
                var token_responseJson = await token_response.Content.ReadAsStringAsync();
                var token_responseObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(token_responseJson);
                try
                {
                    string? access_token = token_responseObject["access_token"].ToString();
                    if (access_token.IsNullOrWhiteSpace())
                        access_token = "";
                    return access_token;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"获取公众号Access_Token>>>{token_responseJson}");
                    return "";
                }
            }, TimeSpan.FromHours(2));
            return cacheValue.Value;
        }*/

        internal async Task<List<StyleDto>> GetStyleListFromCacheAsync()
        {
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.StyleListCacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var styleReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Sys_Style>>();
                var styleList = await styleReposity.GetAll().ToListAsync();
                return Mapper.Value.Map<List<StyleDto>>(styleList);
            }, TimeSpan.FromSeconds(30));
            return cacheValue.Value;
        }

        internal async Task<List<BackGroundDto>> GetBackGroundListFromCacheAsync()
        {
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.BackGroundListCacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var backReposity = scope.ServiceProvider.GetRequiredService<IEfRepository<Sys_BackGround>>();
                var backList = await backReposity.GetAll().ToListAsync();
                return Mapper.Value.Map<List<BackGroundDto>>(backList);
            }, TimeSpan.FromSeconds(30));
            return cacheValue.Value;
        }

        internal async Task SetCommentsTreeToCacheAsync(long id, List<CommentSimpleTreeListDto> value)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.CommentsTreeListKeyPrefix, id);
            await CacheProvider.Value.SetAsync(cacheKey, value, TimeSpan.FromSeconds(GeneralConsts.OneHour));
        }

        internal async Task<List<CommentSimpleTreeListDto>> GetCommentsTreeFromCacheAsync(long msgid, long reader)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.CommentsTreeListKeyPrefix, msgid);
            var cacheValue = await CacheProvider.Value.GetAsync(cacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var _msgRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_msg>>();
                var _comRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_Comments>>();
                var msgRep = _msgRepository.Where(x => x.Id == msgid);
                var commRep = _comRepository.Where(x => x.Msgid == msgid);
                var commentDtos = await commRep.Join(msgRep, x => x.Msgid, y => y.Id, (x, y) => new CommentDto
                {
                    Id = x.Id,
                    Reviewer = x.Reviewer,
                    Content = x.Content,
                    Parentid = x.Parentid,
                    //Owner = y.Releaseid == reader || reader == x.Reviewer ? true : false,
                    Createtime = x.Createtime,
                    Msgid = y.Id,
                    Releaseid = y.Releaseid
                })
                .OrderByDescending(x => x.Createtime)
                .ToListAsync();

                var account = await GetAllAccountFromCacheAsync();
                //默认从最上层ID为 0开始
                var commentTreeListDtos = GetCommentTree(commentDtos, 0);

                //递归 返回评论树
                List<CommentSimpleTreeListDto> GetCommentTree(List<CommentDto> list, long parentId)
                {
                    var childComms = list.Where(x => x.Parentid == parentId).ToList();
                    if (childComms.Count <= 0)
                        return new List<CommentSimpleTreeListDto>();
                    return childComms.Select(comment => new CommentSimpleTreeListDto
                    {
                        Id = comment.Id,
                        Msgid = comment.Msgid,
                        Releaseid = comment.Releaseid,
                        Content = comment.Content,
                        Parentid = comment.Parentid,
                        Reviewer = comment.Reviewer,
                        UserName = account.FirstOrDefault(x => x.Id == comment.Reviewer) != null ? account.FirstOrDefault(x => x.Id == comment.Reviewer).Username
                        : "",
                        NickName = account.FirstOrDefault(x => x.Id == comment.Reviewer) != null ? account.FirstOrDefault(x => x.Id == comment.Reviewer).Openname
                        : "",
                        Photo = account.FirstOrDefault(x => x.Id == comment.Reviewer) != null ? account.FirstOrDefault(x => x.Id == comment.Reviewer).Photo
                        : "",
                        //Owner = comment.Reviewer == reader || reader == comment.Releaseid ? true : false,
                        Createtime = comment.Createtime,
                        Children = GetCommentTree(list, comment.Id)
                    })
                    .OrderByDescending(x => x.Createtime)
                    .ToList();
                }
                return commentTreeListDtos;
            }, TimeSpan.FromSeconds(GeneralConsts.OneHour));
            return cacheValue.Value;
        }

        internal async Task SetReadCountToCacheAsync(long releaseid, int count)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.ReadCountKeyPrefix, releaseid);
            await CacheProvider.Value.SetAsync(cacheKey, count, TimeSpan.FromSeconds(GeneralConsts.OneHour));
        }

        internal async Task SetOrderNoToCacheAsync(long id, string type)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.AccountMemberPayTypeCacheKey, id);
            await CacheProvider.Value.SetAsync(cacheKey, type, TimeSpan.FromMinutes(10));
        }

        internal async Task<string> GetOrderTypeFromCacheAsync(long id)
        {
            var cacheKey = ConcatCacheKey(CachingConsts.AccountMemberPayTypeCacheKey, id);
            var cacheValue = await CacheProvider.Value.GetAsync<string>(cacheKey);
            return cacheValue.Value;
        }

        internal async Task<int> GetReadCountFromCacheAsync(long releaseid, bool today = false)
        {
            var cacheKey = "";
            var readCount = 0;
            if (!today)
            {
                cacheKey = ConcatCacheKey(CachingConsts.ReadCountKeyPrefix, releaseid);
                var cacheValue = await CacheProvider.Value.GetAsync(cacheKey, async () =>
                {
                    using var scope = ServiceProvider.Value.CreateScope();
                    var msgRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_msg>>();
                    var expression = ExpressionCreator
                     .New<Da_msg>()
                     .And(x => x.Releaseid == releaseid);
                    readCount = await msgRepository
                    .Where(expression)
                    .GroupBy(x => x.Releaseid)
                    .Select(x => x.Sum(x => x.Readnum))
                    .FirstOrDefaultAsync();
                    //var count = (await msgRepository.FetchAsync(x => x.Readnum, x => x.Releaseid == releaseid));
                    return readCount;
                }, TimeSpan.FromMinutes(10));
                return cacheValue.Value;
            }
            else
            {
                cacheKey = ConcatCacheKey(CachingConsts.ReadCountTodayKeyPrefix, releaseid);
                var cacheValue = await CacheProvider.Value.GetAsync(cacheKey, async () =>
                {
                    using var scope = ServiceProvider.Value.CreateScope();
                    var msgRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_msg>>();
                    var readRepository = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_ReadDetail>>();
                    var todayStart = DateTime.Today;
                    var msgQ = msgRepository.Where(x => x.Releaseid == releaseid);
                    var readQ = readRepository.Where(x => x.Readtime >= todayStart && x.Readtime <= todayStart.AddDays(1).AddTicks(-1));
                    readCount = await msgQ.Join(readQ, x => x.Id, y => y.Msgid, (x, y) => y)
                    .GroupBy(y => y.Msgid)
                    .Select(y => y.Count())
                    .FirstOrDefaultAsync();
                    //var count = (await msgRepository.FetchAsync(x => x.Readnum, x => x.Releaseid == releaseid));
                    return readCount;
                }, TimeSpan.FromMinutes(10));
                return cacheValue.Value;
            }
        }


        internal async Task<int> GetCardCaseCountFromCacheAsync(long aid, bool today = false)
        {
            var cacheKey = "";
            if (!today)
                cacheKey = ConcatCacheKey(CachingConsts.CardCaseCountKeyPrefix, aid);
            else
                cacheKey = ConcatCacheKey(CachingConsts.CardCaseCountTodayKeyPrefix, aid);
            var cacheValue = await CacheProvider.Value.GetAsync(cacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var cardRepsitory = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_cardinfo>>();
                var userRepsitory = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_userinfo>>();
                var todayStart = DateTime.Today;
                var userids = await userRepsitory.Where(x => x.Aid == aid).Select(x => x.Id).ToArrayAsync();
                if (userids.Length == 0)
                    return 0;
                var expression = ExpressionCreator
                .New<Da_cardinfo>()
                .And(x => userids.Contains(x.Uid) && x.Addtype.Equals("B"))
                .AndIf(today, x => x.Createtime >= todayStart && x.Createtime <= todayStart.AddDays(1).AddTicks(-1));
                var cardCount = await cardRepsitory
                .Where(expression)
                .GroupBy(x => x.Aid)
                .CountAsync();
                return cardCount;
            }, TimeSpan.FromMinutes(10));
            return cacheValue.Value;
        }

        internal async Task<List<CompanyDto>> GetAllCompanysFromCacheAsync()
        {
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.CompanyListCacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var cmpReponsitory = scope.ServiceProvider.GetRequiredService<IEfRepository<Da_companyinfo>>();
                var allcmps = await cmpReponsitory.GetAll().ToListAsync();
                return Mapper.Value.Map<List<CompanyDto>>(allcmps);
            }, TimeSpan.FromSeconds(GeneralConsts.OneYear));
            return cacheValue.Value;
        }


        internal async Task<List<AccountDto>> GetAllAccountFromCacheAsync()
        {
            var cacheValue = await CacheProvider.Value.GetAsync(CachingConsts.AccountListCacheKey, async () =>
            {
                using var scope = ServiceProvider.Value.CreateScope();
                var accountReponsitory = scope.ServiceProvider.GetRequiredService<IEfRepository<Sys_Account>>();
                var allaccount = await accountReponsitory.GetAll<Sys_Account>(writeDb: true).OrderByDescending(x => x.CreateTime).ToListAsync();
                return Mapper.Value.Map<List<AccountDto>>(allaccount);
            }, TimeSpan.FromSeconds(GeneralConsts.OneYear));
            return cacheValue.Value;
        }
    }
}
