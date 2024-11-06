using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class PaymentAppService : AbstractAppService, IPaymentAppService
    {
        private readonly IEfRepository<Da_Pay_Detail> _payDetailRepository;
        private readonly CacheService _cacheService;
        private readonly IUnitOfWork  _unitOfWork;
        private readonly ILogger<PaymentAppService> _logger;
        private readonly IEfRepository<Da_Card_Giveaway> _giveawayRepository;
        private readonly IEfRepository<Sys_Account> _accountRepository;
        private readonly ICustomerAppService _customerAppService;
        private readonly IEfRepository<Bm_Member_Type> _memberRepository;
        private readonly IEfRepository<Da_Revenue_Detail> _revenueRepository;
        private readonly IEfRepository<Da_Cash_Out> _cashRepository;
        
        public PaymentAppService(IEfRepository<Da_Pay_Detail> payDetailRepository, CacheService cacheService, IUnitOfWork unitOfWork, ILogger<PaymentAppService> logger,
            IEfRepository<Da_Card_Giveaway> giveawayRepository, IEfRepository<Sys_Account> accountRepository, ICustomerAppService customerAppService, IEfRepository<Bm_Member_Type> memberRepository, IEfRepository<Da_Revenue_Detail> revenueRepository, IEfRepository<Da_Cash_Out> cashRepository)
        {
            _payDetailRepository = payDetailRepository;
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _giveawayRepository = giveawayRepository;
            _accountRepository = accountRepository;
            _customerAppService = customerAppService;
            _memberRepository = memberRepository;
            _revenueRepository = revenueRepository;
            _cashRepository = cashRepository;
        }

        public async Task<PageModelDto<PayDetailDto>> GetPagedAsync(PayDetailSearchPagedDto search)
        {
            search.TrimStringFields();
            var expression = ExpressionCreator
                .New<Da_Pay_Detail>()
                .And(x => x.Userid == search.Userid);
            var total = await _payDetailRepository.Where(expression).CountAsync();
            if (total == 0)
                return new PageModelDto<PayDetailDto>(search);
            var entities = await _payDetailRepository.Where(expression)
                .OrderByDescending(x => x.Id)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var data = Mapper.Map<List<PayDetailDto>>(entities);
            var members = await _memberRepository.GetAll().ToListAsync();
            foreach(var entity in data)
            {
                var member = members.FirstOrDefault(x => x.Id == entity.Memberid);
                var memberName = member == null ? "" : member.Membername;
                var memberType = member == null ? "" : member.Membertype;
                entity.Membername = memberName;
                entity.Membertype = memberType;
            }
            return new PageModelDto<PayDetailDto>(search, data, total);
        }

        public async Task<AppSrvResult<object>> PurchaseMembership(PayDetailCreationDto input)
        {
            input.TrimStringFields();
            var payDetail = Mapper.Map<Da_Pay_Detail>(input);
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == payDetail.Userid);
            if (accountDto == null)
                return Problem(HttpStatusCode.BadRequest, "支付失败:当前账号不存在或已注销");
            if (accountDto.Openid.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.BadRequest, "支付失败:当前账号未绑定微信");
            _unitOfWork.BeginTransaction();
            string rn = "";
            try
            {
                payDetail.Id = IdGenerater.GetNextId();
                //购买年数
                int buyAmount = Convert.ToInt32(input.Memberamount);

                //默认购买年数  未过期 日期基础上+购买年数
                if (accountDto.Expirationdate == null)
                    payDetail.Expirationdate = DateTime.Now.AddYears(buyAmount);
                else if (accountDto.Expirationdate != null && accountDto.Expirationdate > DateTime.Now)
                    payDetail.Expirationdate = accountDto.Expirationdate.Value.AddYears(buyAmount);
                else
                    payDetail.Expirationdate = DateTime.Now.AddYears(buyAmount);
                //收钱吧金额单位为"分"
                var total_fee = Convert.ToInt32(payDetail.Membermoney * 100);
                rn = Pay_Sqb_Face(payDetail.Id.ToString(), accountDto.Openid, total_fee.ToString(), accountDto.Username);
                if (rn.IsNullOrWhiteSpace())
                    return Problem(HttpStatusCode.BadRequest, "发起收钱吧支付失败，请重新支付");
                await _cacheService.SetOrderNoToCacheAsync(payDetail.Id, "membership");
                await _payDetailRepository.InsertAsync(payDetail);
                await _unitOfWork.CommitAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string,object>>(rn);
                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, "支付失败:"+ ex.Message);
            }
        }

        public async Task<AppSrvResult> PurchaseMembershipByReferralcode(long uid, string referralcode)
        {
            var giveAway = await _giveawayRepository.FetchAsync(x => x, x => x.Referralcode.Equals(referralcode) && x.Usecode == 0 && x.Expirationdate > DateTime.Now, noTracking:false);
            if (giveAway == null)
                return Problem(HttpStatusCode.BadRequest, "推荐码不存在或已失效");
            _unitOfWork.BeginTransaction();
            try
            {
                giveAway.Usedate = DateTime.Now;
                giveAway.Usecode = uid;
                await _giveawayRepository.UpdateAsync(giveAway);
                var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == uid);
                var account = Mapper.Map<Sys_Account>(accountDto);
                account.Memberid = giveAway.Memberid;
                //没有推荐人时使用推荐码则绑定推荐关系
                if (account.Referralid == 0)
                    account.Referralid = giveAway.Userid;
                //account.Expirationdate = DateTime.Now.AddYears(1);

                //默认购买年数  未过期 日期基础上+购买年数
                if (account.Expirationdate == null)
                    account.Expirationdate = DateTime.Now.AddYears(1);
                else if (account.Expirationdate != null && account.Expirationdate > DateTime.Now)
                    account.Expirationdate = account.Expirationdate.Value.AddYears(1);
                else
                    account.Expirationdate = DateTime.Now.AddYears(1);
                await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Memberid, x => x.Referralid, x => x.Expirationdate));
                await _unitOfWork.CommitAsync();
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, "抵扣会员错误,请重新尝试使用:"+ex.Message);
            }
        }

        public async Task<AppSrvResult<object>> PurchaseMembershipCard(PayDetailCreationDto input)
        {
            input.TrimStringFields();
            var payDetail = Mapper.Map<Da_Pay_Detail>(input);
            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == payDetail.Userid);
            if (accountDto == null)
                return Problem(HttpStatusCode.BadRequest, "支付失败:当前账号不存在或已注销");
            if (accountDto.Openid.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.BadRequest, "支付失败:当前账号未绑定微信");
            _unitOfWork.BeginTransaction();
            string rn = "";
            try
            {
                payDetail.Id = IdGenerater.GetNextId();
                //购买会员卡默认为1年
                payDetail.Expirationdate = DateTime.Now.AddYears(1);
                //收钱吧金额单位为"分"
                var total_fee = Convert.ToInt32(payDetail.Membermoney * 100);
                rn = Pay_Sqb_Face(payDetail.Id.ToString(), accountDto.Openid, total_fee.ToString(), accountDto.Username);
                if (rn.IsNullOrWhiteSpace())
                    return Problem(HttpStatusCode.BadRequest, "发起支付失败，请重新支付");
                await _cacheService.SetOrderNoToCacheAsync(payDetail.Id, "membershipcard");
                await _payDetailRepository.InsertAsync(payDetail);
                await _unitOfWork.CommitAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(rn);
                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, "支付失败:" + ex.Message);
            }
        }


        /// <summary>
        /// 收钱吧收款操作
        /// </summary>
        /// <param name="request_id">请求唯一识别码</param>
        /// <param name="openid">用户openid</param>
        /// <param name="amount">金额</param>
        /// <param name="description">备注</param>
        /// <returns></returns>
        public string Pay_Sqb_Face(string request_id, string openid, string amount,string description = "")
        {
            string store_sn = "001";
            string store_name = "成都正火科技有限公司";
            string notify_url = "http://h378658d84.oicp.vip:5555/pay/api/purchase/callback";
            string tender_type = "3";
            string sub_tender_type = "301";
            string payreturn = "";
            //授权类型 小程序=3
            string scene_type = "3";
            string memo = "中芯名片收款";
            payreturn = new JYpayInterFace.jysqbpay().pay_Post(request_id, openid, store_sn, store_name, amount, description, "", description, memo,
                scene_type, tender_type, sub_tender_type, notify_url, "wx4d4ced5119f4d768", "").ToString();
            if (payreturn.StartsWith("error:"))
                payreturn = "";
            return payreturn;
        }

        public async Task<AppSrvResult<object>> PurchaseCallBackAsync(JObject head, JObject body, string signature)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                string out_trade_no = body["check_sn"].ToString();
                string trade_state = body["order_status"].ToString();
                string s_order = body["order_sn"].ToString();
                _logger.LogInformation("支付回调，接收通知结果head>>>{head},通知结果body>>>{body}", head.ToString(), body.ToString());
                if (out_trade_no.IsNullOrWhiteSpace())
                {
                    _logger.LogInformation("收钱吧支付回调失败,商户系统订单号为空");
                    return Problem(HttpStatusCode.BadRequest, "商户系统订单号为空");
                }
                var type = await _cacheService.GetOrderTypeFromCacheAsync((long)out_trade_no.ToLong());
                if (type.IsNullOrWhiteSpace())
                {
                    _logger.LogInformation("收钱吧支付回调失败,付超时或已通过轮序查找到订单支付结果，请确认订单支付结果:{out_trade_no}, 支付状态:{trade_state}，临时订单号已失效，时效最多为10分钟", out_trade_no, trade_state);
                    return Problem(HttpStatusCode.BadRequest, "支付超时或已通过轮序查找到订单支付结果，临时订单号已失效，时效最多为10分钟");
                }
                var payDetail = await _payDetailRepository.FindAsync((long)out_trade_no.ToLong(), noTracking: false);
                if (payDetail == null)
                    throw new Exception($"支付订单未找到:{(long)out_trade_no.ToLong()}");

                if (trade_state.Equals("4") && !payDetail.Status.Equals("B"))
                {
                    payDetail.Status = "B";
                    //支付成功
                    switch (type)
                    {
                        case "membership":
                            //会员购买  二级分销
                            var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == payDetail.Userid);
                            var account = Mapper.Map<Sys_Account>(accountDto);
                            account.Memberid = payDetail.Memberid;
                            //期限以付款订单为准
                            account.Expirationdate = payDetail.Expirationdate;
                            await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Memberid, x => x.Expirationdate));
                            var memberType = await _memberRepository.FindAsync(x => x.Id == payDetail.Memberid);
                            if(memberType.Shareamount > 0)
                            {
                                //防止其他线程更新冲突
                                Dictionary<string, object> _locks = new Dictionary<string, object>();
                                if (!_locks.ContainsKey(account.Id.ToString()))
                                {
                                    _locks[account.Id.ToString()] = new object();
                                }
                                var lockObject = _locks[account.Id.ToString()];
                                //处理分销提成
                                lock (lockObject)
                                {
                                    _ = Task.Run(async () =>
                                    {
                                        await ShareToReferralUser(account.Id, account.Referralid, memberType.Shareamount * payDetail.Memberamount);
                                    });
                                }
                            }
                            break;
                        case "membershipcard":
                            //会员卡购买 不分销（曾总说后期可能会打折）
                            for(int i = 1; i <= payDetail.Memberamount; i++)
                            {
                                var giveAway = new GiveAwayCreationDto
                                {
                                    Userid = payDetail.Userid,
                                    Memberid = payDetail.Memberid,
                                    Expirationdate = payDetail.Expirationdate
                                };
                                //创建会员卡
                                await _customerAppService.GivingPresentsAsync(giveAway);
                            }
                            break;
                        default:
                            break;
                    }
                    await _payDetailRepository.UpdateAsync(payDetail);
                }
                else if(trade_state.Equals("6"))
                {
                    //支付失败或取消支付
                    payDetail.Status = "C";
                    await _payDetailRepository.UpdateAsync(payDetail);
                }
                //返回接受成功标志
                Dictionary<string, object> dic = new Dictionary<string, object>();
                Dictionary<string, object> dic_head = new Dictionary<string, object>();
                Dictionary<string, object> dic_body = new Dictionary<string, object>();
                Dictionary<string, object> dic_response = new Dictionary<string, object>();
                Dictionary<string, object> biz_response = new Dictionary<string, object>();
                dic_head.Add("version", head["version"].ToString());
                dic_head.Add("sign_type", head["sign_type"].ToString());
                dic_head.Add("appid", head["appid"].ToString());
                dic_head.Add("response_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dic_response.Add("head", dic_head);
                dic_body.Add("result_code", "200");
                biz_response.Add("result_code", "200");
                dic_body.Add("biz_response", biz_response);
                dic_response.Add("body", dic_body);
                dic.Add("response", dic_response);
                dic.Add("signature", signature);
                await _unitOfWork.CommitAsync();
                return dic;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogInformation("支付错误:{Message}",ex.Message);
                return Problem(HttpStatusCode.BadRequest, $"支付错误:{ex.Message}");
            }
        }

        private async Task ShareToReferralUser(long id, long referralid,  decimal money)
        {
            //单独开启更新提成事务
            _unitOfWork.BeginTransaction();
            //一级
            try
            {
                var revenue = new Da_Revenue_Detail
                {
                    Id = IdGenerater.GetNextId(),
                    Userid = id,
                    Recommendid = referralid,
                    Money = money * 0.7m,
                    Describe = "用户购买提成",
                    Createtime = DateTime.Now
                };
                await _revenueRepository.InsertAsync(revenue);
                var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == referralid);
                var account = Mapper.Map<Sys_Account>(accountDto);
                account.Totalamount += money * 0.7m;
                account.Nowithdrawal = account.Totalamount - account.Cashwithdrawal;
                await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Totalamount, x => x.Nowithdrawal));
                if (account.Referralid > 0)
                {
                    //二级
                    var revenueSecLv = new Da_Revenue_Detail
                    {
                        Id = IdGenerater.GetNextId(),
                        Userid = account.Id,
                        Recommendid = account.Referralid,
                        Money = money * 0.3m,
                        Describe = "用户购买提成",
                        Createtime = DateTime.Now
                    };
                    await _revenueRepository.InsertAsync(revenue);
                    var accountDtoSecLv = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == account.Referralid);
                    var accountSecLv = Mapper.Map<Sys_Account>(accountDtoSecLv);
                    accountSecLv.Totalamount += money * 0.3m;
                    accountSecLv.Nowithdrawal = accountSecLv.Totalamount - accountSecLv.Cashwithdrawal;
                    await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Totalamount, x => x.Nowithdrawal));
                }
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("二级分销提成处理失败:{message}", ex.Message);
            }
            
        }

        public async Task<AppSrvResult<long>> WithdrawCashAsync(CashOutCreationDto input)
        {
            input.TrimStringFields();
            _unitOfWork.BeginTransaction();
            try
            {
                //验证是否存在自己为推荐人产生的收入记录
                var exists = await _revenueRepository.AnyAsync(x => x.Recommendid == input.Userid);
                if(!exists)
                    return Problem(HttpStatusCode.NotFound, "未查询到个人推荐收入记录");
                var cashOut = Mapper.Map<Da_Cash_Out>(input);
                cashOut.Id = IdGenerater.GetNextId();
                cashOut.Outmoney = input.Outmoney;
                cashOut.Status = "A";
                cashOut.Taxes = input.Outmoney * 0.1m;
                cashOut.createtime = DateTime.Now;
                await _cashRepository.InsertAsync(cashOut);
                //更新账号已提现金额和未提现金额
                var accountDto = (await _cacheService.GetAllAccountFromCacheAsync()).FirstOrDefault(x => x.Id == input.Userid);
                var account = Mapper.Map<Sys_Account>(accountDto);
                account.Cashwithdrawal += input.Outmoney;
                account.Nowithdrawal = account.Totalamount - account.Cashwithdrawal;
                await _accountRepository.UpdateAsync(account, UpdatingProps<Sys_Account>(x => x.Cashwithdrawal, x => x.Nowithdrawal));
                await _unitOfWork.CommitAsync();
                return cashOut.Id;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("提现错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "提现时发生错误:"+ex.Message);
            }
        }

        public string GetSignTest(string body)
        {
            return new JYpayInterFace.jysqbpay().GetSign(body);
        }
    }
}
