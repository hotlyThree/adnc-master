using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class CustomerAppService : AbstractAppService, ICustomerAppService
    {
        private readonly IEfRepository<Bm_Member_Type> _memberRepository;
        private readonly IEfRepository<Da_Card_Giveaway> _giveawayRepository;
        private readonly IEfRepository<Sys_Account> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CacheService _cacheService;
        public CustomerAppService(IEfRepository<Bm_Member_Type> memberRepository, IEfRepository<Da_Card_Giveaway> giveawayRepository, CacheService cacheService, IEfRepository<Sys_Account> accountRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _giveawayRepository = giveawayRepository;
            _accountRepository = accountRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
        }
        public async Task<AppSrvResult<long>> CreateAsync(MemberTypeCreationDto input)
        {
            input.TrimStringFields();
            //会员类型+会员名称不重复验证
            var exists = await _memberRepository.AnyAsync(x => x.Membertype.Equals(input.Membertype) && x.Membername.Equals(input.Membername));
            if (exists)
                return Problem(HttpStatusCode.BadRequest, "不能创建类型相同并且名称相同的会员卡");
            var memberType = Mapper.Map<Bm_Member_Type>(input);
            memberType.Id = IdGenerater.GetNextId();
            await _memberRepository.InsertAsync(memberType);
            return memberType.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _memberRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<PageModelDto<MemberTypeDto>> GetPagedAsync(MemberTypeSearchPagedDto search)
        {
            search.TrimStringFields();
            var express = ExpressionCreator
                .New<Bm_Member_Type>()
                .AndIf(search.Id > 0, x => x.Id == search.Id)
                .AndIf(search.Membername.IsNotNullOrWhiteSpace(), x => x.Membername.Contains(search.Membername))
                .AndIf(search.Memberdescribe.IsNotNullOrWhiteSpace(), x => x.Memberdescribe.Contains(search.Memberdescribe))
                .AndIf(search.Membertype.IsNotNullOrWhiteSpace(), x => x.Membertype.Equals(search.Membertype))
                .AndIf(search.Memberstatus.IsNotNullOrWhiteSpace(), x => x.Memberstatus.Equals(search.Memberstatus));
            var total = await _memberRepository.CountAsync(express);
            if (total == 0)
                return new PageModelDto<MemberTypeDto>(search);
            var entities = await _memberRepository.Where(express)
                .OrderByDescending(x => x.Id)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var data = Mapper.Map<List<MemberTypeDto>>(entities);
            return new PageModelDto<MemberTypeDto>(search, data, total);
        }

        public async Task<AppSrvResult> UpdateAsync(MemberTypeUpdationDto input)
        {
            input.TrimStringFields();
            //验证会员卡是否存在
            var exists = await _memberRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
                return Problem(HttpStatusCode.BadRequest, "更新失败,当前会员卡不存在或已删除");
            var entity = Mapper.Map<Bm_Member_Type>(input);
            await _memberRepository.UpdateAsync(entity, UpdatingProps<Bm_Member_Type>(x => x.Memberdescribe, x => x.Memberprice, x => x.Memberstatus, x => x.Membername));
            return AppSrvResult();
        }

        public async Task<AppSrvResult<long>> GivingPresentsAsync(GiveAwayCreationDto input)
        {
            input.TrimStringFields();
            var giveAway = Mapper.Map<Da_Card_Giveaway>(input);
            giveAway.Id = IdGenerater.GetNextId();
            giveAway.Referralcode = Guid.NewGuid().ToString();
            await _giveawayRepository.InsertAsync(giveAway);
            return giveAway.Id;
        }

        public async Task<AppSrvResult> UseCardAsync(GiveAwayUpdationDto input)
        {
            input.TrimStringFields();
            try
            {
                _unitOfWork.BeginTransaction();
                var giveAway = await _giveawayRepository.FetchAsync(x => x, x => x.Id == input.Id && x.Expirationdate > DateTime.Now && x.Usecode == 0, noTracking: false);
                if (giveAway == null)
                    return Problem(HttpStatusCode.BadRequest, "当前卡不存在或已失效");
                giveAway.Usedate = DateTime.Now;
                giveAway.Usecode = input.Usecode;
                if (giveAway.Memberid > 0)
                {
                    var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input.Usecode);
                    if (accountDto == null)
                        throw new Exception("使用人员不存在或已删除");
                    var account = Mapper.Map<Sys_Account>(accountDto);
                    account.Memberid = giveAway.Memberid;
                    //默认1年  未过期 日期基础上+1年
                    if(account.Expirationdate == null)
                        account.Expirationdate = DateTime.Now.AddYears(1);
                    else if(account.Expirationdate != null && account.Expirationdate > DateTime.Now)
                        account.Expirationdate = accountDto.Expirationdate.Value.AddYears(1);
                    else
                        account.Expirationdate = DateTime.Now.AddYears(1);
                    //account.Referralid = giveAway.Userid;
                    await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Memberid, x => x.Expirationdate));
                }
                await _giveawayRepository.UpdateAsync(giveAway);
                await _unitOfWork.CommitAsync();
                return AppSrvResult();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, "使用失败:"+ex.Message);
            }

        }

        public async Task<AppSrvResult<GiveAwayDto>> GetGiveAwayInfo(string referralcode)
        {
            var giveaway = await _giveawayRepository.FetchAsync(x => x, x => x.Referralcode.Equals(referralcode) && x.Expirationdate > DateTime.Now && x.Usecode == 0);
            if (giveaway == null)
                return Problem(HttpStatusCode.NotFound, "推荐码不存在或已失效");
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            var memerbers = await _memberRepository.GetAll().ToListAsync();
            var accountDto = accountDtos.FirstOrDefault(x => x.Id == giveaway.Userid);
            var giveAwayDto = Mapper.Map<GiveAwayDto>(giveaway);
            var member = memerbers.FirstOrDefault(x => x.Id == giveAwayDto.Memberid);
            giveAwayDto.Username = accountDto == null ? "" : accountDto.Username;
            giveAwayDto.Membername = member == null ? "" : member.Membername;
            giveAwayDto.Membertype = member == null ? "" : member.Membertype;
            return giveAwayDto;
        }

        public async Task<PageModelDto<GiveAwayDto>> GetOwnerPagedAsync(GiveAwaySearchPagedDto search)
        {
            search.TrimStringFields();
            if (search.Userid == 0)
                return new PageModelDto<GiveAwayDto>(search);
            //修复之前创建账号不生成自己的推荐码，在这里查询时如果用户信息没有个人推荐码，则创建
            var exists = await _giveawayRepository.AnyAsync(x => x.Userid == search.Userid && x.Memberid == 0);
            if (!exists)
            {
                var myGiveAway = new Da_Card_Giveaway
                {
                    Id = IdGenerater.GetNextId(),
                    Userid = search.Userid,
                    Referralcode = Guid.NewGuid().ToString()
                };
                await _giveawayRepository.InsertAsync(myGiveAway);
            }
            var express = ExpressionCreator
                .New<Da_Card_Giveaway>()
                .And(x => x.Userid == search.Userid)
                .And(x => x.Memberid > 0);
            var total = await _giveawayRepository.CountAsync(express);
            if (total == 0)
                return new PageModelDto<GiveAwayDto>(search);
            var entities = await _giveawayRepository.Where(express)
                .OrderByDescending(x => x.Id)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            var members = await _memberRepository.GetAll().ToListAsync();
            var data = Mapper.Map<List<GiveAwayDto>>(entities);
            foreach (var entity in data)
            {
                var account = accountDtos.FirstOrDefault(x => x.Id == entity.Userid);
                var useAccount = accountDtos.FirstOrDefault(x => x.Id == entity.Usecode);
                var member = members.FirstOrDefault(x => x.Id == entity.Memberid);
                entity.Username = account == null ? "" : account.Username;
                entity.Usename = useAccount == null ? "" : useAccount.Username;
                entity.Membername = member == null ? "" : member.Membername;
                entity.Membertype = member == null ? "" : member.Membertype;
            }
            return new PageModelDto<GiveAwayDto>(search, data, total);
        }
    }
}
