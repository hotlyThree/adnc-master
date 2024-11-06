using Adnc.Fstorch.User.Application.Dtos.CardCase;
using Adnc.Fstorch.User.Application.Dtos.WeChat;
using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class CardCaseAppService : AbstractAppService, ICardCaseAppService
    {
        private IEfRepository<Da_cardinfo> _cardinfoRepository;
        private IEfRepository<Da_userinfo> _userinfoRepository;
        private readonly IWeChatAppService _weChatAppService;
        private readonly IEfRepository<Da_Subscribe_Detail> _subDtRepository;
        private CacheService _cacheService;
        public CardCaseAppService(IEfRepository<Da_cardinfo> cardinfoRepository, IEfRepository<Da_userinfo> userinfoRepository, CacheService cacheService,
            IWeChatAppService weChatAppService, IEfRepository<Da_Subscribe_Detail> subDtRepository)
        {
            _cardinfoRepository = cardinfoRepository;
            _userinfoRepository = userinfoRepository;
            _cacheService = cacheService;
            _weChatAppService = weChatAppService;
            _subDtRepository = subDtRepository;
        }

        public async Task<AppSrvResult<long>> CreateAsync(CardCaseCreationDto input)
        {
            input.TrimStringFields();
            var cardInfo = Mapper.Map<Da_cardinfo>(input);
            var exists = await _cardinfoRepository.AnyAsync(x => x.Uid == cardInfo.Uid && x.Aid == cardInfo.Aid);
            if (exists)
                return Problem(HttpStatusCode.BadRequest, "请勿重复添加名片");
            cardInfo.Id = IdGenerater.GetNextId();
            cardInfo.Createtime = DateTime.Now;
            //首次分享后, B给A分享名片，input中账号id是A的，名片id是B的，B接收到的名片中没有CardExchangeId 也就是A是接收者
            await _cardinfoRepository.InsertAsync(cardInfo);
            //通知对方已经接收到名片
            //正常添加名片时，input中的账号ID是自己的, 名片ID是对方的，回递名片时，账号ID是对方的，名片ID是自己的
            var aid = await _userinfoRepository.FetchAsync(x => x.Aid, x => x.Id == cardInfo.Uid);
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == aid);
            var otherAccountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == cardInfo.Aid);
            var openid = await _subDtRepository.FetchAsync(x => x.OpenId, x => x.UnionId.Equals(accountDto.UnionId));
            var data = new
            {
                first = new { value = "您好，有人收藏了您的名片" },
                keyword1 = new { value = otherAccountDto.Username },
                keyword2 = new { value = DateTime.Now.ToString("yyyy年-MM月-dd日 HH:mm") },
                keyword3 = new { value = "收藏名片通知" },
                keyword4 = new { value = "" },
                keyword5 = new { value = "" },
                remark = new { value = $"{otherAccountDto.Username}已收藏您的名片" }
            };
            string template_id = "9PjqECq0gdQF0R643cOZbWwl7f0LFJ8bYEAHJ4EOygo";

            var sendContent = new SendMsgCreationDto
            {
                Template_id = template_id,
                Touser = openid,
                MiniPro = new MiniProgram { appid = input.Appid }
            };
            //回递名片 回写更新A首次接收到的名片CardExchangeId UID和CardExchangeId永远是相反的，如果回递A的名片  UID是A的名片ID  CardExchangeId是B的名片ID
            if (cardInfo.CardExchangeId > 0)
            {

                openid = await _subDtRepository.FetchAsync(x => x.OpenId, x => x.UnionId.Equals(otherAccountDto.UnionId));
                data = new
                {
                    first = new { value = "您好，有人给您推送了名片" },
                    keyword1 = new { value = accountDto.Username },
                    keyword2 = new { value = DateTime.Now.ToString("yyyy年-MM月-dd日 HH:mm") },
                    keyword3 = new { value = "回递名片通知" },
                    keyword4 = new { value = "" },
                    keyword5 = new { value = "" },
                    remark = new { value = $"请收藏我的名片" }
                };
                sendContent.Touser = openid;
                //通过A账号id+A已经接收到的名片夹中的B名片的id进行查询
                var otherCard = await _cardinfoRepository.FetchAsync(x => x, x => x.Aid == aid && x.Uid == cardInfo.CardExchangeId);
                //A账号的CardExchangeId更改为回递名片中的名片ID
                otherCard.CardExchangeId = cardInfo.Uid;
                await _cardinfoRepository.UpdateAsync(otherCard, UpdatingProps<Da_cardinfo>(x => x.CardExchangeId));
            }
            sendContent.Data = data;
            await _weChatAppService.SendMsgByModelAsync(sendContent);
            return cardInfo.Id;
        }

        public async Task<AppSrvResult<long[]>> CreateBothAsync(List<CardCaseCreationDto> input)
        {
            input.TrimStringFields();
            var list = new List<long>();
            var cardInfoList = Mapper.Map<List<Da_cardinfo>>(input);
            var exists = await _cardinfoRepository.AnyAsync(x => (x.Aid == cardInfoList[0].Aid && x.Uid == cardInfoList[0].Uid && !x.Addtype.Equals("C")) || 
                (x.Aid == cardInfoList[1].Aid && x.Uid == cardInfoList[1].Uid && !x.Addtype.Equals("C")));
            if (exists)
                return Problem(HttpStatusCode.BadRequest, "不能重复交换相同名片,请更换其他名片再交换");
            foreach ( var cardInfo in cardInfoList)
            {
                cardInfo.Id = IdGenerater.GetNextId();
                cardInfo.Createtime = DateTime.Now;
                list.Add(cardInfo.Id);
            }

            await _cardinfoRepository.InsertRangeAsync(cardInfoList);
            //小程序消息通知
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input[1].Aid); //消息接收方
            var otherAccountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input[0].Aid);
            var openid = await _subDtRepository.FetchAsync(x => x.OpenId, x => x.UnionId.Equals(accountDto.UnionId));
            if (openid.IsNotNullOrWhiteSpace())
            {
                var data = new
                {
                    first = new { value = "您好，有人想和您交换名片" },
                    keyword1 = new { value = otherAccountDto.Username },
                    keyword2 = new { value = DateTime.Now.ToString("yyyy年-MM月-dd日 HH:mm") },
                    keyword3 = new { value = "交换名片通知" },
                    keyword4 = new { value = "" },
                    keyword5 = new { value = "" },
                    remark = new { value = $"请与我交换名片吧！" }
                };
                string template_id = "9PjqECq0gdQF0R643cOZbWwl7f0LFJ8bYEAHJ4EOygo";

                var sendContent = new SendMsgCreationDto
                {
                    Template_id = template_id,
                    Touser = openid,
                    Data = data,
                    MiniPro = new MiniProgram { appid = input[0].Appid }
                };
                await _weChatAppService.SendMsgByModelAsync(sendContent);
            }
            return list.ToArray();
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            var card = await _cardinfoRepository.FetchAsync(x => new
            {
                x.CardExchangeId,
                x.Uid
            }, x => x.Id == id);
            if (card == null)
                return Problem(HttpStatusCode.BadRequest, "名片夹中没有此名片");
            if(card.CardExchangeId > 0)
            {
                //从对方账号删除个人交换时所用名片
                var userinfo = await _userinfoRepository.FindAsync(x => x.Id == card.Uid);
                if (userinfo != null)
                {
                    var othercard = await _cardinfoRepository.FetchAsync(x => new
                    {
                        x.Id
                    }, x => x.Aid == userinfo.Aid && x.Uid == card.CardExchangeId);
                    if(othercard != null)
                        await _cardinfoRepository.DeleteAsync(othercard.Id);
                }
            }
            await _cardinfoRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<PageModelDto<CardCaseDto>> GetPagedAsync(CardCaseSearchPaged search)
        {
            search.TrimStringFields();
            var expressions = ExpressionCreator
                .New<Da_cardinfo>()
                .And(x => x.Aid == search.Aid)
                .AndIf(search.Addtype.IsNotNullOrWhiteSpace(), x => x.Addtype.Equals(search.Addtype))
                .AndIf(search.Uid > 0, x => x.Uid == search.Uid)
                .AndIf(search.Created.IsNotNullOrWhiteSpace() && search.Created.Equals("1"), x => x.Creater == x.Aid)
                .AndIf(search.Created.IsNotNullOrWhiteSpace() && search.Created.Equals("0"), x => x.Creater != x.Aid);
            var cardinfoList = await _cardinfoRepository.Where(expressions).ToListAsync();
            if (cardinfoList.Count == 0)
                return new PageModelDto<CardCaseDto>(search);
            var uids = cardinfoList.Select(x => x.Uid).ToArray();
            var userinfoList = await _userinfoRepository.Where(x => uids.Contains(x.Id))
                .ToListAsync();
            var userDtos = Mapper.Map<List<UserDto>>(userinfoList);
            var comids = userDtos.Where(u => u.Cid > 0).Select(u => u.Cid).Distinct().ToArray();
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList != null)
            {
                //公司列表不会很多，不需要用到字典
                var comps = comDtoList.Where(x => comids.Contains(x.Id)).ToList();
                if (userDtos.IsNotNullOrEmpty() && comps.FirstOrDefault() != null)
                {
                    foreach (var user in userDtos)
                    {
                        if(user.Cid > 0)
                        {
                            var cmp = comps.FirstOrDefault(x => x.Id == user.Cid);
                            user.Cname = cmp == null ? "" : cmp.Name;
                        }
                    }
                }
            }
            if (search.SearchInput.IsNotNullOrWhiteSpace())
            {

                var usercards = userDtos.Where(x => (x.Cname.IsNotNullOrWhiteSpace() && x.Cname.Contains(search.SearchInput)) || (x.Job.IsNotNullOrWhiteSpace() && x.Job.Contains(search.SearchInput)) || (x.Username.IsNotNullOrWhiteSpace() && x.Username.Contains(search.SearchInput))).ToList();
                
                //.AndIf(search.SearchInput.IsNotNullOrWhiteSpace(), x => x.Description.Contains(search.SearchInput) || x.Memo.Contains(search.SearchInput));
                if (usercards.Count > 0)
                    foreach (var card in usercards)
                    {
                        if (userDtos.FirstOrDefault(x => x.Id == card.Id) == null)
                            userDtos.Add(card);
                    }

                var cardCaseDtoList = cardinfoList.Where(x => x.Description.Contains(search.SearchInput) || x.Memo.Contains(search.SearchInput)).ToList();
                if (cardCaseDtoList.Count == 0 && usercards.Count == 0)
                    cardinfoList = new List<Da_cardinfo>();
            }
            var cardCaseDtos = Mapper.Map<List<CardCaseDto>>(cardinfoList);
            var ids = userDtos.Select(x => x.Id).ToArray();
            cardCaseDtos = cardCaseDtos.Where(x => ids.Contains(x.Uid)).ToList();
            var total = cardCaseDtos.Count();
            if (total == 0)
                return new PageModelDto<CardCaseDto>(search);
            cardCaseDtos = cardCaseDtos
                .OrderByDescending(x => x.Createtime)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToList();
            //可能名片列表存在很多，将列表转换为字典，提升查询效率，键为Id字段
            var userDtoDict = userDtos.ToDictionary(x => x.Id);

            //在循环中使用字典来查找userDto
            foreach (var cardCaseDto in cardCaseDtos)
            {
                if (userDtoDict.TryGetValue(cardCaseDto.Uid, out var userDto))
                {
                    cardCaseDto.Job = userDto.Job;
                    cardCaseDto.Name = userDto.Username;
                    cardCaseDto.Cname = userDto.Cname;
                }
            };
            return new PageModelDto<CardCaseDto>(search, cardCaseDtos, total);
        }
        public async Task<AppSrvResult> UpdateAsync(long id, CardCaseUpdationDto input)
        {
            input.TrimStringFields();
            if (input.Addtype.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.BadRequest, "请指定添加类型");
            var card = await _cardinfoRepository.FetchAsync(x => new
            {
                x.Aid,
                x.CardExchangeId,
                x.Uid
            }, x => x.Id == id);
            if (card == null)
                return Problem(HttpStatusCode.BadRequest, "名片夹中没有此名片");
            var cardinfo = Mapper.Map<Da_cardinfo>(input);
            cardinfo.Id = id;
            await _cardinfoRepository.UpdateAsync(cardinfo, UpdatingProps<Da_cardinfo>(x => x.Addtype, x => x.Description, x => x.Memo));
            if (input.Addtype.Equals("B") || input.Addtype.Equals("C"))
            {
                //同意或拒绝
                if (card.CardExchangeId > 0)
                {
                    var userinfo = await _userinfoRepository.FindAsync(x => x.Id == card.Uid);
                    if (userinfo != null)
                    {
                        var othercard = await _cardinfoRepository.FetchAsync(x => new
                        {
                            x.Id
                        }, x => x.Aid == userinfo.Aid && x.Uid == card.CardExchangeId);
                        if (othercard != null)
                        {
                            var othercardinfo = Mapper.Map<Da_cardinfo>(input);
                            othercardinfo.Id = othercard.Id;
                            await _cardinfoRepository.UpdateAsync(othercardinfo, UpdatingProps<Da_cardinfo>(x => x.Addtype));
                        }

                        // 微信公众号通知
                        if (input.Appid.IsNotNullOrWhiteSpace())
                        {
                            
                            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == userinfo.Aid); //消息接收方
                            var otherAccountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == card.Aid);
                            var resultStr = input.Addtype.Equals("B") ? "同意" : "拒绝";
                            var openid = await _subDtRepository.FetchAsync(x => x.OpenId, x => x.UnionId.Equals(accountDto));
                            if (openid.IsNotNullOrWhiteSpace())
                            {
                                string template_id = "9PjqECq0gdQF0R643cOZbWwl7f0LFJ8bYEAHJ4EOygo";
                                var data = new
                                {
                                    first = new { value = "您好，您有一个新的申请进度" },
                                    keyword1 = new { value = otherAccountDto.Username },
                                    keyword2 = new { value = DateTime.Now.ToString("yyyy年-MM月-dd日 HH:mm") },
                                    keyword3 = new { value = "交换名片通知" },
                                    keyword4 = new { value = "" },
                                    keyword5 = new { value = "" },
                                    remark = new { value = $"对方已{resultStr}与你交换名片" }
                                };
                                var sendContent = new SendMsgCreationDto
                                {
                                    Template_id = template_id,
                                    Touser = accountDto.Openid,
                                    Data = data,
                                    MiniPro = new MiniProgram { appid = input.Appid }
                                };
                                await _weChatAppService.SendMsgByModelAsync(sendContent);
                            }
                        }
                    }

                }
            }
            return AppSrvResult();
        }

        public async Task<int> CardCaseCountAsync(long aid)
        {
            return await _cacheService.GetCardCaseCountFromCacheAsync(aid);
        }

        public async Task<int> CardCaseCountTodayAsync(long aid)
        {
            return await _cacheService.GetCardCaseCountFromCacheAsync(aid, true);
        }

        public async Task<PageModelDto<CardCaseGroupDto>> CardCaseGroupAsync(CardCaseGroupSearchPagedDto search)
        {
            search.TrimStringFields();
            var accountDtos = await _cacheService.GetAllAccountFromCacheAsync();
            //当前账号所有名片
            var ids = await _userinfoRepository.Where(x => x.Aid == search.Aid).Select(x => x.Id).ToArrayAsync();
            var todayStart = DateTime.Today;
            var expression = ExpressionCreator
                .New<Da_cardinfo>()
                .And(x => ids.Contains(x.Uid) && x.Addtype.Equals("B"))
                .AndIf(search.Today == 1, x => x.Createtime >= todayStart && x.Createtime <= todayStart.AddDays(1).AddTicks(-1));
            var total = await _cardinfoRepository.Where(expression).GroupBy(x => x.Aid).CountAsync();
            if(total == 0)
                return new PageModelDto<CardCaseGroupDto>(search);
            //哪些账号的名片夹中有当前账号的名片，并且是有效的状态 一个人可能有多个当前账号的名片，所以需要去重
            var aids = await _cardinfoRepository.Where(expression).GroupBy(x => x.Aid).Select(x => x.First().Aid).ToArrayAsync();
            //当前账号名片夹中所有有效名片所属的账号ID 去重（名片夹中可能存在多个相同对方账号的不同名片）
            var otherAidQ = _cardinfoRepository.Where(x => x.Aid == search.Aid && x.Addtype.Equals("B"));
            var otherUidQ = _userinfoRepository.GetAll();
            //当前账号名片夹中的账号信息
            var otherAids = await otherAidQ.Join(otherUidQ, x => x.Uid, y => y.Id, (x, y) => y)
                .GroupBy(y => y.Aid)
                .Select(y => y.Key)
                .ToArrayAsync();

            //其他添加了当前账号名片的账号信息
            var cardCaseGroup = accountDtos.Where(x => aids.Contains(x.Id))
                .Select(x => new CardCaseGroupDto
                {
                    Aid = x.Id,
                    Name = x.Username,
                    Photo = x.Photo
                })
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToList();
            //对比添加了当前账号名片的账号信息是否在当前账号名片夹中的账号列表中
            foreach(var card in cardCaseGroup)
            {
                card.InClude = otherAids.Contains(card.Aid);
            }
            return new PageModelDto<CardCaseGroupDto>(search, cardCaseGroup, total);
        }

        public async Task<AppSrvResult<int>> TaskCountAsync(long aid)
        {
            return await _cardinfoRepository.CountAsync(x => x.Aid == aid && x.Creater == aid && x.Addtype == "A");
        }

        public async Task<AppSrvResult<int>> ExchangeCountAsync(long aid)
        {
            return await _cardinfoRepository.CountAsync(x => x.Aid == aid && x.Creater != aid && x.Addtype == "A");
        }

        public async Task<AppSrvResult> TransferCardCaseAsync(CardCaseTransferDto input)
        {
            input.TrimStringFields();
            if (input.Aid == 0 || input.TargetAid == 0)
                return Problem(HttpStatusCode.BadRequest, "需指定转移账号或目标账号");
            else if (input.Aid == input.TargetAid)
                return Problem(HttpStatusCode.BadRequest, "违规操作");
            //转移到目标名片夹，不包含目标名片夹中已存在的名片
            var hadCards = await _cardinfoRepository.Where(x => x.Aid == input.TargetAid && x.Addtype == "B").Select(x => x.Uid).ToArrayAsync();
            var cards = await _cardinfoRepository.Where(x => x.Aid == input.Aid && !hadCards.Contains(x.Uid), noTracking: false).ToListAsync();
            cards.ForEach(x => x.Aid = input.TargetAid);
            await _cardinfoRepository.UpdateRangeAsync(cards);
            //异步线程处理所有对方名片夹
            _= Task.Factory.StartNew(async () =>
            {
                foreach(var card in cards)
                {
                    if(card.CardExchangeId > 0)
                    {
                        //查找对方收藏的目标账号所属名片(排除自己和目标账号)
                        var otherCard = await _cardinfoRepository.FetchAsync(x => x, x => x.Uid == card.CardExchangeId && x.Aid != input.Aid && x.Addtype.Equals("B") && x.Aid != input.TargetAid, noTracking : false);
                        if(otherCard != null)
                        {
                            //更改对方收藏的名片为目标账号的名片
                            otherCard.Uid = input.TargetUid;
                            await _cardinfoRepository.UpdateAsync(otherCard);
                        }
                    }
                }
            });
            return AppSrvResult();
        }

        public Task<PageModelDto<CardCaseDto>> GetPagedByCompanyAsync()
        {
            throw new NotImplementedException();
        }
    }
}
