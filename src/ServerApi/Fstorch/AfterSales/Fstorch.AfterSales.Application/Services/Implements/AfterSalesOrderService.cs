using Fstorch.AfterSales.Application.SingletonHub;
using Newtonsoft.Json;

namespace Fstorch.AfterSales.Application.Services.Implements
{
    public class AfterSalesOrderService : AbstractAppService, IAfterSalesOrderService
    {
        private readonly ILogger<AfterSalesOrderService> _logger;
        private readonly ISystemRestClient _systemRestClient;
        private readonly IEfRepository<Da_Service_Order> _serviceOrderRepository;
        private readonly IEfRepository<Da_Service_Main> _mainRepository;
        private readonly IEfRepository<Da_Service_Order_Process> _processRepository;
        private readonly IEfRepository<Da_Service_Product> _productRepository;
        private readonly IEfRepository<Da_Service_Order_Produc> _orderProductRepository;
        private readonly IEfRepository<Bm_Service_Brand> _brandRepository;
        private readonly IEfRepository<Bm_Brand_Content> _contentRepository;
        private readonly IEfRepository<Da_Service_Order_Engineer> _engineerRepository;
        private readonly IEfRepository<Da_Service_Order_Rec> _recRespository;
        private readonly IEfRepository<Da_Service_Order_Cope> _copeRepository;
        private readonly IEfRepository<Da_Service_Followup> _followupRepository;
        private readonly NotificationServiceFromSignalR _notification;
        private readonly DbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CacheService _cacheService;
        private readonly IStaffRestClient _staffRestClient;
        public AfterSalesOrderService(ILogger<AfterSalesOrderService> logger, ISystemRestClient systemRestClient, IEfRepository<Da_Service_Order> serviceOrderRepository, IEfRepository<Da_Service_Main> mainRepository, IEfRepository<Da_Service_Order_Process> processRepository, IEfRepository<Da_Service_Product> productRepository, IUnitOfWork unitOfWork, IEfRepository<Da_Service_Order_Produc> orderProductRepository, CacheService cacheService, DbContext dbContext, IStaffRestClient staffRestClient, IEfRepository<Bm_Service_Brand> brandRepository, IEfRepository<Bm_Brand_Content> contentRepository, IEfRepository<Da_Service_Order_Engineer> engineerRepository, IEfRepository<Da_Service_Order_Rec> recRespository, IEfRepository<Da_Service_Order_Cope> copeRepository, IEfRepository<Da_Service_Followup> followupRepository, NotificationServiceFromSignalR notification)
        {
            _logger = logger;
            _systemRestClient = systemRestClient;
            _serviceOrderRepository = serviceOrderRepository;
            _mainRepository = mainRepository;
            _processRepository = processRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _orderProductRepository = orderProductRepository;
            _cacheService = cacheService;
            _dbContext = dbContext;
            _staffRestClient = staffRestClient;
            _contentRepository = contentRepository;
            _brandRepository = brandRepository;
            _engineerRepository = engineerRepository;
            _recRespository = recRespository;
            _copeRepository = copeRepository;
            _followupRepository = followupRepository;
            _notification = notification;
        }

        public async Task<AppSrvResult> CancelWorkOrderAsync(ServiceOrderCancelDto input)
        {
            if(input.Id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CancelWorkOrderAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking : false);
            if(serviceOrder == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CancelWorkOrderAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单不存在或已删除", instance: errMsg);
            }
            
            try
            {
                serviceOrder.ServiceOrderStatus = "C";
                await _serviceOrderRepository.UpdateAsync(serviceOrder, UpdatingProps<Da_Service_Order>(x => x.ServiceOrderStatus));
                var serviceMainId = serviceOrder.ServiceInfoID;
                //检查是否还有其他产品正在服务中，如果没有则更新服务客户档案为未派工
                var isExists = await _serviceOrderRepository.AnyAsync(x => x.ServiceInfoID == serviceMainId && x.Id != input.Id && !x.ServiceOrderStatus.Equals("C"));
                if(!isExists)
                {
                    await _mainRepository.UpdateAsync(new Da_Service_Main { Id = serviceMainId, ServiceStatus = "1" }, UpdatingProps<Da_Service_Main>(x => x.ServiceStatus));
                }
                string _operator = "";
                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(serviceOrder.CompanyID);
                    if (staffResponse.IsSuccessStatusCode)
                    {
                        var staffList = staffResponse.Content;
                        if (staffList.Count > 0)
                            _operator = staffList.FirstOrDefault(x => x.Id == input.Operator).StaffName;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取人事信息异常:{message}", ex.Message);
                    _operator = "";
                }
                var orderProcess = new Da_Service_Order_Process
                {
                    Id = IdGenerater.GetNextId(),
                    ServiceInfoId = serviceOrder.Id,
                    CompanyId = serviceOrder.CompanyID,
                    ServiceOrderId = serviceOrder.ServiceOrderID,
                    ServiceProcessId = serviceOrder.ServiceprocessID,
                    ServiceSpecificationId = "",
                    SerialNumber = 99,
                    ProcessType = "工单作废",
                    OperationPersonnel = serviceOrder.CompanyDispatchPersonnel.ToString(),
                    OperationTime = DateTime.Now,
                    ProcessReasons = $"[作废人员]:{_operator},[作废原因]:{input.Reason}, {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}"
                };
                await _processRepository.InsertAsync(orderProcess);
                await _unitOfWork.CommitAsync();
                return AppSrvResult();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("作废工单时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CancelWorkOrderAsync", "04");
                return Problem(HttpStatusCode.BadRequest, "作废工单时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult> ComplatedWorkOrderAsync(ServiceOrderComplateDto input)
        {
            if(input.Id == 0 || input.CompanyEndPersonnel == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ComplatedWorkOrderAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单或完工人员不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking: false);

            if (serviceOrder == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ComplatedWorkOrderAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单不存在或已删除", instance: errMsg);
            }
            try
            {
                serviceOrder.ServiceOrderStatus = "B";
                serviceOrder.CompanyEndPersonnel = input.CompanyEndPersonnel;
                serviceOrder.CompanyEndTime = DateTime.Now;
                serviceOrder.ProcessingEndTime = DateTime.Now;
                serviceOrder.ServiceMeasures = input.ServiceMeasures;
                serviceOrder.ServiceResult = input.ServiceResult;
                serviceOrder.Disbursement = Math.Round(input.Cope.Tocome1 + input.Cope.Tocome2 + input.Cope.Tocome3 + input.Cope.Tocome4 + input.Cope.Tocome5 + input.Cope.Tocome6 + input.Cope.Tocome7 + input.Cope.Tocome8 + input.Cope.Tocome9 + input.Cope.Tocome10
                    + input.Cope.Tocome11, 2);
                serviceOrder.GuaranteedIncomeManufactor = Math.Round(input.Rec.GuaranteeDIncomeManufactor,2);
                serviceOrder.GuaranteedIncomeBusiness = Math.Round(input.Rec.GuaranteeDIncomeBusiness, 2);
                serviceOrder.Offbailincome = Math.Round(input.Rec.Income1 + input.Rec.Income2 + input.Rec.Income3 + input.Rec.Income4 + input.Rec.Income5 + input.Rec.Income6 + input.Rec.Income7 + input.Rec.Income8 + input.Rec.Income9 + input.Rec.Income10 + input.Rec.Income11, 2);
                await _serviceOrderRepository.UpdateAsync(serviceOrder);
                var serviceProcessIds = await _productRepository.Where(x => x.CompanyID == serviceOrder.CompanyID && x.ServiceInfoID ==  serviceOrder.ServiceInfoID).Select(x => x.ServiceProcess).Distinct().ToArrayAsync();
                var isFinalProcess = false; //后续调用其他服务接口，查询当前客户所有产品是否都为最终流程阶段
                try
                {

                    var processStatusResponse = await _systemRestClient.GetCodeBmServiceProcessAsync(serviceOrder.CompanyID);
                    if (processStatusResponse.IsSuccessStatusCode)
                    {
                        var processStatusList = processStatusResponse.Content;
                        if(processStatusList.Count > 0)
                        {
                            var endNum = processStatusList.Where(x => serviceProcessIds.Contains(x.ServiceProcessID) && x.IsEndProcess.Equals("1")).Count();
                            if(endNum == serviceProcessIds.Length)
                                isFinalProcess = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取公司服务流程出错:{message}", ex.Message);
                }
                if (isFinalProcess)
                {
                    //所有产品最终流程对应的完工工单数量
                    var complatedNum = await _serviceOrderRepository.CountAsync(x => x.ServiceInfoID == serviceOrder.ServiceInfoID && serviceProcessIds.Contains(x.ServiceprocessID) && x.ServiceOrderStatus.Equals("B"));
                    var isAllComplated = false;
                    //验证所有产品最终流程与完工工单数量是否一致
                    if (complatedNum == serviceProcessIds.Length) 
                        isAllComplated = true;
                    if (isAllComplated)
                    {
                        //更新客户档案状态为已完工
                        await _mainRepository.UpdateAsync(new Da_Service_Main { Id = serviceOrder.ServiceInfoID, ServiceStatus = "3" }, UpdatingProps<Da_Service_Main>(x => x.ServiceStatus));
                    }
                }
                string _operator = "";
                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(serviceOrder.CompanyID);
                    if (staffResponse.IsSuccessStatusCode)
                    {
                        var staffList = staffResponse.Content;
                        if(staffList.Count > 0)
                            _operator = staffList.FirstOrDefault(x => x.Id == input.CompanyEndPersonnel).StaffName;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取人事信息异常:{message}", ex.Message);
                    _operator = "";
                }
                var orderProcess = new Da_Service_Order_Process
                {
                    Id = IdGenerater.GetNextId(),
                    ServiceInfoId = serviceOrder.Id,
                    CompanyId = serviceOrder.CompanyID,
                    ServiceOrderId = serviceOrder.ServiceOrderID,
                    ServiceProcessId = serviceOrder.ServiceprocessID,
                    ServiceSpecificationId = input.ServiceSpecificationId,
                    SerialNumber = 1,
                    ProcessType = "工单完工",
                    OperationPersonnel = input.CompanyEndPersonnel.ToString(),
                    OperationTime = DateTime.Now,
                    ProcessReasons = $"产品工单已完工, [完工人员]:{_operator} [{input.CompanyEndPersonnel}], [服务结果]:{serviceOrder.ServiceResult}    {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}"
                };
                await _processRepository.InsertAsync(orderProcess);

                //录入服务过程应收和应付
                var cope = Mapper.Map<Da_Service_Order_Cope>(input.Cope);
                var rec = Mapper.Map<Da_Service_Order_Rec>(input.Rec);
                cope.Id = IdGenerater.GetNextId();
                cope.ServiceInfoId = serviceOrder.ServiceInfoID;
                cope.CompanyId = serviceOrder.CompanyID;
                cope.ServiceOrderId = serviceOrder.ServiceOrderID;

                rec.Id = IdGenerater.GetNextId();
                rec.ServiceInfoId = serviceOrder.ServiceInfoID;
                rec.CompanyId = serviceOrder.CompanyID;
                rec.ServiceOrderId = serviceOrder.ServiceOrderID;

                await _copeRepository.InsertAsync(cope);
                await _recRespository.InsertAsync(rec);

                var allClients = await _cacheService.GetClientsFromCacheAsync();
                var clients = allClients.Where(x => serviceOrder.CompanyDispatchPersonnel.ToString().Equals(x.UserId) && x.CompanyId == serviceOrder.CompanyID).ToList();
                if (clients.Count > 0)
                {
                    _ = Task.Run(() =>
                    {
                        foreach (var client in clients)
                        {
                            var obj = new { message = "工单已完工，请及时处理", type = "工单", orderid = serviceOrder.Id, time = DateTime.Now, Operator = _operator };
                            _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                        }
                    });
                }

                await _unitOfWork.CommitAsync();
                //Task.Run()
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("工单完工时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ComplatedWorkOrderAsync", "04");
                return Problem(HttpStatusCode.BadRequest, "工单完工时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long>> CreateProcessAsync(ServiceOrderProcessCreationDto input)
        {
            input.TrimStringFields();
            if(input.CompanyId == 0 || input.ServiceInfoId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateProcessAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单或公司不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var serviceOrderProcess = Mapper.Map<Da_Service_Order_Process>(input);
            serviceOrderProcess.Id = IdGenerater.GetNextId();
            serviceOrderProcess.OperationTime = DateTime.Now;
            if (serviceOrderProcess.ProcessType.IsNullOrWhiteSpace())
                serviceOrderProcess.ProcessType = "过程反馈";
            try
            {
                await _processRepository.InsertAsync(serviceOrderProcess);
                if (serviceOrderProcess.ProcessType.Equals("工程师拒单"))
                {
                    var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == serviceOrderProcess.ServiceInfoId, noTracking: false);
                    serviceOrder.EngineerCacelTime = DateTime.Now;
                    serviceOrder.EngineerCacelReason = input.ProcessReasons;
                    await _serviceOrderRepository.UpdateAsync(serviceOrder);

                    var allClients = await _cacheService.GetClientsFromCacheAsync();
                    var clients = allClients.Where(x => serviceOrder.CompanyDispatchPersonnel.ToString().Equals(x.UserId) && x.CompanyId == serviceOrder.CompanyID).ToList();
                    if (clients.Count > 0)
                    {
                        _ = Task.Run(() =>
                        {
                            foreach (var client in clients)
                            {
                                var obj = new { message = "工单已被工程师拒单，请及时审核", type = "工单", orderid = serviceOrder.Id, time = DateTime.Now, Operator = serviceOrder.ServicestaffName };
                                _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                            }
                        });
                    }
                }
                else if (serviceOrderProcess.ProcessType.Equals("服务商拒单"))
                {
                    var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == serviceOrderProcess.ServiceInfoId, noTracking: false);
                    serviceOrder.ProviderCacelTime = DateTime.Now;
                    serviceOrder.ProviderCacleReason = input.ProcessReasons;
                    await _serviceOrderRepository.UpdateAsync(serviceOrder);
                    var allClients = await _cacheService.GetClientsFromCacheAsync();
                    var clients = allClients.Where(x => serviceOrder.ProviderDispatchPersonnel.ToString().Equals(x.UserId) && x.CompanyId == serviceOrder.CompanyID).ToList();
                    if (clients.Count > 0)
                    {
                        _ = Task.Run(() =>
                        {
                            foreach (var client in clients)
                            {
                                var obj = new { message = "工单已被服务商拒单，请及时审核", type = "工单", orderid = serviceOrder.Id, time = DateTime.Now, Operator = serviceOrder.ServiceProviderID };
                                _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                            }
                        });
                    }
                }
                await _unitOfWork.CommitAsync();
                return serviceOrderProcess.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("新增过程反馈记录出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateProcessAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "新增过程反馈记录出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long>> DispatchWorkOrderAsync(ServiceOrderCreationDto input)
        {
            input.TrimStringFields();
            if(input.ServiceInfoID == 0 || input.CompanyID == 0 || input.CompanyDispatchPersonnel == 0 || input.ProductSerial.Count() == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "客户档案、公司、产品明细、派工人员不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var productSerial = input.ProductSerial.ToArray();
            var currentOrderProducts = await _orderProductRepository.Where(x => x.CompanyId == input.CompanyID && productSerial.Contains(x.ProductSerial) && x.ServiceInfoId == input.ServiceInfoID && x.ServiceProcessId.Equals(input.ServiceprocessID))
                .Select(x => x.ServiceOrderId)
                .ToArrayAsync();
            if(currentOrderProducts.Length > 0)
            {
                var isExists = await _serviceOrderRepository.AnyAsync(x => currentOrderProducts.Contains(x.ServiceOrderID) && x.CompanyID == input.CompanyID && !x.ServiceOrderStatus.Equals("C"));
                if (isExists)
                {
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderAsync", "02");
                    return Problem(HttpStatusCode.BadRequest, "存在已派工的产品,请勿重复操作", instance: errMsg);
                }
            }

            var engineerIds = input.ServicestaffID.Split(',');
            var engineerNames = input.ServicestaffName.Split(',');
            var serviceOrder = Mapper.Map<Da_Service_Order>(input);
            serviceOrder.Id = IdGenerater.GetNextId();
            serviceOrder.ServiceOrderStatus = "A";
            serviceOrder.CompanyDispatchTime = DateTime.Now;
            serviceOrder.ProviderDispatchTime = DateTime.Now;
            // 生成新的工单号
            string newWorkOrderCode = await _cacheService.GenerateWorkOrderCodeForTodayAsync(input.CompanyID);
            if(newWorkOrderCode.IsNullOrWhiteSpace())
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "当前产品流程派工时单号获取出错", instance: errMsg);
            }
            serviceOrder.ServiceOrderID = newWorkOrderCode;
            //maxSerialNumber++;
            string _operator = "";
            List<Da_StaffListDto> staffRtos = new List<Da_StaffListDto>();
            try
            {
                var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(serviceOrder.CompanyID);
                if (staffResponse != null && staffResponse.IsSuccessStatusCode)
                {
                    staffRtos = staffResponse.Content;
                    if (staffRtos.Count > 0)
                        _operator = staffRtos.FirstOrDefault(x => x.Id == input.CompanyDispatchPersonnel).StaffName;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("获取人事信息异常:{message}", ex.Message);
                staffRtos = null;
                _operator = "";
            }

            try
            {
                await _serviceOrderRepository.InsertAsync(serviceOrder);
                //重新按照工单当日预约日期、时段排序
                if(serviceOrder.AppointmentDate != null)
                {
                    var resetGroup = new string[] { "B", "C" };
                    var serviceOrderSortedList = await _serviceOrderRepository.Where(x => !resetGroup.Contains(x.ServiceOrderStatus) && x.ServicestaffID.StartsWith(engineerIds[0]) && x.CompanyID == input.CompanyID && x.AppointmentDate == serviceOrder.AppointmentDate, noTracking: false)
                       .OrderBy(x => x.AppointmentDate)
                       .OrderBy(x => x.Appointmentperiod)
                       .ToListAsync();
                    if(serviceOrderSortedList.Count > 0)
                    {
                        var i = 1;
                        foreach (var item in serviceOrderSortedList)
                        {
                            item.SerialNumber = i;
                            i++;
                        };
                        await _serviceOrderRepository.UpdateRangeAsync(serviceOrderSortedList);
                    }
                }
                var orderProcess = new Da_Service_Order_Process
                {
                    Id = IdGenerater.GetNextId(),
                    ServiceInfoId = serviceOrder.Id,
                    CompanyId = serviceOrder.CompanyID,
                    ServiceOrderId = serviceOrder.ServiceOrderID,
                    ServiceProcessId = serviceOrder.ServiceprocessID,
                    ServiceSpecificationId = input.ServiceSpecificationId,
                    SerialNumber = 1,
                    ProcessType = "指派工程师",
                    OperationPersonnel = input.CompanyDispatchPersonnel.ToString(),
                    OperationTime = DateTime.Now,
                    ProcessReasons = $"指派工程师[{serviceOrder.ServicestaffID} => {serviceOrder.ServicestaffName}], [派工人员]:{_operator},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}"
                };
                await _processRepository.InsertAsync(orderProcess);
                var serviceProduct = await _productRepository.FetchAsync(x => x, x => x.CompanyID == input.CompanyID && x.ServiceInfoID == input.ServiceInfoID && productSerial.Contains(x.ProductSerial), noTracking : false);
                if(serviceProduct == null)
                {
                    await _unitOfWork.RollbackAsync();
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderAsync", "05");
                    return Problem(HttpStatusCode.BadRequest, "指定产品不存在", instance: errMsg);
                }
                serviceProduct.ServiceProcess = input.ServiceprocessID;
                List<Da_Service_Order_Produc> orderProducts = new List<Da_Service_Order_Produc>();
                foreach (var product in productSerial)
                {
                    var orderProduct = new Da_Service_Order_Produc
                    {
                        Id = IdGenerater.GetNextId(),
                        ServiceInfoId = serviceOrder.ServiceInfoID,
                        ServiceOrderId = serviceOrder.ServiceOrderID,
                        ProductSerial = product,
                        ServiceProcessId = input.ServiceprocessID,
                        CompanyId = input.CompanyID
                    };
                    orderProducts.Add(orderProduct);
                }
                await _orderProductRepository.InsertRangeAsync(orderProducts);
                //工程师信息录入提成
                var proportion = Math.Round(1m / engineerIds.Length, 2);
                List<Da_Service_Order_Engineer> orderEngineers = new List<Da_Service_Order_Engineer>();
                for (var index = 0; index < engineerIds.Length; index++)
                {
                    var department = "";
                    if (staffRtos != null && staffRtos.Count > 0)
                        department = staffRtos.FirstOrDefault(x => x.Id.Equals(engineerIds[index].ToLong())).DepartMentID.ToString();
                    var orderEngineer = new Da_Service_Order_Engineer
                    {
                        Id = IdGenerater.GetNextId(),
                        CompanyId = input.CompanyID,
                        ServiceInfoId = serviceOrder.ServiceInfoID,
                        ServiceOrderId = serviceOrder.ServiceOrderID,
                        EngineerId = (long)engineerIds[index].ToLong(),
                        EngineerName = engineerNames[index],
                        Proportion = proportion,
                        DepartmentId = department
                    };
                    orderEngineers.Add(orderEngineer);
                }
                await _engineerRepository.InsertRangeAsync(orderEngineers);
                await _productRepository.UpdateAsync(serviceProduct);
                await _mainRepository.UpdateAsync(new Da_Service_Main { Id = input.ServiceInfoID, ServiceStatus = "2" }, UpdatingProps<Da_Service_Main>(x => x.ServiceStatus));
                await _unitOfWork.CommitAsync();
                var allClients = await _cacheService.GetClientsFromCacheAsync();
                var clients = allClients.Where(x => engineerIds.Contains(x.UserId) && x.CompanyId == input.CompanyID).ToList();
                if(clients.Count > 0)
                {
                    _ = Task.Run(() =>
                    {
                        foreach (var client in clients)
                        {
                            var obj = new { message = "您有一张新工单待处理，请及时查阅", type = "工单", orderid = serviceOrder.Id, time = DateTime.Now, dispatchOperator = _operator };
                            _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                        }
                    });
                }
                return serviceOrder.Id;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("当前产品流程派工时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderAsync", "04");
                return Problem(HttpStatusCode.BadRequest, "当前产品流程派工时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long[]>> DispatchWorkOrderBatchAsync(ServiceOrderDispatchBatchDto input)
        {
            input.TrimStringFields();
            if (input.Ids.Count() == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderBatchAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "客户档案不正确", instance: errMsg);
            }
            //var serviceOrder = Mapper.Map<Da_Service_Order>(input);
            List<long> orders = new List<long>();
            try
            {
                var brands = await _brandRepository.Where(x => x.CompanyId == input.CompanyID, writeDb : true).ToListAsync();
                var brandContents = await _contentRepository.Where(x => x.CompanyId == input.CompanyID, writeDb: true).ToListAsync();
                foreach (var id in input.Ids)
                {
                    var serviceProducts = await _productRepository.Where(x => x.ServiceInfoID == id, writeDb: true).OrderBy(x => x.ServiceProcess).ToListAsync();
                    var groups = serviceProducts.GroupBy(x => x.ServiceProcess).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
                    if(groups.Count > 0)
                    {
                        foreach(var group in groups)
                        {
                            //查询系统编码服务查询流程对应的规范
                            var serviceSpecificationId = "";

                            var serviceOrder = new Da_Service_Order
                            {
                                ServiceInfoID = id,
                                CompanyID = input.CompanyID,
                                ServiceprocessID = group,
                                ServiceProviderID = input.ServiceProviderID,
                                ServicestaffID = input.ServicestaffID,
                                ServicestaffName = input.ServicestaffName,
                                CompanyDispatchPersonnel = input.CompanyDispatchPersonnel,
                                ProviderDispatchPersonnel = input.ProviderDispatchPersonnel,
                                ServiceRequest = input.ServiceRequest,
                            };
                            var serviceOrderDto = Mapper.Map<ServiceOrderCreationDto>(serviceOrder);
                            serviceOrderDto.ServiceSpecificationId = serviceSpecificationId;
                            var productSerials = serviceProducts.Where(x => x.ServiceProcess.Equals(group)).ToList();
                            serviceOrderDto.ProductSerial = productSerials.Select(x => x.ProductSerial).ToArray();
                            foreach (var product in productSerials)
                            {
                                var brand = brands.Find(x => x.Id == product.Branding.ToLong()).ServiceBrandName;
                                var brandContent = brandContents.Find(x => x.Id == product.BrandedContent.ToLong()).BrandContentName;
                                serviceOrderDto.Details += $"{brand}{brandContent}{product.Model}({product.ModelNumber});";
                            }
                            var orderid = DispatchWorkOrderAsync(serviceOrderDto).Result.Content;
                            orders.Add(orderid);
                            serviceProducts.RemoveAll(x => x.ServiceProcess.Equals(group));
                        }
                        if(serviceProducts.Count > 0)
                        {
                            foreach(var serviceProduct in serviceProducts)
                            {
                                var brand = brands.Find(x => x.Id == serviceProduct.Branding.ToLong()).ServiceBrandName;
                                var brandContent = brandContents.Find(x => x.Id == serviceProduct.BrandedContent.ToLong()).BrandContentName;
                                //查询系统编码服务查询流程对应的规范
                                var serviceSpecificationId = "";
                                List<int> productSerials = new List<int>();
                                productSerials.Add(serviceProduct.ProductSerial);
                                var serviceOrder = new Da_Service_Order
                                {
                                    ServiceInfoID = id,
                                    CompanyID = input.CompanyID,
                                    ServiceprocessID = serviceProduct.ServiceProcess,
                                    ServiceProviderID = input.ServiceProviderID,
                                    ServicestaffID = input.ServicestaffID,
                                    ServicestaffName = input.ServicestaffName,
                                    CompanyDispatchPersonnel = input.CompanyDispatchPersonnel,
                                    ProviderDispatchPersonnel = input.ProviderDispatchPersonnel,
                                    ServiceRequest = input.ServiceRequest,
                                    Details = $"{brand}{brandContent}{serviceProduct.Model}({serviceProduct.ModelNumber});",
                                    Appointmentperiod = ""
                                };
                                var serviceOrderDto = Mapper.Map<ServiceOrderCreationDto>(serviceOrder);
                                serviceOrderDto.ProductSerial = productSerials;
                                serviceOrderDto.ServiceSpecificationId = serviceSpecificationId;
                                var orderid = DispatchWorkOrderAsync(serviceOrderDto).Result.Content;
                                orders.Add(orderid);
                            }
                        }
                    }
                    else
                    {
                        foreach(var item in serviceProducts)
                        {
                            var s = item.Branding;
                            var f = item.BrandedContent;
                            var brand = brands.Find(x => x.Id == item.Branding.ToLong()).ServiceBrandName;
                            var brandContent = brandContents.Find(x => x.Id == item.BrandedContent.ToLong()).BrandContentName;
                            //查询系统编码服务查询流程对应的规范
                            var serviceSpecificationId = "";
                            List<int> productSerials = new List<int>();
                            var i = item.ProductSerial;
                            productSerials.Add(item.ProductSerial);
                            var serviceOrder = new Da_Service_Order
                            {
                                ServiceInfoID = id,
                                CompanyID = input.CompanyID,
                                ServiceprocessID = item.ServiceProcess,
                                ServiceProviderID = input.ServiceProviderID,
                                ServicestaffID = input.ServicestaffID,
                                ServicestaffName = input.ServicestaffName,
                                CompanyDispatchPersonnel = input.CompanyDispatchPersonnel,
                                ProviderDispatchPersonnel = input.ProviderDispatchPersonnel,
                                ServiceRequest = input.ServiceRequest,
                                Details = $"{brand}{brandContent}{item.Model}({item.ModelNumber});",
                                Appointmentperiod = ""
                            };
                            var serviceOrderDto = Mapper.Map<ServiceOrderCreationDto>(serviceOrder);
                            serviceOrderDto.ProductSerial = productSerials;
                            serviceOrderDto.ServiceSpecificationId = serviceSpecificationId;
                            var orderid = DispatchWorkOrderAsync(serviceOrderDto).Result.Content;
                            orders.Add(orderid);
                        }
                    }
                }
                return orders.ToArray();
            }
            catch(Exception ex)
            {
                _logger.LogError("批量派工时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DispatchWorkOrderBatchAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "批量派工时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
        }

        public async Task<AppSrvResult<object>> GetComplateCountReport(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetComplateCountReport", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var sdate = DateTime.Today;
            var edate = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var serviceOrderExpress = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => x.ProcessingEndTime >= sdate && x.ProcessingEndTime <= edate)
                .And(x => x.ServiceOrderStatus != "C");
            var serviceProductExpress = ExpressionCreator.New<Da_Service_Product>()
                .And(x => x.CompanyID == input.Companyid)
                .AndIf(input.Brand.IsNotNullOrWhiteSpace(), x => x.Branding.Equals(input.Brand))
                .AndIf(input.BrandContent.IsNotNullOrWhiteSpace(), x => x.BrandedContent.Equals(input.BrandContent))
                .AndIf(input.ServiceType.IsNotNullOrWhiteSpace(), x => x.ServiceTypeID.Equals(input.ServiceType));
            var serviceOrderQueryable = _serviceOrderRepository.Where(serviceOrderExpress);
            var serviceProductQueryable = _productRepository.Where(serviceProductExpress);
            var serviceOrderProductQueryable = _orderProductRepository.GetAll();
            var complateCount = 0;
            /*var dispatchCount = await _serviceOrderRepository.CountAsync(x => x.CompanyDispatchTime >= sdate && x.CompanyDispatchTime <= edate && x.CompanyID == input.Companyid && x.ServiceOrderStatus != "C");*/
            if (input.IsGroupBy == 1)
            {
                var complateCountGroup = await(from a in serviceOrderQueryable
                                               join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                               join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                               where b.ProductSerial == c.ProductSerial
                                               group a by new { a.ServicestaffName } into g
                                               select new { ServicestaffName = g.Key, Count = g.Count() })
                                       .ToListAsync();
                return complateCountGroup;
            }
            else if (input.IsGroupBy == 0)
            {
                complateCount = await(from a in serviceOrderQueryable
                                      join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                      join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                      where b.ProductSerial == c.ProductSerial
                                      select a.Id)
                                       .CountAsync();
            }
            return complateCount;
        }

        public async Task<AppSrvResult<object>> GetDispatchCountReport(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetDispatchCountReport", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var sdate = DateTime.Today;
            var edate = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var serviceOrderExpress = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => x.CompanyDispatchTime >= sdate && x.CompanyDispatchTime <= edate)
                .And(x => x.ServiceOrderStatus != "C");
            var serviceProductExpress = ExpressionCreator.New<Da_Service_Product>()
                .And(x => x.CompanyID == input.Companyid)
                .AndIf(input.Brand.IsNotNullOrWhiteSpace(), x => x.Branding.Equals(input.Brand))
                .AndIf(input.BrandContent.IsNotNullOrWhiteSpace(), x => x.BrandedContent.Equals(input.BrandContent))
                .AndIf(input.ServiceType.IsNotNullOrWhiteSpace(), x => x.ServiceTypeID.Equals(input.ServiceType));
            var serviceOrderQueryable = _serviceOrderRepository.Where(serviceOrderExpress);
            var serviceProductQueryable = _productRepository.Where(serviceProductExpress);
            var serviceOrderProductQueryable = _orderProductRepository.GetAll();
            var dispatchCount = 0;
            /*var dispatchCount = await _serviceOrderRepository.CountAsync(x => x.CompanyDispatchTime >= sdate && x.CompanyDispatchTime <= edate && x.CompanyID == input.Companyid && x.ServiceOrderStatus != "C");*/
            if (input.IsGroupBy == 1)
            {
                var dispatchCountGroupList = new List<object>();
                var dispatchCountGroup = await (from a in serviceOrderQueryable
                                       join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                       join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                       where b.ProductSerial == c.ProductSerial
                                       group a by new {a.CompanyDispatchPersonnel} into g
                                       select new { CompanyDispatchPersonnel = g.Key , Count = g.Count()})
                                       .ToListAsync();
                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(input.Companyid);
                    if (staffResponse.IsSuccessStatusCode)
                    {
                        var staffList = staffResponse.Content;
                        foreach(var item in dispatchCountGroup)
                        {
                            var staff = staffList.FirstOrDefault(x => x.Id == item.CompanyDispatchPersonnel.CompanyDispatchPersonnel);
                            var child = new
                            {
                                CompanyDispatchPersonnel = item.CompanyDispatchPersonnel.CompanyDispatchPersonnel,
                                Personnel = staff == null ? "" : staff.StaffName,
                                Count = item.Count
                            };
                            dispatchCountGroupList.Add(child);
                        }
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError("获取公司人事信息时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                }
                if (dispatchCountGroupList.Count == 0)
                    return dispatchCountGroup;
                return dispatchCountGroupList;
            }
            else if (input.IsGroupBy == 0)
            {
                dispatchCount = await (from a in serviceOrderQueryable
                                                join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                                join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                                where b.ProductSerial == c.ProductSerial
                                                select a.Id)
                                       .CountAsync();
            }
            return dispatchCount;
        }

        public async Task<AppSrvResult<object>> Count24UncompletedWorkOrder(CountReportDto input)
        {
            if(input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "Count24UncompletedWorkorder", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var today = DateTime.Now;
            var count = await _serviceOrderRepository.CountAsync(x => x.CompanyID == input.Companyid && x.ProcessingEndTime == null && EF.Functions.DateDiffHour(x.CompanyDispatchTime, today) >= 24 && EF.Functions.DateDiffHour(x.CompanyDispatchTime, today) < 48 && x.ServiceOrderStatus != "C");
            return count;
        }

        public async Task<AppSrvResult<object>> Count48UncompletedWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "Count48UncompletedWorkorder", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var today = DateTime.Now;
            var count = await _serviceOrderRepository.CountAsync(x => x.CompanyID == input.Companyid && x.ProcessingEndTime == null && EF.Functions.DateDiffHour(x.CompanyDispatchTime, today) >= 48 && EF.Functions.DateDiffHour(x.CompanyDispatchTime, today) < 72 && x.ServiceOrderStatus != "C");
            return count;
        }

        public async Task<AppSrvResult<object>> Count72UncompletedWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "Count72UncompletedWorkorder", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var today = DateTime.Now;
            var count = await _serviceOrderRepository.CountAsync(x => x.CompanyID == input.Companyid && x.ProcessingEndTime == null && EF.Functions.DateDiffHour(x.CompanyDispatchTime, today) >= 72 && x.ServiceOrderStatus != "C");
            return count;
        }

        public async Task<AppSrvResult<object>> GetFollowCountReport(FollowCountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetFollowCountReport", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            if(input.Timer <= 0 | input.Timer > 3)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetFollowCountReport", "02");
                return Problem(HttpStatusCode.NotFound, "回访次数不正确", instance: errMsg);
            }
            var sdate = DateTime.Today;
            var edate = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var serviceOrderExpress = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => x.ServiceOrderStatus != "C")
                .AndIf(input.Timer == 1, x => x.FollowUpTime1 >= sdate && x.FollowUpTime1 <= edate)
                .AndIf(input.Timer == 2, x => x.FollowUpTime2 >= sdate && x.FollowUpTime2 <= edate)
                .AndIf(input.Timer == 3, x => x.FollowUpTime3 >= sdate && x.FollowUpTime3 <= edate);
            var serviceProductExpress = ExpressionCreator.New<Da_Service_Product>()
                .And(x => x.CompanyID == input.Companyid)
                .AndIf(input.Brand.IsNotNullOrWhiteSpace(), x => x.Branding.Equals(input.Brand))
                .AndIf(input.BrandContent.IsNotNullOrWhiteSpace(), x => x.BrandedContent.Equals(input.BrandContent))
                .AndIf(input.ServiceType.IsNotNullOrWhiteSpace(), x => x.ServiceTypeID.Equals(input.ServiceType));
            var serviceOrderQueryable = _serviceOrderRepository.Where(serviceOrderExpress);
            var serviceProductQueryable = _productRepository.Where(serviceProductExpress);
            var serviceOrderProductQueryable = _orderProductRepository.GetAll();
            var followCount = 0;
            /*var dispatchCount = await _serviceOrderRepository.CountAsync(x => x.CompanyDispatchTime >= sdate && x.CompanyDispatchTime <= edate && x.CompanyID == input.Companyid && x.ServiceOrderStatus != "C");*/
            if (input.IsGroupBy == 1)
            {
                var followCountGroup = await(from a in serviceOrderQueryable
                                               join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                               join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                               where b.ProductSerial == c.ProductSerial
                                               group a by new { a.ServicestaffName } into g
                                               select new { ServicestaffName = g.Key, Count = g.Count() })
                                       .ToListAsync();
                return followCountGroup;
            }
            else if (input.IsGroupBy == 0)
            {
                followCount = await(from a in serviceOrderQueryable
                                      join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                      join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                      where b.ProductSerial == c.ProductSerial
                                      select a.Id)
                                       .CountAsync();
            }
            return followCount;
        }

        public async Task<AppSrvResult<string>> GetNewWorkOrder(long companyid)
        {
            string newWorkOrderCode = await _cacheService.GenerateWorkOrderCodeForTodayAsync(companyid);
            return newWorkOrderCode;
        }

        public async Task<AppSrvResult<List<ServiceOrderProcessDto>>> GetProcessListAsync(long companyid, long orderid)
        {
            if(orderid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetProcessListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "工单ID或公司ID不正确", instance: errMsg);
            }
            var orderProcess = await _processRepository.Where(x => x.ServiceInfoId == orderid).OrderByDescending(x => x.Id).ToListAsync();
            var orderProcessDtos = Mapper.Map<List<ServiceOrderProcessDto>>(orderProcess);
            try
            {
                var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(companyid);
                if (staffResponse.IsSuccessStatusCode)
                {
                    var staffList = staffResponse.Content;
                    if(staffList.Count > 0)
                    {
                        foreach(var item in orderProcessDtos)
                        {
                            if(item.ReplyPersonnel.IsNotNullOrWhiteSpace())
                                item.ReplyPersonnelName = staffList.FirstOrDefault(x => x.Id == (long)item.ReplyPersonnel.ToLong()).StaffName;
                            if(item.OperationPersonnel.IsNotNullOrWhiteSpace())
                                item.OperationPersonnelName = staffList.FirstOrDefault(x => x.Id == (long)item.OperationPersonnel.ToLong()).StaffName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("获取公司人事信息列表出现意外错误:{message}", ex.Message);
            }
            return orderProcessDtos;
        }

        public async Task<AppSrvResult<List<ServiceOrderDto>>> GetRefuseOrderList(long companyid)
        {
            if(companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetRefuseOrderList", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceOrderQueryable = _serviceOrderRepository.Where(x => x.CompanyID == companyid && string.IsNullOrWhiteSpace(x.ServicestaffID));
            var serviceOrderProcessQueryable = _processRepository.Where(x => x.CompanyId == companyid && (x.ProcessType.Equals("工程师拒单") || x.ProcessType.Equals("服务商拒单")));
            var serviceOrderList = await serviceOrderQueryable.Join(serviceOrderProcessQueryable, x => x.Id, y => y.ServiceInfoId, (x,y) => x)
                .Distinct()
                .ToListAsync();
            var serviceOrderDtos = Mapper.Map<List<ServiceOrderDto>>(serviceOrderList);
            return serviceOrderDtos;
        }

        public async Task<AppSrvResult<List<ServiceOrderProductDto>>> GetServiceOrderProductAsync(long companyid, long orderid)
        {
            if(companyid == 0 || orderid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetWorkOrderPagedAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceOrderQueryable = _serviceOrderRepository.Where(x => x.Id == orderid);
            var serviceOrderProductQueryable = _orderProductRepository.Where(x => x.CompanyId == companyid);
            var serviceOrderProducts = await(from a in serviceOrderQueryable 
                                             join b in serviceOrderProductQueryable 
                                             on a.ServiceOrderID equals b.ServiceOrderId
                                             select b)
                                             .ToListAsync();
            var serviceInfoId = serviceOrderProducts[0].ServiceInfoId;
            var productSerials = serviceOrderProducts.Select(x => x.ProductSerial).ToArray();
            var serviceProducts = await _productRepository.Where(x => x.ServiceInfoID == serviceInfoId && productSerials.Contains(x.ProductSerial)).ToListAsync();
            var serviceOrderProductDtos = Mapper.Map<List<ServiceOrderProductDto>>(serviceOrderProducts);
            serviceOrderProductDtos.ForEach(x =>
            {
                var serviceProduct = serviceProducts.FirstOrDefault(y => y.ServiceInfoID == serviceInfoId && y.ProductSerial == x.ProductSerial);
                x.ProductType = serviceProduct == null ? "" : serviceProduct.ProductType;
                x.Branding = serviceProduct == null ? "" : serviceProduct.Branding;
                x.BrandedContent = serviceProduct == null ? "" : serviceProduct.BrandedContent;
                x.Model = serviceProduct == null ? "" : serviceProduct.Model;
                x.ModelNumber = serviceProduct == null ? 0 : serviceProduct.ModelNumber;
                x.ServiceTypeID = serviceProduct == null ? "" : serviceProduct.ServiceTypeID;
                x.PurchaseDate = serviceProduct == null ? null : serviceProduct.PurchaseDate;
                x.PurchasePrice = serviceProduct == null ? 0 : serviceProduct.PurchasePrice;
                x.PurchaseStore = serviceProduct == null ? "" : serviceProduct.PurchaseStore;
                x.GuaranteedIncomeBusiness = serviceProduct == null ? 0 : serviceProduct.GuaranteedIncomeBusiness;
                x.GuaranteedIncomeManufactor = serviceProduct == null ? 0 : serviceProduct.GuaranteedIncomeManufactor;
                x.Barcode1 = serviceProduct == null ? "" : serviceProduct.Barcode1;
                x.Barcode2 = serviceProduct == null ? "" : serviceProduct.Barcode2;
                x.ProductId = serviceProduct == null ? 0 : serviceProduct.Id;
            });
            return serviceOrderProductDtos;
        }

        public async Task<AppSrvResult<object>> GetUnDispatchCountReport(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetUnDispatchCountReport", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }

            var serviceMainExpress = ExpressionCreator.New<Da_Service_Main>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => x.ServiceStatus.Equals("1"));
            var serviceProductExpress = ExpressionCreator.New<Da_Service_Product>()
                .And(x => x.CompanyID == input.Companyid)
                .AndIf(input.Brand.IsNotNullOrWhiteSpace(), x => x.Branding.Equals(input.Brand))
                .AndIf(input.BrandContent.IsNotNullOrWhiteSpace(), x => x.BrandedContent.Equals(input.BrandContent))
                .AndIf(input.ServiceType.IsNotNullOrWhiteSpace(), x => x.ServiceTypeID.Equals(input.ServiceType));
            var serviceMainQueryable = _mainRepository.Where(serviceMainExpress);
            var serviceProductQueryable = _productRepository.Where(serviceProductExpress);
            //var serviceOrderProductQueryable = _orderProductRepository.GetAll();

            var unDispatchCount = await (from a in serviceMainQueryable
                                   join b in serviceProductQueryable on a.Id equals b.ServiceInfoID
                                   group a by a.Id into g
                                   select g.Key)
                                   .CountAsync();
            return unDispatchCount;
        }

        public async Task<AppSrvResult<object>> GetWorkingCountReport(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetWorkingCountReport", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }

            var serviceOrderExpress = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => x.ServiceOrderStatus.Equals("A"))
                .AndIf(input.AppointmentDateS != null, x => x.AppointmentDate >= input.AppointmentDateS)
                .AndIf(input.AppointmentDateE != null, x => x.AppointmentDate <= input.AppointmentDateE);
            var serviceProductExpress = ExpressionCreator.New<Da_Service_Product>()
                .And(x => x.CompanyID == input.Companyid)
                .AndIf(input.Brand.IsNotNullOrWhiteSpace(), x => x.Branding.Equals(input.Brand))
                .AndIf(input.BrandContent.IsNotNullOrWhiteSpace(), x => x.BrandedContent.Equals(input.BrandContent))
                .AndIf(input.ServiceType.IsNotNullOrWhiteSpace(), x => x.ServiceTypeID.Equals(input.ServiceType));
            var serviceOrderQueryable = _serviceOrderRepository.Where(serviceOrderExpress);
            var serviceProductQueryable = _productRepository.Where(serviceProductExpress);
            var serviceOrderProductQueryable = _orderProductRepository.GetAll();
            var workingCount = 0;
            /*var dispatchCount = await _serviceOrderRepository.CountAsync(x => x.CompanyDispatchTime >= sdate && x.CompanyDispatchTime <= edate && x.CompanyID == input.Companyid && x.ServiceOrderStatus != "C");*/
            if (input.IsGroupBy == 1)
            {
                var workingCountGroup = await(from a in serviceOrderQueryable
                                               join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                               join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                               where b.ProductSerial == c.ProductSerial
                                               group a by new { a.ServicestaffName } into g
                                               select new { ServicestaffName = g.Key, Count = g.Count() })
                                       .ToListAsync();
                return workingCountGroup;
            }
            else if (input.IsGroupBy == 0)
            {
                workingCount = await(from a in serviceOrderQueryable
                                      join b in serviceProductQueryable on a.ServiceInfoID equals b.ServiceInfoID
                                      join c in serviceOrderProductQueryable on a.ServiceOrderID equals c.ServiceOrderId
                                      where b.ProductSerial == c.ProductSerial
                                      select a.Id)
                                       .CountAsync();
            }
            return workingCount;
        }


        /// <summary>
        /// 前端传入sql条件有注入风险
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<PageModelDto<ServiceOrderDto>>> GetWorkOrderPagedAsync(ServiceOrderSearchPagedDto search)
        {
            search.TrimStringFields();
            if (search.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetWorkOrderPagedAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                var countQuery = string.Format(
                    @"select count(1) 
              from da_service_main a, da_service_order b 
              where a.id = b.serviceInfoid 
              and a.companyid = {0} {1}",
                    search.CompanyId, search.SqlWhere);

                var total = await connection.ExecuteScalarAsync<int>(countQuery);

                if (total == 0)
                {
                    return new PageModelDto<ServiceOrderDto>(search);
                }
                var orderBy = "b.Id desc";
                if (search.OrderBy.IsNotNullOrWhiteSpace())
                    orderBy = search.OrderBy;
                var dataQuery = string.Format(
                    @"select a.UserName, a.ContactPerson, a.Telephone, a.Province, a.City, a.Street, a.Village, 
                     a.Address, b.*
              from da_service_main a, da_service_order b 
              where a.id = b.serviceInfoid 
              and a.companyid = {0} {1}
              ORDER BY {2}
              LIMIT {3}  OFFSET {4}",
                    search.CompanyId, search.SqlWhere, orderBy, search.PageSize,search.SkipRows());

                var entities = (await connection.QueryAsync<ServiceOrderDto>(dataQuery)).ToList();
                if(search.IsShowEngineerAcceptResult == 1)
                {
                    var orderIds = entities.Select(x => x.ServiceOrderID).ToArray();
                    var serviceInfoIds = entities.Select(x => x.ServiceInfoID).Distinct().ToArray();
                    //查询是否有工人提成存在异议
                    var serviceOrderEngineers = await _engineerRepository.Where(x => serviceInfoIds.Contains(x.ServiceInfoId) && orderIds.Contains(x.ServiceOrderId) && x.EngAcceptResult == "B").ToListAsync();
                    if(serviceOrderEngineers.Count > 0)
                    {
                        entities.ForEach(x =>
                        {
                            var exists = serviceOrderEngineers.Exists(y => y.ServiceInfoId == x.ServiceInfoID && y.ServiceOrderId.Equals(x.ServiceOrderID));
                            if (exists)
                                x.EngineerAcceptResult = 1;
                        });
                    }
                }
                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(search.CompanyId);
                    if (staffResponse.IsSuccessStatusCode)
                    {
                        var staffList = staffResponse.Content;
                        foreach (var entity in entities)
                        {
                            if (entity.CompanyDispatchPersonnel > 0)
                                entity.CompanyDispatchPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.CompanyDispatchPersonnel).StaffName;
                            if (entity.ProviderDispatchPersonnel > 0)
                                entity.ProviderDispatchPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.ProviderDispatchPersonnel).StaffName;
                            if (entity.AppointmentPersonnel > 0)
                                entity.AppointmentPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.AppointmentPersonnel).StaffName;
                            if (entity.QuotationPersonnel > 0)
                                entity.QuotationPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.QuotationPersonnel)
                                    .StaffName;
                            if (entity.SubmitPersonnel > 0)
                                entity.SubmitPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.SubmitPersonnel)
                                    .StaffName;
                            if (entity.CompanyEndPersonnel > 0)
                                entity.CompanyEndPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.CompanyEndPersonnel)
                                    .StaffName;
                            if (entity.SettlementPersonnel > 0)
                                entity.SettlementPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.SettlementPersonnel).StaffName;
                            if (entity.MaterialVerPersonnel > 0)
                                entity.MaterialVerPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.MaterialVerPersonnel).StaffName;
                            if (entity.CommissionPersonnel > 0)
                                entity.CommissionPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.CommissionPersonnel).StaffName;
                            if (entity.CommissionDistributionPersonnel > 0)
                                entity.CommissionDistributionPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.CommissionDistributionPersonnel).StaffName;
                            if (entity.GuaranteedReconciliationPersonnel > 0)
                                entity.GuaranteedReconciliationPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.GuaranteedReconciliationPersonnel).StaffName;
                            if (entity.GuaranteedInvoicingPersonnel > 0)
                                entity.GuaranteedInvoicingPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.GuaranteedInvoicingPersonnel).StaffName;
                            if (entity.FollowUpPersonnel1 > 0)
                                entity.FollowUpPersonnel1Name = staffList.FirstOrDefault(x => x.Id == entity.FollowUpPersonnel1).StaffName;
                            if (entity.FollowUpPersonnel2 > 0)
                                entity.FollowUpPersonnel2Name = staffList.FirstOrDefault(x => x.Id == entity.FollowUpPersonnel2).StaffName;
                            if (entity.FollowUpPersonnel3 > 0)
                                entity.FollowUpPersonnel3Name = staffList.FirstOrDefault(x => x.Id == entity.FollowUpPersonnel3).StaffName;
                            if (entity.SharePersonnel > 0)
                                entity.SharePersonnelName = staffList.FirstOrDefault(x => x.Id == entity.SharePersonnel).StaffName;
                            if (entity.PrintPersonnel > 0)
                                entity.PrintPersonnelName = staffList.FirstOrDefault(x => x.Id == entity.PrintPersonnel).StaffName;
                        }
                    }
                }
                catch(Exception ex)
                {
                    
                }

                return new PageModelDto<ServiceOrderDto>(search, entities, total);
            }
        }

        public async Task<AppSrvResult> ReassignmentWorkOrderAsync(ServiceOrderReassignmentDto input)
        {
            input.TrimStringFields();
            if(input.Id == 0 || input.CompanyID == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReassignmentWorkOrderAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单或公司不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == input.Id && x.IsSettlement == 0, noTracking : false);
            if (serviceOrder == null)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReassignmentWorkOrderAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户工单不存在或已结算", instance: errMsg);
            }

            //预约顺序变更
            if(serviceOrder.AppointmentDate != null)
            {
                //当前工程师预约顺序
                var status = new string[]{"B","C" };
                var serviceOrderList = await _serviceOrderRepository.Where(x => x.CompanyID == input.CompanyID && x.ServicestaffID.StartsWith(serviceOrder.ServicestaffID) && x.AppointmentDate == serviceOrder.AppointmentDate && !status.Contains(x.ServiceOrderStatus), noTracking : false, writeDb: true)
                    .OrderBy(x => x.AppointmentDate)
                    .OrderBy(x => x.Appointmentperiod)
                    .ToListAsync();
                if(serviceOrderList.Count > 0)
                {
                    var i = 1;
                    foreach(var item in  serviceOrderList)
                    {
                        item.SerialNumber = i;
                        i++;
                    }
                    await _serviceOrderRepository.UpdateRangeAsync(serviceOrderList);
                }
                //改派后第一个工程师预约顺序
                var nowEngineer = input.ServicestaffID.Split(',')[0];
                var otherServiceOrderList = await _serviceOrderRepository.Where(x => x.CompanyID == input.CompanyID && x.ServicestaffID.StartsWith(nowEngineer) && x.AppointmentDate == serviceOrder.AppointmentDate && !status.Contains(x.ServiceOrderStatus), noTracking: false, writeDb: true)
                    .OrderBy(x => x.AppointmentDate)
                    .OrderBy(x => x.Appointmentperiod)
                    .ToListAsync();

                if (otherServiceOrderList.Count > 0)
                {
                    var f = 1;
                    foreach (var item in otherServiceOrderList)
                    {
                        item.SerialNumber = f;
                        f++;
                    }
                    await _serviceOrderRepository.UpdateRangeAsync(otherServiceOrderList);
                }
            }
            var allClients = await _cacheService.GetClientsFromCacheAsync();
            var oldEngineer = serviceOrder.ServicestaffID.Split(",");
            try
            {
                var originalEngineerID = serviceOrder.ServicestaffID;
                var originalEngineerName = serviceOrder.ServicestaffName;
                var originalCompanyDispatchTime = serviceOrder.CompanyDispatchTime;
                var originalProviderDispatchTime = serviceOrder.ProviderDispatchTime;
                var originalCompanyDispatchPersonnel = serviceOrder.CompanyDispatchPersonnel;
                var originalProviderDispatchPersonnel = serviceOrder.ProviderDispatchPersonnel;
                serviceOrder.ServicestaffID = input.ServicestaffID;
                serviceOrder.ServicestaffName = input.ServicestaffName;
                serviceOrder.CompanyDispatchTime = DateTime.Now;
                serviceOrder.ProviderDispatchTime = DateTime.Now;

                await _serviceOrderRepository.UpdateAsync(serviceOrder, UpdatingProps<Da_Service_Order>(x => x.ServicestaffID, x => x.ServicestaffName, x => x.CompanyDispatchTime, x => x.ProviderDispatchTime, x => x.CompanyDispatchPersonnel, x => x.ProviderDispatchPersonnel));
                var originalCompanyDispatchPersonnelName = "";
                var staffRtos = new List<Da_StaffListDto>();
                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(serviceOrder.CompanyID);
                    if (staffResponse != null && staffResponse.IsSuccessStatusCode)
                    {
                        staffRtos = staffResponse.Content;
                        if(originalCompanyDispatchPersonnel > 0)
                            originalCompanyDispatchPersonnelName = staffRtos.FirstOrDefault(x => x.Id == input.CompanyDispatchPersonnel).StaffName;
                    }
                }
                catch (Exception ex)
                {
                    originalCompanyDispatchPersonnelName = "";
                }
                var orderProcess = new Da_Service_Order_Process
                {
                    Id = IdGenerater.GetNextId(),
                    ServiceInfoId = serviceOrder.Id,
                    CompanyId = serviceOrder.CompanyID,
                    ServiceOrderId = serviceOrder.ServiceOrderID,
                    ServiceProcessId = serviceOrder.ServiceprocessID,
                    ServiceSpecificationId = input.ServiceSpecificationId,
                    SerialNumber = 2,
                    ProcessType = "改派工程师",
                    OperationPersonnel = input.CompanyDispatchPersonnel.ToString(),
                    OperationTime = DateTime.Now,
                    ProcessReasons = $"改派工程师, 由[{originalEngineerID} => {originalEngineerName}]改派为[{serviceOrder.ServicestaffID} => {serviceOrder.ServicestaffName}], [改派人员]:{originalCompanyDispatchPersonnelName},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}"
                };
                if (originalEngineerID.IsNullOrWhiteSpace())
                    orderProcess.ProcessReasons = $"改派工程师, 由[工程师已拒绝的工单]改派为[{serviceOrder.ServicestaffID} => {serviceOrder.ServicestaffName}], [改派人员]:{originalCompanyDispatchPersonnelName},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                await _processRepository.InsertAsync(orderProcess);
                //清空工程师提成并重新分配比例
                await _engineerRepository.DeleteRangeAsync(x => x.ServiceInfoId == serviceOrder.ServiceInfoID && x.ServiceOrderId.Equals(serviceOrder.ServiceOrderID));
                var engineerIds = input.ServicestaffID.Split(',');
                var engineerNames = input.ServicestaffName.Split(',');
                var proportion = Math.Round(1m / engineerIds.Length, 2);
                for (int i = 0; i < engineerIds.Length; i++)
                {
                    var department = "";
                    if (staffRtos.Count > 0)
                        department = staffRtos.FirstOrDefault(x => x.Id.Equals(engineerIds[i].ToLong())).DepartMentID.ToString();
                    var orderEngineer = new Da_Service_Order_Engineer
                    {
                        Id = IdGenerater.GetNextId(),
                        CompanyId = input.CompanyID,
                        ServiceInfoId = serviceOrder.ServiceInfoID,
                        ServiceOrderId = serviceOrder.ServiceOrderID,
                        EngineerId = (long)engineerIds[i].ToLong(),
                        EngineerName = engineerNames[i],
                        Proportion = proportion,
                        DepartmentId = department
                    };
                    await _engineerRepository.InsertAsync(orderEngineer);
                }
                var clients = allClients.Where(x => engineerIds.Contains(x.UserId) && x.CompanyId == input.CompanyID).ToList();
                if (clients.Count > 0)
                {
                    _ = Task.Run(() =>
                    {
                        foreach (var client in clients)
                        {
                            var obj = new { message = "您有一张新工单待处理，请及时查阅", type = "工单", orderid = serviceOrder.Id, time = DateTime.Now, dispatchOperator = originalCompanyDispatchPersonnelName };
                            _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                        }
                    });
                }

                var clientsOld = allClients.Where(x => oldEngineer.Contains(x.UserId) && x.CompanyId == input.CompanyID).ToList();
                if (clientsOld.Count > 0)
                {
                    _ = Task.Run(() =>
                    {
                        foreach (var client in clientsOld)
                        {
                            var obj = new { message = "您有工单已改派", type="工单", orderid = serviceOrder.Id, time = DateTime.Now, dispatchOperator = originalCompanyDispatchPersonnelName };
                            _notification.Publish(JsonConvert.SerializeObject(obj), client.ClientId);
                        }
                    });
                }
                await _unitOfWork.CommitAsync();
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("改派时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReassignmentWorkOrderAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "改派时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult> ReplyProcessAsync(ServiceOrderProcessUpdationDto input)
        {
            input.TrimStringFields();
            if(input.Id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReplyProcessAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "过程记录不正确", instance: errMsg);
            }
            var orderProcess = Mapper.Map<Da_Service_Order_Process>(input);
            orderProcess.ReplyTime = DateTime.Now;
            try
            {
                await _processRepository.UpdateAsync(orderProcess, UpdatingProps<Da_Service_Order_Process>(x => x.ReplyTime, x => x.ReplyPersonnel, x => x.ReplyResult));
                var serviceOrderProcess = await _processRepository.FetchAsync(x => new { x.ProcessType, x.ServiceInfoId }, x => x.Id == input.Id, writeDb : true);
                if (serviceOrderProcess.ProcessType.Equals("工程师拒单") && input.ReplyResult.Equals("同意"))
                {
                    var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == serviceOrderProcess.ServiceInfoId, noTracking: false, writeDb: true);
                    serviceOrder.CompanyDispatchPersonnel = 0;
                    serviceOrder.CompanyDispatchTime = null;
                    serviceOrder.ServicestaffID = "";
                    serviceOrder.ServicestaffName = "";
                    serviceOrder.EngineerCacelTime = null;
                    serviceOrder.EngineerCacelReason = "";
                    await _serviceOrderRepository.UpdateAsync(serviceOrder);
                }
                else if (serviceOrderProcess.ProcessType.Equals("服务商拒单") && input.ReplyResult.Equals("同意"))
                {
                    var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == serviceOrderProcess.ServiceInfoId, noTracking: false, writeDb: true);
                    serviceOrder.CompanyDispatchPersonnel = 0;
                    serviceOrder.CompanyDispatchTime = null;
                    serviceOrder.ServicestaffID = "";
                    serviceOrder.ServicestaffName = "";
                    serviceOrder.ProviderCacelTime = null;
                    serviceOrder.ProviderCacleReason = "";
                    var serviceMain = await _mainRepository.FetchAsync(x => x, x => x.Id == serviceOrder.ServiceInfoID, noTracking: false, writeDb: true);
                    serviceMain.ProviderID = 0;
                    await _serviceOrderRepository.UpdateAsync(serviceOrder);
                    await _mainRepository.UpdateAsync(serviceMain);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("回复时出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ReplyProcessAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "回复时出现意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult<long[]>> ServiceOrderFollow(ServiceOrderFollowCreationDto input)
        {
            input.TrimStringFields();
            if(input.FollowupIds.Count() == 0 || input.CompanyId == 0 || input.ServiceOrderId.IsNullOrWhiteSpace() || input.FollowResults.Count() == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ServiceOrderFollow", "01");
                return Problem(HttpStatusCode.BadRequest, "回访信息、公司信息、工单号、回访结果不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var isExists = await _followupRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceOrderId.Equals(input.ServiceOrderId) && x.FollowNumber == input.FollowNumber);
            if(isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ServiceOrderFollow", "02");
                return Problem(HttpStatusCode.BadRequest, $"已存在{input.FollowNumber}次回访记录，请勿重复操作", instance: errMsg);
            }
            var serviceOrderFollows = new List<Da_Service_Followup>();
            for(var i = 0; i <  input.FollowupIds.Count(); i++)
            {
                var followup = Mapper.Map<Da_Service_Followup>(input);
                followup.Id = IdGenerater.GetNextId();
                followup.FollowupId = input.FollowupIds.ToArray()[i];
                followup.FollowResult = input.FollowResults.ToArray()[i];
                serviceOrderFollows.Add(followup);
            }
            //var serviceOrderFollow = Mapper.Map<Da_Service_Followup>(input);
            //serviceOrderFollow.Id = IdGenerater.GetNextId();
            try
            {
                var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.CompanyID == input.CompanyId && x.ServiceOrderID.Equals(input.ServiceOrderId), noTracking : false);
                if (input.FollowNumber == 1)
                {
                    serviceOrder.FollowUpPersonnel1 = input.Creater;
                    serviceOrder.FollowUpResult1 = input.FinalResult;
                    serviceOrder.FollowUpTime1 = DateTime.Now;
                }
                else if (input.FollowNumber == 2)
                {
                    serviceOrder.FollowUpPersonnel2 = input.Creater;
                    serviceOrder.FollowUpResult2 = input.FinalResult;
                    serviceOrder.FollowUpTime2 = DateTime.Now;
                }
                else if (input.FollowNumber == 3)
                {
                    serviceOrder.FollowUpPersonnel3 = input.Creater;
                    serviceOrder.FollowUpResult3 = input.FinalResult;
                    serviceOrder.FollowUpTime3 = DateTime.Now;
                }
                else
                {
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ServiceOrderFollow", "03");
                    return Problem(HttpStatusCode.BadRequest, $"回访次数错误,仅允许进行1-3次回访", instance: errMsg);
                }
                await _followupRepository.InsertRangeAsync(serviceOrderFollows);
                await _serviceOrderRepository.UpdateAsync(serviceOrder);
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ServiceOrderFollow", "04");
                _logger.LogError("工单回访时发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "工单回访时发生意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
            return serviceOrderFollows.Select(x => x.Id).ToArray();

        }

        public async Task<AppSrvResult> UpdateOrderAsync(ServiceOrderUpdationDto input)
        {
            input.TrimStringFields();
            if(input.Id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateOrderAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "工单不正确", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var isExists = await _serviceOrderRepository.AnyAsync(x => x.Id == input.Id);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateOrderAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "工单不存在或已删除", instance: errMsg);
            }
            var serviceOrder = await _serviceOrderRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking: false);
            ConditionalMap(input, serviceOrder);
            try
            {
                await _serviceOrderRepository.UpdateAsync(serviceOrder);


                var staffid = serviceOrder.ServicestaffID.Split(',');
                //不调整已完工和作废工单 仅更新预约日期为工单当天的
                var resetGroup = new string[] { "B", "C" };
                if (input.SerialNumber > 0)
                {
                    var exists = await _serviceOrderRepository.AnyAsync(x => x.SerialNumber > serviceOrder.SerialNumber && x.ServicestaffID.StartsWith(staffid[0]) && !resetGroup.Contains(x.ServiceOrderStatus) && x.AppointmentDate == serviceOrder.AppointmentDate);
                    if (exists)
                        await _serviceOrderRepository.UpdateRangeAsync(x => x.SerialNumber > serviceOrder.SerialNumber && x.ServicestaffID.StartsWith(staffid[0]) && !resetGroup.Contains(x.ServiceOrderStatus) && x.AppointmentDate == serviceOrder.AppointmentDate, x => new Da_Service_Order { SerialNumber = x.SerialNumber + 1 });
                }
                if (input.AppointmentDate != null)
                {
                    var serviceOrderList = await _serviceOrderRepository.Where(x => x.ServicestaffID.StartsWith(staffid[0]) && !resetGroup.Contains(x.ServiceOrderStatus) && x.AppointmentDate == serviceOrder.AppointmentDate, noTracking: false)
                        .OrderBy(x => x.AppointmentDate)
                        .OrderBy(x => x.Appointmentperiod)
                        .ToListAsync();
                    if (serviceOrderList.Count > 0)
                    {
                        var i = 1;
                        foreach(var item in  serviceOrderList)
                        {
                            item.SerialNumber = i;
                            i++;
                        }
                        await _serviceOrderRepository.UpdateRangeAsync(serviceOrderList);
                    }

                }
                await _unitOfWork.CommitAsync();
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateOrderAsync", "03");
                _logger.LogError("更新服务客户工单信息发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "更新服务客户工单信息发生意外错误", instance: errMsg, title: ex.InnerException.Message);
            }
        }

        public async Task<AppSrvResult<List<ServiceOrderFollowDto>>> GetServiceOrderFollowAsync(long companyid, string serviceOrderId)
        {
            if(companyid == 0 || serviceOrderId.IsNullOrEmpty())
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceOrderFollowAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司或工单号不正确", instance: errMsg);
            }
            var serviceFollowList = await _followupRepository.Where(x => x.CompanyId == companyid && x.ServiceOrderId.Equals(serviceOrderId)).ToListAsync();
            var serviceFollowDtos = Mapper.Map<List<ServiceOrderFollowDto>>(serviceFollowList);
            return serviceFollowDtos;
        }

        public async Task<AppSrvResult<object>> CountPendingWorkOrder(CountReportDto input)
        {
            if(input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountPendingWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var serviceOrderStatus = new string[] { "B", "C" };
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !serviceOrderStatus.Contains(x.ServiceOrderStatus) && x.ReadTime == null)
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountPendingPartWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountPendingPartWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var serviceOrderStatus = new string[] { "B", "C" };
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !serviceOrderStatus.Contains(x.ServiceOrderStatus) && x.LegacyType.Contains("A"))
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountLegacyWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountLegacyWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var serviceOrderStatus = new string[] { "B", "C" };
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !serviceOrderStatus.Contains(x.ServiceOrderStatus) && !string.IsNullOrWhiteSpace(x.LegacyType))
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountRefuseWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountRefuseWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var serviceOrderStatus = new string[] { "B", "C" };
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !serviceOrderStatus.Contains(x.ServiceOrderStatus) && x.EngineerCacelTime != null)
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountReadyToFollowWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountReadyToFollowWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !x.ServiceOrderStatus.Equals("C") && x.FollowUpTime1 == null)
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountPendingVerificationWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountPendingVerificationWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !x.ServiceOrderStatus.Equals("C") && x.FollowUpTime1 == null)
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<object>> CountPendingSettlementWorkOrder(CountReportDto input)
        {
            if (input.Companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CountPendingSettlementWorkOrder", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var expression = ExpressionCreator.New<Da_Service_Order>()
                .And(x => x.CompanyID == input.Companyid)
                .And(x => !x.ServiceOrderStatus.Equals("C") && x.SettlementTime == null)
                .AndIf(input.ServiceStaffId.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.ServicestaffID, $"%{input.ServiceStaffId}%"));
            var count = await _serviceOrderRepository.CountAsync(expression);
            return count;
        }

        public async Task<AppSrvResult<int>> ExpectedEngineerAppointmentNumber(CountEngineerAppointmentDto input)
        {
            if (input.CompanyId == 0 || input.StaffId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "ExpectedEngineerAppointmentNumber", "01");
                return Problem(HttpStatusCode.BadRequest, "公司或工程师不正确", instance: errMsg);
            }
            var query = _serviceOrderRepository.Where(x => x.CompanyID == input.CompanyId && x.ServicestaffID.StartsWith(input.StaffId.ToString())
             && x.AppointmentDate == input.AppointmentDate && string.Compare(x.Appointmentperiod, input.Appointmentperiod) <= 0);
            var num = 0;
            if (!(await query.AnyAsync()))
                num++;
            else
            {
                num = await query.MaxAsync(x => x.SerialNumber);
                num++;
            }
            return num;
        }
    }
}
