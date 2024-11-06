namespace Fstorch.AfterSales.Application.Services.Implements
{
    public class AfterSalesCostService : AbstractAppService, IAfterSalesCostService
    {
        private readonly ILogger<AfterSalesOrderService> _logger;
        private readonly ISystemRestClient _systemRestClient;
        private readonly IStaffRestClient _staffRestClient;
        private readonly IEfRepository<Da_Service_Order_Cope> _copeRepository;
        private readonly IEfRepository<Da_Service_Order_Rec> _recRepository;
        private readonly IEfRepository<Da_Service_Order_Engineer> _engineerRepository;
        private readonly IEfRepository<Da_Service_Order> _orderRepository;
        private readonly IEfRepository<Da_Service_Eng_Grant> _grantRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AfterSalesCostService(IEfRepository<Da_Service_Order_Cope> copeRepository, IEfRepository<Da_Service_Order_Rec> recRepository, IEfRepository<Da_Service_Order_Engineer> engineerRepository, ILogger<AfterSalesOrderService> logger, ISystemRestClient systemRestClient, IEfRepository<Da_Service_Order> orderRepository, IUnitOfWork unitOfWork, IEfRepository<Da_Service_Eng_Grant> grantRepository, IStaffRestClient staffRestClient)
        {
            _copeRepository = copeRepository;
            _recRepository = recRepository;
            _engineerRepository = engineerRepository;
            _logger = logger;
            _systemRestClient = systemRestClient;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _grantRepository = grantRepository;
            _staffRestClient = staffRestClient;
        }

        public async Task<AppSrvResult> SetPayrollRecords(GeneratePayrollDto input)
        {
            input.TrimStringFields();
            if(input.Operator == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GeneratePayrollRecords", "01");
                return Problem(HttpStatusCode.BadRequest, "操作人员不正确", instance: errMsg);
            }
            var serviceOrderList = await _orderRepository.Where(x => input.Ids.Contains(x.Id), noTracking : false).ToListAsync();
            serviceOrderList.ForEach(x =>
            {
                x.CommissionDistributionMonth = input.SetMonth;
                x.CommissionDistributionPersonnel = input.Operator;
                x.CommissionDistributionTime = DateTime.Now;
                x.IsCommission = 1;
            });
            await _orderRepository.UpdateRangeAsync(serviceOrderList);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<ServiceOrderCopeDto>> GetCopeInfo(long serviceInfoId, string serviceOrderId)
        {
            if(serviceInfoId == 0 || string.IsNullOrEmpty(serviceOrderId))
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetCopeInfo", "01");
                return Problem(HttpStatusCode.NotFound, "客户档案、服务工单号不正确", instance: errMsg);
            }
            var serviceOrderCope = await _copeRepository.FetchAsync(x => x, x => x.ServiceInfoId == serviceInfoId && x.ServiceOrderId.Equals(serviceOrderId));
            if(serviceOrderCope == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetCopeInfo", "02");
                return Problem(HttpStatusCode.NotFound, "应收不存在", instance: errMsg);
            }
            var serviceOrderCopeDto = Mapper.Map<ServiceOrderCopeDto>(serviceOrderCope);
            return serviceOrderCopeDto;
        }

        public async Task<AppSrvResult<List<ServiceOrderEngineerDto>>> GetEngineerInfo(long serviceInfoId, string serviceOrderId)
        {
            if (serviceInfoId == 0 || string.IsNullOrEmpty(serviceOrderId))
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetEngineerInfo", "01");
                return Problem(HttpStatusCode.NotFound, "客户档案、服务工单号不正确", instance: errMsg);
            }
            var serviceOrderEngineers = await _engineerRepository.Where(x => x.ServiceInfoId == serviceInfoId && x.ServiceOrderId.Equals(serviceOrderId))
                .ToListAsync();
            var serviceOrderEngineerDtos = Mapper.Map<List<ServiceOrderEngineerDto>>(serviceOrderEngineers);
            return serviceOrderEngineerDtos;
        }

        public async Task<AppSrvResult<ServiceOrderRecDto>> GetRecInfo(long serviceInfoId, string serviceOrderId)
        {
            if (serviceInfoId == 0 || string.IsNullOrEmpty(serviceOrderId))
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetRecInfo", "01");
                return Problem(HttpStatusCode.NotFound, "客户档案、服务工单号不正确", instance: errMsg);
            }
            var serviceOrderRec = await _recRepository.FetchAsync(x => x, x => x.ServiceInfoId == serviceInfoId && x.ServiceOrderId.Equals(serviceOrderId));
            if (serviceOrderRec == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetRecInfo", "02");
                return Problem(HttpStatusCode.NotFound, "应付不存在", instance: errMsg);
            }
            var serviceOrderRecDto = Mapper.Map<ServiceOrderRecDto>(serviceOrderRec);
            try
            {
                if (serviceOrderRecDto.Reviewer > 0)
                {
                    var response = await _staffRestClient.GetCodeBmStaffListAsync(serviceOrderRec.CompanyId);
                    if (response.IsSuccessStatusCode && response.Content.Count > 0)
                    {
                        serviceOrderRecDto.ReviewerName = response.Content.FirstOrDefault(x => x.Id == serviceOrderRecDto.Reviewer).StaffName;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("人员信息获取失败:{message}", ex.Message);
            }
            return serviceOrderRecDto;
        }

        public async Task<AppSrvResult> ReviewRec(long id, long reviewer)
        {
            if(id == 0 || reviewer == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReviewRec", "01");
                return Problem(HttpStatusCode.NotFound, "应收信息或审核人员不正确", instance: errMsg);
            }
            var isExists = await _recRepository.AnyAsync(x => x.Id == id);
            if(!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReviewRec", "02");
                return Problem(HttpStatusCode.NotFound, "应收信息或审核人员不正确", instance: errMsg);
            }
            var serviceOrderRec = new Da_Service_Order_Rec();
            serviceOrderRec.Id = id;
            serviceOrderRec.Reviewer = reviewer;
            serviceOrderRec.ReviewTime = DateTime.Now;
            await _recRepository.UpdateAsync(serviceOrderRec, UpdatingProps<Da_Service_Order_Rec>(x => x.Reviewer, x => x.ReviewTime));
            return AppSrvResult();
        }

        public async Task<AppSrvResult> SetCopeInfo(ServiceOrderCopeUpdationDto input)
        {
            input.TrimStringFields();
            var isExists = await _copeRepository.AnyAsync(x => x.Id == input.Id);
            if(!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetCopeInfo", "01");
                return Problem(HttpStatusCode.BadRequest, "支出信息不存在或已删除", instance: errMsg);
            }
            //var serviceOrderCope = Mapper.Map<Da_Service_Order_Cope>(input);
            var serviceOrderCope = await _copeRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking : false);
            ConditionalMap(input, serviceOrderCope);
            var disbursement = Math.Round(input.Tocome1 + input.Tocome2 + input.Tocome3 + input.Tocome4 + input.Tocome5 + input.Tocome6 + input.Tocome7 + input.Tocome8 + input.Tocome9 + input.Tocome10 + input.Tocome11, 2);
            var serviceOrder = await _orderRepository.FetchAsync(x => x, x => x.CompanyID == serviceOrderCope.CompanyId && x.ServiceOrderID.Equals(serviceOrderCope.ServiceOrderId) && x.ServiceInfoID == serviceOrderCope.ServiceInfoId, noTracking: false);
            if (serviceOrder == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetCopeInfo", "02");
                return Problem(HttpStatusCode.BadRequest, "支出信息对应工单不存在或已删除", instance: errMsg);
            }
            try
            {
                await _copeRepository.UpdateAsync(serviceOrderCope);
                serviceOrder.Disbursement = disbursement;
                await _orderRepository.UpdateAsync(serviceOrder);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetCopeInfo", "03");
                return Problem(HttpStatusCode.BadRequest, "设置支出信息时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> SetEngineerInfo(ServiceOrderEngineerUpdationDto input)
        {
            input.TrimStringFields();
            var isExists = await _engineerRepository.AnyAsync(x => x.Id == input.Id);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetCopeInfo", "01");
                return Problem(HttpStatusCode.BadRequest, "工程师提成不存在或已删除", instance: errMsg);
            }
            //var serviceOrderCope = Mapper.Map<Da_Service_Order_Cope>(input);
            var serviceOrderEngineer = await _engineerRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking: false);
            ConditionalMap(input,serviceOrderEngineer);
            serviceOrderEngineer.Takessum = Math.Round(serviceOrderEngineer.Takes1 + serviceOrderEngineer.Takes2 + serviceOrderEngineer.Takes3
                 + serviceOrderEngineer.Takes4 + serviceOrderEngineer.Takes5 + serviceOrderEngineer.Takes6 + serviceOrderEngineer.Takes7 + serviceOrderEngineer.Takes8 + serviceOrderEngineer.Takes9 + serviceOrderEngineer.Takes10 + serviceOrderEngineer.Takes11, 2);
            await _engineerRepository.UpdateAsync(serviceOrderEngineer);
            var serviceOrder = await _orderRepository.FetchAsync(x => x, x => x.ServiceInfoID == serviceOrderEngineer.ServiceInfoId && x.ServiceOrderID.Equals(serviceOrderEngineer.ServiceOrderId), noTracking : false);
            serviceOrder.SettlementPersonnel = input.Operator;
            serviceOrder.SettlementTime = DateTime.Now;
            if (input.IsWeek == 1)
            {

                DateTime currentDate = DateTime.Today;
                int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                serviceOrder.SettlementWeek = weekNumber;
            }
            serviceOrder.IsSettlement = 1;
            await _orderRepository.UpdateAsync(serviceOrder);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> SetRecInfo(ServiceOrderRecUpdationDto input)
        {
            input.TrimStringFields();
            var isExists = await _recRepository.AnyAsync(x => x.Id == input.Id);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetRecInfo", "01");
                return Problem(HttpStatusCode.BadRequest, "收费信息不存在或已删除", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var serviceOrderRec = await _recRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking: false);
            ConditionalMap(input,serviceOrderRec);
            //var serviceOrderCope = Mapper.Map<Da_Service_Order_Cope>(input);
            var offbailIncome = Math.Round(input.Income1 + input.Income2 + input.Income3 + input.Income4 + input.Income5 + input.Income6 + input.Income7 + input.Income8 + input.Income9 + input.Income10 + input.Income11 - input.DiscountAmount, 2);
            try
            {
                await _recRepository.UpdateAsync(serviceOrderRec);
                var serviceOrder = await _orderRepository.FetchAsync(x => x, x => x.CompanyID == serviceOrderRec.CompanyId && x.ServiceInfoID == serviceOrderRec.ServiceInfoId && x.ServiceOrderID.Equals(serviceOrderRec.ServiceOrderId), noTracking: false);
                if(serviceOrder == null)
                {
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetRecInfo", "02");
                    return Problem(HttpStatusCode.BadRequest, "收费信息对应工单不存在或已删除", instance: errMsg);
                }
                serviceOrder.GuaranteedIncomeBusiness = Math.Round(serviceOrderRec.GuaranteeDIncomeBusiness, 2);
                serviceOrder.GuaranteedIncomeManufactor = Math.Round(serviceOrderRec.GuaranteeDIncomeManufactor, 2);
                serviceOrder.Offbailincome = offbailIncome;
                await _orderRepository.UpdateAsync(serviceOrder);
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetRecInfo", "03");
                return Problem(HttpStatusCode.BadRequest, "设置收费信息时出现意外错误", instance: errMsg, title : ex.InnerException.Message);
            }
            
            return AppSrvResult();
        }

        public async Task<AppSrvResult> GeneratePayrollRecords(long companyid, string setmonth)
        {
            if(companyid == 0 || setmonth.IsNullOrWhiteSpace())
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "SetRecInfo", "01");
                return Problem(HttpStatusCode.BadRequest, "公司或结算月份不正确", instance: errMsg);
            }
            // 生成前先清空
            await _grantRepository.DeleteRangeAsync(x => x.CompanyId == companyid && x.SettlementMonth == setmonth);
            var serviceOrderList = _orderRepository.Where(x => x.CompanyID == companyid && x.CommissionDistributionMonth.Equals(setmonth));
            var serviceOrderEngineer = _engineerRepository.Where(x => x.CompanyId == companyid);
            var serviceOrderEngineerGrantDtos = await (from a in serviceOrderEngineer
                                                       join b in serviceOrderList on a.CompanyId equals b.CompanyID into groupResult
                                                       from c in groupResult.DefaultIfEmpty()
                                                       where a.CompanyId == companyid && a.ServiceOrderId == c.ServiceOrderID && c.CommissionDistributionMonth == setmonth
                                                       group new { a,c } by new {a.EngineerId, a.EngineerName,c.CommissionDistributionMonth,a.CompanyId, c.SettlementWeek} into g
                                                       select new ServiceOrderEngineerGrantCreationDto
                                                       {
                                                           CompanyId = companyid, 
                                                           SettlementMonth = setmonth, 
                                                           SettlementWeek = g.Key.SettlementWeek,
                                                           EngineerId = g.Key.EngineerId, 
                                                           EngineerName = g.Key.EngineerName,
                                                           TakesSum = g.Sum(x => x.a.Takessum),
                                                           Takes1 = g.Sum(x => x.a.Takes1),
                                                           Takes2 = g.Sum(x => x.a.Takes2),
                                                           Takes3 = g.Sum(x => x.a.Takes3),
                                                           Takes4 = g.Sum(x => x.a.Takes4),
                                                           Takes5 = g.Sum(x => x.a.Takes5),
                                                           Takes6 = g.Sum(x => x.a.Takes6),
                                                           Takes7 = g.Sum(x => x.a.Takes7),
                                                           Takes8 = g.Sum(x => x.a.Takes8),
                                                           Takes9 = g.Sum(x => x.a.Takes9),
                                                           Takes10 = g.Sum(x => x.a.Takes10),
                                                           Takes11 = g.Sum(x => x.a.Takes11),
                                                           GrantStatus = "0",
                                                       }).ToListAsync();
            var serviceOrderEngineerGrantList = Mapper.Map<List<Da_Service_Eng_Grant>>(serviceOrderEngineerGrantDtos);
            serviceOrderEngineerGrantList.ForEach(x => x.Id = IdGenerater.GetNextId());
            await _grantRepository.InsertRangeAsync(serviceOrderEngineerGrantList);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<List<ServiceOrderEngineerGrantDto>>> GetPayrollRecordsList(long companyid, string setmonth)
        {
            if (companyid == 0 || setmonth.IsNullOrWhiteSpace())
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetPayrollRecordsList", "01");
                return Problem(HttpStatusCode.BadRequest, "公司或结算月份不正确", instance: errMsg);
            }
            var serviceOrderEngineerGrantList = await _grantRepository.Where(x => x.CompanyId == companyid && x.SettlementMonth == setmonth).ToListAsync();
            return Mapper.Map<List<ServiceOrderEngineerGrantDto>>(serviceOrderEngineerGrantList);
        }

        public async Task<AppSrvResult<List<ServiceOrderRecDto>>> GetUnRecInfo(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetUnRecInfo", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceOrderRec = await _recRepository.Where(x => x.CompanyId == companyid && x.DiscountAmount > 0 && x.Reviewer == 0).ToListAsync() ;
            var serviceOrderRecDtos = Mapper.Map<List<ServiceOrderRecDto>>(serviceOrderRec);

            try
            {

                var response = await _staffRestClient.GetCodeBmStaffListAsync(companyid);
                if (response.IsSuccessStatusCode && response.Content.Count > 0)
                {
                    serviceOrderRecDtos.ForEach(x =>
                    {
                        if(x.Reviewer > 0)
                        {
                            var staff = response.Content.FirstOrDefault(y => y.StaffID.Equals(x.Reviewer.ToString()));
                            x.ReviewerName = staff == null ? "" : staff.StaffName;
                        }
                    });
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("人员信息获取失败:{message}", ex.Message);
            }
            return serviceOrderRecDtos;
        }

        public async Task<AppSrvResult> MarkedWagePayment(ServiceOrderEngineerGrantUpdationDto input)
        {
            input.TrimStringFields();
            if(input.Ids.Count() == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "MarkedWagePayment", "01");
                return Problem(HttpStatusCode.BadRequest, "未指定提成列表", instance: errMsg);
            }
            var ids = input.Ids.ToArray();
            var serviceOrderEngineerGrants = await _grantRepository.Where(x => ids.Contains(x.Id), noTracking : false).ToListAsync();
            serviceOrderEngineerGrants.ForEach(x =>
            {
                x.GrantStatus = input.GrantStatus;
                x.GrantTime = DateTime.Now;
            });
            await _grantRepository.UpdateRangeAsync(serviceOrderEngineerGrants);
            return AppSrvResult();
        }
    }
}
