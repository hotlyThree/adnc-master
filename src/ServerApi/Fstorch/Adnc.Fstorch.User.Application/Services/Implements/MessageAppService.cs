
using Adnc.Fstorch.User.Application.Dtos.ReadDetail;
using Adnc.Fstorch.User.Application.Dtos.WeChat;
using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class MessageAppService : AbstractAppService, IMessageAppService
    {
        private readonly IEfRepository<Da_msg> _msgRepository;
        private readonly IEfRepository<Da_ReadDetail> _readRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeChatAppService _weChatAppService;
        private readonly IEfRepository<Da_Subscribe_Detail> _subDtRepository;
        private readonly CacheService _cacheService;

        public MessageAppService (IEfRepository<Da_msg> msgRepository, CacheService cacheService, IEfRepository<Da_ReadDetail> readRepository, IUnitOfWork unitOfWork, IWeChatAppService weChatAppService, IEfRepository<Da_Subscribe_Detail> subDtRepository)
        {
            _msgRepository = msgRepository;
            _cacheService = cacheService;
            _readRepository = readRepository;
            _unitOfWork = unitOfWork;
            _weChatAppService = weChatAppService;
            _subDtRepository = subDtRepository;
        }


        public async Task<AppSrvResult<long>> CreateAsync(MessageCreationDto input)
        {
            input.TrimStringFields();
            if (input.Releaseid == 0)
                return Problem(HttpStatusCode.BadRequest, "创建失败! 需指定发布人员");
            var msg = Mapper.Map<Da_msg>(input);
            msg.Id = IdGenerater.GetNextId();
            msg.Releasetime = DateTime.Now;
            var dicCheck = await _weChatAppService.MsgSecCheck(input.Memo);
            if (!dicCheck["errcode"].ToString().Equals("0"))
                return Problem(HttpStatusCode.BadRequest, dicCheck["errcode"].ToString().Equals("87014") ? "存在违规内容" :dicCheck["errmsg"].ToString());
            if (input.Quoteid > 0)
            {
                var otherMsg = await _msgRepository.FindAsync(input.Quoteid);
                if (otherMsg == null)
                    return Problem(HttpStatusCode.BadRequest, "引用文章不存在或已删除");
                otherMsg.Quotenum += 1;
                await _msgRepository.UpdateAsync(otherMsg, UpdatingProps<Da_msg>(x => x.Quotenum));
            }
            await _msgRepository.InsertAsync(msg);
            return msg.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _msgRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<MessageDto>> GetInfoAsync(ReadCreationDto input)
        {
            if (input.Msgid == 0)
                return Problem(HttpStatusCode.BadRequest, "参数错误，文章ID不存在");

            var msg = await _msgRepository.FindAsync(input.Msgid);
            if(msg == null)
                return Problem(HttpStatusCode.NotFound, "指定内容不存在或已删除");
            var reads = await _readRepository.Where(x => x.Msgid == input.Msgid).ToListAsync();
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            var companyDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            var readerAccount = accountDtos.FirstOrDefault(x => x.Id == input.Reader);
            _unitOfWork.BeginTransaction();
            try
            {
                //更新阅读次数
                msg.Readnum += 1;
                await _msgRepository.UpdateAsync(msg, UpdatingProps<Da_msg>(x => x.Readnum));
                await _cacheService.SetReadCountToCacheAsync(input.Msgid, msg.Readnum);
                //增加阅读者明细信息
                var readerInfo = Mapper.Map<Da_ReadDetail>(input);
                readerInfo.Id = IdGenerater.GetNextId();
                readerInfo.Readopenname = readerAccount == null ? "" : readerAccount.Openname;
                readerInfo.Readopenwxid = readerAccount == null ? "" : readerAccount.Openid;
                readerInfo.Readtime = DateTime.Now;
                await _readRepository.InsertAsync(readerInfo);
                var messageDto = Mapper.Map<MessageDto>(msg);
                //返回当前阅读明细ID，用于更新阅读时长
                messageDto.Rid = readerInfo.Id;
                messageDto.CommentSimpleTreeList = await _cacheService.GetCommentsTreeFromCacheAsync(input.Msgid, input.Reader);
                var accountDto = accountDtos.FirstOrDefault(x => x.Id == messageDto.Releaseid);
                /*if (accountDto == null)
                    throw new Exception("原文发布者信息不存在, 无法查看");*/
                var companyDto = companyDtos.FirstOrDefault(x => x.Id == messageDto.Cid);
                messageDto.ReleaseName = accountDto == null ?  "" : accountDto.Username;
                messageDto.Cname = companyDto == null ? "" : companyDto.Name;
                messageDto.Wechatvideo = accountDto == null ? "" : accountDto.Wechatvideo;
                messageDto.Tiktokvideo = accountDto == null ? "" : accountDto.Tiktokvideo;
                messageDto.Photo = accountDto == null ? "" : accountDto.Photo;
                messageDto.Signature = accountDto == null ? "" : accountDto.Signature;
                await _unitOfWork.CommitAsync();
                if(accountDto != null && accountDto.UnionId.IsNotNullOrWhiteSpace() && input.Appid.IsNotNullOrWhiteSpace())
                {
                    var openid = await _subDtRepository.FetchAsync(x => x.OpenId, x => x.UnionId.Equals(accountDto.UnionId));
                    //异步通知 
                    var data = new
                    {
                        first = new {value = "您好，我正在阅读您发布的文章"},
                        keyword1 = new { value = input.Reader == 0 ? "游客" : $"{readerAccount.Openname}({readerAccount.Username})"},
                        keyword2 = new { value = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")},
                        keyword3 = new { value = "阅读通知"},
                        keyword4 = new { value = "" },
                        keyword5 = new { value = "" },
                        remark = new { value = "请进入小程序查看" }
                    };
                    string template_id = "9PjqECq0gdQF0R643cOZbWwl7f0LFJ8bYEAHJ4EOygo";
                    //string template_id = "MeE6yiQKGxYMI1fWa6bbeWprbrAVR3uECrDAsfzKeLI";
                    var sendContent = new SendMsgCreationDto
                    {
                        Template_id = template_id,
                        Touser = openid,
                        Data = data,
                        MiniPro = new MiniProgram { appid = input.Appid}
                    };
                    await _weChatAppService.SendMsgByModelAsync(sendContent);
                }
                return messageDto;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<PageModelDto<MessageTitleDto>> GetPagedAsync(MessageSearchPagedDto search)
        {
            search.TrimStringFields();
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            var companyDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            long[] aids = Array.Empty<long>(), cids = Array.Empty<long>();
            if(accountDtos.Count > 0)
                aids = accountDtos.Where(x => search.SearchInput.IsNotNullOrWhiteSpace() && x.Username.Contains(search.SearchInput)).Select(x => x.Id).ToArray();
            if(companyDtos.Count > 0)
                cids = companyDtos.Where(x => search.SearchInput.IsNotNullOrWhiteSpace() && x.Name.Contains(search.SearchInput)).Select(x => x.Id).ToArray();
            var expression = ExpressionCreator
                .New<Da_msg>()
                .AndIf(search.Msgtype.IsNotNullOrWhiteSpace(), x => x.Msgtype.Equals(search.Msgtype))
                .AndIf(search.SearchInput.IsNotNullOrWhiteSpace(), x => x.Title.Contains(search.SearchInput) || (aids.Length > 0 && aids.Contains(x.Releaseid)
                || (cids.Length > 0 && cids.Contains(x.Cid))))
                .AndIf(search.Aid > 0, x => x.Releaseid == search.Aid)
                .AndIf(search.Cid > 0, x => x.Cid == search.Cid)
                .AndIf(search.Timestamp.IsNotNullOrEmpty(), x => x.Releasetime >= search.Timestamp[0] && x.Releasetime <= search.Timestamp[1]);
            var total = await _msgRepository.CountAsync(expression);
            if (total == 0)
                return new PageModelDto<MessageTitleDto>(search);
            var entities = await _msgRepository
                .Where(expression)
                .OrderByDescending(x => x.Releasetime)
                .Select(x => new MessageTitleDto()
                {
                    Id = x.Id,
                    Msgtype = x.Msgtype,
                    Title = x.Title,
                    Releasetime = x.Releasetime,
                    Readnum = x.Readnum,
                    Cid = x.Cid,
                    Releaseid = x.Releaseid,
                    Thumimg = x.Thumimg
                })
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            foreach( var entity in entities)
            {
                var accountDto = accountDtos.FirstOrDefault(x => x.Id == entity.Releaseid);
                var companyDto = companyDtos.FirstOrDefault(x => x.Id == entity.Cid);
                entity.ReleaseName = accountDto == null ? "" : accountDto.Username;
                entity.Cname = companyDto == null ? "" : companyDto.Name;
            }
            return new PageModelDto<MessageTitleDto>(search, entities, total);
        }

        public async Task<AppSrvResult> UpdateAsync(long id, MessageUpdationDto input)
        {
            input.TrimStringFields();
            var msg = Mapper.Map<Da_msg>(input);
            msg.Id = id;
            var dicCheck = await _weChatAppService.MsgSecCheck(input.Memo);
            if (!dicCheck["errcode"].ToString().Equals("0"))
                return Problem(HttpStatusCode.BadRequest, dicCheck["errcode"].ToString().Equals("87014") ? "存在违规内容" : dicCheck["errmsg"].ToString());
            await _msgRepository.UpdateAsync(msg, UpdatingProps<Da_msg>(x => x.Title, x => x.Memo, x => x.Ishome, x => x.Thumimg));
            return AppSrvResult();
        }


        public async Task<AppSrvResult> ChangeThumimg(MessageThumimgUpdationDto input)
        {
            await _msgRepository.UpdateAsync(new Da_msg { Id = input.Id, Thumimg = input.Path}, UpdatingProps<Da_msg>(x => x.Thumimg));
            return AppSrvResult();
        }

        public async Task<PageModelDto<ReadDetailDto>> GetPageAsync(ReadDetailSearchPagedDto search)
        {
            var total = await _readRepository
                .Where(x => x.Msgid == search.id)
                .Select(x => new
                {
                    x.Reader
                })
                .GroupBy(x => x.Reader)
                .CountAsync();
            if (total == 0)
                return new PageModelDto<ReadDetailDto>(search);
            var readDtos = await _readRepository
                .Where(x => x.Msgid == search.id)
                .GroupBy(x => x.Reader)
                .Select(g => new ReadDetailDto
                {
                    Reader = g.Key,
                    Readopenname = g.First().Readopenname,
                    Readopenwxid = g.First().Readopenwxid,
                    Count = g.Count(),
                    Duration = g.Sum(x => x.Duration)
                })
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var allAccount = await _cacheService.GetAllAccountFromCacheAsync();
            if(allAccount == null)
                return new PageModelDto<ReadDetailDto>();
            foreach(var read in readDtos)
            {
                var accountDto = allAccount.FirstOrDefault(x => x.Id == read.Reader);
                if (accountDto != null)
                {
                    read.Name = accountDto.Username;
                    read.Photo = accountDto.Photo;
                    read.WeChatVideo = accountDto.Wechatvideo;
                }
            }
            return new PageModelDto<ReadDetailDto>(search, readDtos, total);
        }

        public async Task<AppSrvResult> ChangeDurationAsync(ReadUpdationDto input)
        {
            var readDetail = await _readRepository.FindAsync(input.Id);
            if (readDetail == null)
                return Problem(HttpStatusCode.BadRequest, $"阅读记录不存在:{input.Id}");
            readDetail.Endtime = input.Endtime;
            var difference = input.Endtime - readDetail.Readtime;
            readDetail.Duration = difference.Value.Seconds;
            await _readRepository.UpdateAsync(readDetail, UpdatingProps<Da_ReadDetail>(x => x.Endtime, x => x.Duration));
            return AppSrvResult();
        }

        public async Task<int> ReadCountByReleaseidAsync(long releaseid)
        {
            return await _cacheService.GetReadCountFromCacheAsync(releaseid);
        }

        public async Task<int> ReadCountTodaytReleaseidAsync(long releaseid)
        {
            return await _cacheService.GetReadCountFromCacheAsync(releaseid, true);
        }

        public async Task<PageModelDto<ReadDetailDto>> ReadCountGroupByAsync(ReadDetailGroupSearchPagedDto search)
        {
            var mids = await _msgRepository.Where(x => x.Releaseid == search.Releaseid).Select(x => x.Id).ToArrayAsync();
            if (mids.Length == 0)
                return new PageModelDto<ReadDetailDto>(search);
            var todayStart = DateTime.Today;
            var total = await _readRepository
                .Where(x => mids.Contains(x.Msgid))
                .WhereIf(search.Today == 1, x => x.Readtime >= todayStart && x.Readtime <= todayStart.AddDays(1).AddTicks(-1))
                .Select(x => new
                {
                    x.Reader
                })
                .GroupBy(x => x.Reader)
                .CountAsync();
            if (total == 0)
                return new PageModelDto<ReadDetailDto>(search);
            var readDtos = await _readRepository
                .Where(x => mids.Contains(x.Msgid))
                .WhereIf(search.Today == 1, x => x.Readtime >= todayStart && x.Readtime <= todayStart.AddDays(1).AddTicks(-1))
                .GroupBy(x => x.Reader)
                .Select(g => new ReadDetailDto
                {
                    Reader = g.Key,
                    Readopenname = g.First().Readopenname,
                    Readopenwxid = g.First().Readopenwxid,
                    Count = g.Count(),
                    Duration = g.Sum(x => x.Duration)
                })
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var allAccount = await _cacheService.GetAllAccountFromCacheAsync();
            if (allAccount == null)
                return new PageModelDto<ReadDetailDto>();
            foreach (var read in readDtos)
            {
                var accountDto = allAccount.FirstOrDefault(x => x.Id == read.Reader);
                if (accountDto != null)
                {
                    read.Name = accountDto.Username;
                    read.Photo = accountDto.Photo;
                    read.WeChatVideo = accountDto.Wechatvideo;
                }
            }
            return new PageModelDto<ReadDetailDto>(search, readDtos, total);
        }

        public async Task<PageModelDto<ReadDetailReaderDto>> ReadDetailPagedAsync(ReadDetailReaderSearchPagedDto search)
        {
            search.TrimStringFields();
            var mids = await _msgRepository.Where(x => x.Releaseid == search.Releaseid).Select(x => x.Id).ToArrayAsync();
            var todayStart = DateTime.Today;
            var expression = ExpressionCreator
                .New<Da_ReadDetail>()
                .And(x => x.Reader == search.Reader)
                .AndIf(search.Releaseid > 0, x => mids.Contains(x.Msgid))
                .AndIf(search.MsgId > 0, x => x.Msgid == search.MsgId)
                .AndIf(search.Today == 1, x => x.Readtime >= todayStart && x.Readtime <= todayStart.AddDays(1).AddTicks(-1));
            var total = await _readRepository.CountAsync(expression);
            if (total == 0)
                return new PageModelDto<ReadDetailReaderDto>(search);
            //读者账号信息
            //var account = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id ==  search.Reader);
            var readDetailQ = _readRepository.Where(expression);
            var msgQ = _msgRepository.GetAll();
            var entities = await readDetailQ.Join(msgQ, x => x.Msgid, y => y.Id, (x, y) => new ReadDetailReaderDto
            {
                Id = x.Id,
                Msgid = x.Msgid,
                MsgTitle = y.Title,
                MsgType = y.Msgtype,
                Readtime = x.Readtime,
                Endtime = x.Endtime,
                Duration = x.Duration
            })
            .OrderByDescending(x => x.Readtime)
            .Skip(search.SkipRows())
            .Take(search.PageSize)
            .ToListAsync();
            return new PageModelDto<ReadDetailReaderDto>(search, entities, total);

        }
    }
}
