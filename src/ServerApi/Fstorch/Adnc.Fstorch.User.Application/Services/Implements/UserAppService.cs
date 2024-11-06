
using Adnc.Fstorch.User.Application.Dtos.Home;
using Adnc.Infra.Repository.EfCore.Transaction;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class UserAppService : AbstractAppService, IUserAppService
    {
        private readonly IEfRepository<Da_userinfo> _userinfoRepository;
        private readonly IEfRepository<Da_HomeInfo> _homeinfoRepository;
        private readonly IEfRepository<Da_msg> _msgRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string? _instanceId = Environment.GetEnvironmentVariable("INSTANCE_ID");
        public UserAppService(IEfRepository<Da_userinfo> userinfoRepository, CacheService cacheService, IUnitOfWork unitOfWork, IEfRepository<Da_HomeInfo> homeinfoRepository, IEfRepository<Da_msg> msgRepository)
        {
            _userinfoRepository = userinfoRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            _homeinfoRepository = homeinfoRepository;
            _msgRepository = msgRepository;
        }

        public async Task<AppSrvResult> ChangeHomeInfo(long uid, HomeCreationAndUpdationDto input)
        {
            if (uid == 0)
                return Problem(HttpStatusCode.BadRequest, "参数错误，必须指定名片");
            if (input.Homes.Count == 0)
                await _homeinfoRepository.DeleteRangeAsync(x => x.Uid == uid);
            else
            {
                var homes = Mapper.Map<List<Da_HomeInfo>>(input.Homes);
                foreach(var home in homes)
                    home.Id = IdGenerater.GetNextId();
                await _homeinfoRepository.DeleteRangeAsync(x => x.Uid == uid);
                await _homeinfoRepository.InsertRangeAsync(homes);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> ChangeStatusAsync(long id, string status)
        {
            var exists = await _userinfoRepository.AnyAsync(x => x.Id == id);
            if (!exists)
                return Problem(HttpStatusCode.NotFound, "名片不存在，可能已经被删除");
            await _userinfoRepository.UpdateAsync(new Da_userinfo { Id = id, Status = status }, UpdatingProps<Da_userinfo>(x => x.Status));
            return AppSrvResult();
        }

        public async Task<AppSrvResult> ChangeStatusAsync(IEnumerable<long> ids, string status)
        {
            var exists = await _userinfoRepository.AnyAsync(x => ids.Contains(x.Id));
            if (!exists)
                return Problem(HttpStatusCode.NotFound, "名片不存在，这些名片可能已经被删除");
            await _userinfoRepository.UpdateRangeAsync(u => ids.Contains(u.Id), u => new Da_userinfo { Status = status });
            return AppSrvResult();
        }

        public async Task<AppSrvResult<long>> CreateAsync(UserCreation input)
        {
            input.TrimStringFields();
            /*if (await _userinfoRepository.AnyAsync(x => x.Openid == input.Openid))
                return Problem(HttpStatusCode.BadRequest, "当前用户已存在，无法重复创建");*/
            var userinfo = Mapper.Map<Da_userinfo>(input);
            userinfo.Id = IdGenerater.GetNextId();
            userinfo.Reg_time = DateTime.Now;
            await _userinfoRepository.InsertAsync(userinfo);
            return userinfo.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _userinfoRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<List<BackGroundDto>> GetBackGroundAsync()
        {
            var backList = await _cacheService.GetBackGroundListFromCacheAsync();
            if (backList == null)
                return new List<BackGroundDto>();
            return backList;
        }


        public async Task<List<StyleDto>> GetStyleAsync()
        {
            var styleList = await _cacheService.GetStyleListFromCacheAsync();
            if (styleList == null)
                return new List<StyleDto>();
            return styleList;
        }

        public async Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search)
        {
            search.TrimStringFields();
            if(search.Aid == 0)
                return new PageModelDto<UserDto>(search);
            var expressions = ExpressionCreator
                .New<Da_userinfo>()
                .And(it => it.Aid == search.Aid)
                .AndIf(search.UserName.IsNotNullOrWhiteSpace(), it => EF.Functions.Like(it.Username, $"%{search.UserName}%"))
                .AndIf(search.Address.IsNotNullOrWhiteSpace(), it => EF.Functions.Like(it.Address, $"%{search.Address}%"))
                .AndIf(search.Phone.IsNotNullOrWhiteSpace(), it => EF.Functions.Like(it.Phone, $"%{search.Phone}%"))
                .AndIf(search.Status.IsNotNullOrWhiteSpace(), it => EF.Functions.Like(it.Status, $"%{search.Status}%"))
                .AndIf(search.Default.IsNotNullOrWhiteSpace(), it => EF.Equals(it.Default, search.Default))
                .AndIf(search.Cid > 0, it => it.Cid == search.Cid);
            var total = await _userinfoRepository.CountAsync(expressions);
            if (total == 0)
                return new PageModelDto<UserDto>(search);
            var entities = await _userinfoRepository
                .Where(expressions)
                .OrderByDescending(x => x.Default)
                .ThenByDescending(x => x.Reg_time)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var userDtos = Mapper.Map<List<UserDto>>(entities);
            var comids = userDtos.Where(u => u.Cid > 0).Select(u => u.Cid).Distinct();
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == search.Aid);
            if(comDtoList != null)
            {
                var comps = comDtoList.Where(x => comids.Contains(x.Id)).Select(x => new { x.Id, x.Name, x.Scope });
                if (comps.FirstOrDefault() != null)
                {
                    foreach (var user in userDtos)
                    {
                        if (user.Cid > 0)
                        {
                            user.Cname = comps.FirstOrDefault(x => x.Id == user.Cid).Name;
                            user.Scope = comps.FirstOrDefault(x => x.Id == user.Cid).Scope;
                        }
                    }
                }
            }
            userDtos.ForEach(x =>
            {
                x.Photo = accountDto == null ? "" : accountDto.Photo;
                x.Wechatvideo = accountDto == null ? "" : accountDto.Wechatvideo;
                x.Tiktokvideo = accountDto == null ? "" : accountDto.Tiktokvideo;
            });

            return new PageModelDto<UserDto>(search, userDtos, total, null);
        }


        public async Task<AppSrvResult<UserDto>> GetUserInfoAsync(long id)
        {
            var userinfo = await _userinfoRepository.FetchAsync(u => u, x => x.Id == id);
            if(userinfo == null)
                return Problem(HttpStatusCode.NotFound, "名片不存在");
            var userdto = Mapper.Map<UserDto>(userinfo);
            if (userdto == null)
                return Problem(HttpStatusCode.NotFound, "名片不存在");
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == userdto.Aid);
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            var comDto = comDtoList.FirstOrDefault(x => x.Id == userdto.Cid);
            if(comDto != null)
            {
                userdto.Cname = comDto.Name;
                userdto.Scope = comDto.Scope;
            }
            userdto.Photo = accountDto == null ? "" : accountDto.Photo;
            var homes = _homeinfoRepository.Where(x => x.Uid == id);
            var msgs = _msgRepository.GetAll();
            var homelist = await homes.Join(msgs, x => x.Msgid, y => y.Id, (x,y) => new HomeDto
            {
                Id = x.Id, Msgid = x.Msgid, Title = y.Title, MsgType = y.Msgtype, Thumimg = y.Thumimg, Sort = x.Sort
            })
            .OrderBy(x => x.MsgType)
            .ThenBy(x => x.Sort)
            .ToListAsync();
            if(homelist.Count > 0)
                userdto.Homes = homelist;
            userdto.Wechatvideo = accountDto == null ? "" : accountDto.Wechatvideo;
            userdto.Tiktokvideo = accountDto == null ? "" : accountDto.Tiktokvideo;
            userdto.Photo = accountDto == null ? "" : accountDto.Photo;
            return userdto;
        }

        public async Task<AppSrvResult> UpdateAsync(long id, UserUpdation input)
        {
            input.TrimStringFields();
            var exists = await _userinfoRepository.AnyAsync(x => x.Id == id);
            if (!exists)
                return Problem(HttpStatusCode.BadRequest, "名片不存在，可能已经被删除");
            _unitOfWork.BeginTransaction();
            try
            {
                var user = Mapper.Map<Da_userinfo>(input);
                user.Id = id;
                user.Audit_time = DateTime.Now;
                await _userinfoRepository.UpdateAsync(user, UpdatingProps<Da_userinfo>(x => x.Sex, x => x.Username, x => x.QQ, x => x.Address,
                    x => x.Audit_time, x => x.Email, x => x.Cid, x => x.Email, x => x.Job, x => x.Phone, x => x.Wechatvideo, x => x.Tiktokvideo, x => x.Default,
                    x => x.Usertitle, x => x.Country, x => x.Memo, x => x.Photo, x => x.Styleid, x => x.Bgid, x => x.Wxid));
                //更改其他为不默认
                if (user.Default.Equals("true"))
                {
                    var aid = await _userinfoRepository.FetchAsync(x => x.Aid, x => x.Id == user.Id);
                    var otherUsers = await _userinfoRepository.FetchAsync(x => x, x => x.Aid == aid && x.Id != user.Id && x.Default.Equals("true"), noTracking: false);
                    if (otherUsers != null)
                    {
                        otherUsers.Default = "false";
                        await _userinfoRepository.UpdateAsync(otherUsers, UpdatingProps<Da_userinfo>(x => x.Default));
                    }
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, $"更新时出错:{ex.Message}");
            }
            finally { _unitOfWork.Dispose(); }

            return AppSrvResult();
        }

        public async Task<PageModelDto<HomeDto>> HomePagedAsync(HomeSearchPagedDto search)
        {
            search.TrimStringFields();
            var expression = ExpressionCreator
                .New<Da_HomeInfo>()
                .And(x => x.Uid == search.Aid);
            var homeQ = _homeinfoRepository.Where(expression);
            var msgQ = _msgRepository.GetAll();
            var total = await homeQ.Join(msgQ, x => x.Msgid, y => y.Id, (x,y) =>  new { x.Id}).CountAsync();
            if (total == 0)
                return new PageModelDto<HomeDto>(search);
            var entities = await homeQ.Join(msgQ, x => x.Msgid, y => y.Id, (x, y) => new HomeDto
            {
                Id = x.Id,
                Uid = x.Uid,
                Msgid = x.Msgid,
                MsgType = y.Msgtype,
                Title = y.Title,
                Thumimg = y.Thumimg,
                Sort = x.Sort,
                Releaseid = y.Releaseid,
                Releasetime = y.Releasetime
            })
            .OrderByDescending(x => x.Releasetime)
            .Skip(search.SkipRows())
            .Take(search.PageSize)
            .ToListAsync();
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            foreach(var entity in entities)
            {
                entity.ReleaseName = accountDtos.FirstOrDefault(x => x.Id == entity.Releaseid).Username;
                entity.Photo = accountDtos.FirstOrDefault(x => x.Id == entity.Releaseid).Photo;
            }
            return new PageModelDto<HomeDto>(search, entities, total);
        }

        public async Task<AppSrvResult<long[]>> FavoritesListAsync(long uid)
        {
            return await _homeinfoRepository.Where(x => x.Uid == uid).Select(x => x.Msgid).ToArrayAsync();
        }

        public async Task<AppSrvResult<long>> CreatedFavoritesAsync(HomeCreationDto input)
        {
            input.TrimStringFields();
            if (input.Uid == 0 || input.Msgid == 0)
                return Problem(HttpStatusCode.BadRequest, "必须指定账号或文章收藏");
            var exists = await _homeinfoRepository.AnyAsync(x => x.Uid == input.Uid && x.Msgid == input.Msgid);
            if (exists)
                return Problem(HttpStatusCode.BadRequest, "请勿重复收藏");
            var entity = Mapper.Map<Da_HomeInfo>(input);
            entity.Id = IdGenerater.GetNextId();
            await _homeinfoRepository.InsertAsync(entity);
            return entity.Id;
        }

        public async Task<AppSrvResult> DeleteFavoritesAsync(long uid, long mid)
        {
            //var homeId = await _homeinfoRepository.FetchAsync(x => x.Id, x => x.Uid == uid && x.Msgid == mid, noTracking: false);
            await _homeinfoRepository.DeleteRangeAsync(x => x.Uid == uid && x.Msgid == mid);
            return AppSrvResult();
        }
    }
}
