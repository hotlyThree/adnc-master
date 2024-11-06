
using System.Text.Json;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class AccountAppService : AbstractAppService, IAccountAppService
    {
        private readonly IEfRepository<Sys_Account> _accountRepository;
        private readonly IEfRepository<Da_userinfo> _userinfoRepository;
        private readonly IEfRepository<Sys_Program> _programRepository;
        private readonly IEfRepository<Da_Card_Giveaway> _giveAwayRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CacheService _cacheService;
        public AccountAppService(IEfRepository<Sys_Account> accountRepository, CacheService cacheService, IEfRepository<Sys_Program> programRepository,
            IHttpClientFactory httpClientFactory, IEfRepository<Da_userinfo> userinfoRepository, IEfRepository<Da_Card_Giveaway> giveAwayRepository)
        {
            _accountRepository = accountRepository;
            _cacheService = cacheService;
            _programRepository = programRepository;
            _httpClientFactory = httpClientFactory;
            _userinfoRepository = userinfoRepository;
            _giveAwayRepository = giveAwayRepository;
        }
        
        public async Task<AppSrvResult<string>> ChangePhotoAsync(AccountPhotoUpdationDto input)
        {
            input.TrimStringFields();
            if (!(await _cacheService.GetAllAccountFromCacheAsync()).Exists(x => x.Id == input.Id))
                return Problem(HttpStatusCode.BadRequest, "账号不存在");
            if (input.Type.Equals("photo"))
            {
                await _accountRepository.UpdateAsync(new Sys_Account { Id = input.Id, Photo = input.Photo }, UpdatingProps<Sys_Account>(x => x.Photo));
                //await _userinfoRepository.UpdateRangeAsync(x => x.Aid == input.Id, x => new Da_userinfo { Photo = input.Photo });
            }
            else if (input.Type.Equals("wechat"))
                await _accountRepository.UpdateAsync(new Sys_Account { Id = input.Id, Wechatvideo = input.Photo }, UpdatingProps<Sys_Account>(x => x.Wechatvideo));
            else if (input.Type.Equals("tiktok"))
                await _accountRepository.UpdateAsync(new Sys_Account { Id = input.Id, Tiktokvideo = input.Photo }, UpdatingProps<Sys_Account>(x => x.Tiktokvideo));
            return input.Photo;
        }

        public async Task<AppSrvResult<long>> CreateAsync(AccountCreationDto input)
        {
            input.TrimStringFields();
            if(await _accountRepository.AnyAsync(x => x.Openid == input.Openid))
                return Problem(HttpStatusCode.BadRequest, "账号已经存在");
            var account = Mapper.Map<Sys_Account>(input);
            account.Id = IdGenerater.GetNextId();
            account.CreateTime = DateTime.Now;
            //推荐码含会员
            if(account.Memberid > 0)
            {
                var exists = await _giveAwayRepository.AnyAsync(x => x.Referralcode.Equals(input.Referralcode) && (x.Usecode > 0 || x.Expirationdate < DateTime.Now));
                if (exists)
                    return Problem(HttpStatusCode.BadRequest, "注册失败,推荐码已失效或不存在");
                account.Expirationdate = DateTime.Now.AddYears(1);
                var giveAway = await _giveAwayRepository.FetchAsync(x => x, x => x.Referralcode.Equals(input.Referralcode), noTracking : false);
                giveAway.Usedate = DateTime.Now;
                giveAway.Usecode = account.Id;
                await _giveAwayRepository.UpdateAsync(giveAway);
            }
            await _accountRepository.InsertAsync(account);
            //创建账号默认生成自己的推荐码
            var myGiveAway = new Da_Card_Giveaway
            {
                Id = IdGenerater.GetNextId(),
                Userid = account.Id,
                Referralcode = Guid.NewGuid().ToString()
            };
            await _giveAwayRepository.InsertAsync(myGiveAway);
            return account.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _accountRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<AccountDto>> GetAccountAsync(string openid)
        {
            var accountList = await _cacheService.GetAllAccountFromCacheAsync();
            if(accountList == null)
                return Problem(HttpStatusCode.NotFound, "请先注册账号");
            /*var account = await _accountRepository.FetchAsync(u => u, x => x.Openid == openid);
            if (account == null)
                return Problem(HttpStatusCode.BadRequest, "账号不存在");*/
            var account = accountList.Where(x => x.Openid == openid).FirstOrDefault();
            if (account == null)
                return Problem(HttpStatusCode.NotFound, "账号不存在");
            var code = await _giveAwayRepository.FetchAsync(x => x.Referralcode, x => x.Userid == account.Id && x.Memberid == 0);
            account.Referralcode = code;
            return account;
        }

        public async Task<AppSrvResult<List<AccountDto>>> GetAccountList()
        {
            return await _cacheService.GetAllAccountFromCacheAsync();
        }

        public async Task<AppSrvResult<Dictionary<string, object>>> GetAccountOpenIdAsync(AccountOpenIdDto input)
        {
            var app = await _programRepository.FetchAsync(x => x, x => x.Appid.Equals(input.Appid));
            if (app == null)
                return Problem(HttpStatusCode.NotFound,"appid不存在");

            var requestUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={app.Appid}&secret={app.AppSecret}&js_code={input.Code}&grant_type=authorization_code";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            //string openId = responseObject["openid"].ToString();
            return responseObject;
        }

        public async Task<AppSrvResult> UpdateAsync(long id, AccountUpdationDto input)
        {
            input.TrimStringFields();
            var accountDto= (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == id);
            if (accountDto == null)
                return Problem(HttpStatusCode.BadRequest, "您还未注册账号");
            var account = Mapper.Map<Sys_Account>(input);
            account.Id = id;
            var updatingProps = UpdatingProps<Sys_Account>(x => x.Username, x => x.Phone, x => x.Wechatvideo, x => x.Tiktokvideo, x => x.UnionId, x => x.Signature);
            await _accountRepository.UpdateAsync(account, updatingProps);
            return AppSrvResult();
        }
    }
}
