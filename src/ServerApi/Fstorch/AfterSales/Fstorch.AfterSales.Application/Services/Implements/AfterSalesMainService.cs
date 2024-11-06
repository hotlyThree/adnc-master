

using Microsoft.AspNetCore.Http;

namespace Fstorch.AfterSales.Application.Services.Implements
{
    public class AfterSalesMainService : AbstractAppService, IAfterSalesMainService
    {
        private readonly ISystemRestClient _restClient;
        private readonly IEfRepository<Da_Service_Main> _serviceMainRepository;
        private readonly IEfRepository<Da_Service_Product> _serviceProductRepository;
        private readonly IEfRepository<Da_Service_Order> _orderRepository;
        private readonly ILogger<AfterSalesMainService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEfRepository<Da_Service_Order_Produc> _orderProductRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DbContext _dbContext;
        private readonly IStaffRestClient _staffRestClient;

        public AfterSalesMainService(IEfRepository<Da_Service_Product> serviceProductRepository, IEfRepository<Da_Service_Main> serviceMainRepository, ISystemRestClient restClient, ILogger<AfterSalesMainService> logger, IUnitOfWork unitOfWork, IEfRepository<Da_Service_Order> orderRepository, IEfRepository<Da_Service_Order_Produc> orderProductRepository, IHttpClientFactory httpClientFactory, DbContext dbContext, IStaffRestClient staffRestClient)
        {
            _serviceProductRepository = serviceProductRepository;
            _serviceMainRepository = serviceMainRepository;
            _restClient = restClient;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
            _staffRestClient = staffRestClient;
        }

        public async Task<AppSrvResult<List<ServiceMainDto>>> AdjacentCustomer(ServiceMainAdjacentSearchDto input)
        {
            if(input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "AdjacentCustomer", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceMainList = await _serviceMainRepository.Where(x => x.CompanyID == input.CompanyId && x.Lat > 0 && x.Lng > 0).ToListAsync();
            var point1 = new Point(new Coordinate(input.Lng, input.Lat));
            var Distance = point1.Distance(new Point(new Coordinate(serviceMainList[0].Lng, serviceMainList[0].Lat)));
            var result = serviceMainList.FindAll(x => point1.Distance(new Point(x.Lat, x.Lng)) <= input.Distance);
            var serviceMainDtos = Mapper.Map<List<ServiceMainDto>>(result);
            return serviceMainDtos;
            //serviceMainList.Where(x => serviceMainList.g)
        }

        public async Task<AppSrvResult> ChangeStatus(long id, string status)
        {
            if (string.IsNullOrWhiteSpace(status) || id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatus", "01");
                return Problem(HttpStatusCode.BadRequest, "服务状态或服务档案不正确", instance: errMsg);
            }
            var isExists = await _serviceMainRepository.AnyAsync(x => x.Id == id, writeDb : true);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatus", "02");
                return Problem(HttpStatusCode.BadRequest, "服务档案不存在或已删除", instance :  errMsg);
            }
            try
            {
                await _serviceMainRepository.UpdateAsync(new Da_Service_Main { Id = id, ServiceStatus = status }, UpdatingProps<Da_Service_Main>(x => x.ServiceStatus));
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("更新服务客户档案出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatus", "03");
                return Problem(HttpStatusCode.BadRequest, "更新服务客户档案出现意外错误", instance: errMsg, title : ex.Message);
            }
        }

        public async Task<AppSrvResult> ChangeStatusBatch(IEnumerable<long> ids, string status)
        {
            if(ids.Count() == 0 || ids.Any(x => x == 0) || string.IsNullOrWhiteSpace(status))
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatusBatch", "01");
                return Problem(HttpStatusCode.BadRequest, "服务状态或服务档案不正确", instance: errMsg);
            }
            var keys = ids.ToArray();
            var isExists = await _serviceMainRepository.AnyAsync(x => keys.Contains(x.Id), writeDb: true);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatusBatch", "02");
                return Problem(HttpStatusCode.BadRequest, "部分服务档案不存在或已删除", instance: errMsg);
            }
            try
            {
                var serviceMainList = await _serviceMainRepository.Where(x => keys.Contains(x.Id), noTracking: false, writeDb: true).ToListAsync();
                serviceMainList.ForEach(x => x.ServiceStatus = status);
                await _serviceMainRepository.UpdateRangeAsync(serviceMainList);
                return AppSrvResult();
            }
            catch(Exception ex)
            {
                _logger.LogError("更新服务客户档案出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "ChangeStatusBatch", "03");
                return Problem(HttpStatusCode.BadRequest, "更新服务客户档案出现意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long[]>> CopyServiceMainBatch(long mainid, int num, long ordertaker)
        {
            if(mainid == 0 || ordertaker == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CopyServiceMainBatch", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案或接单人不正确", instance: errMsg);
            }
            if(num <= 0 || num > 10)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CopyServiceMainBatch", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案复制数量不符合，可复制数量>0且<=10", instance: errMsg);
            }
            _unitOfWork.BeginTransaction();
            var isExists = await _serviceMainRepository.AnyAsync(x => x.Id ==  mainid);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CopyServiceMainBatch", "03");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案复制数量不符合，可复制数量>0且<=10", instance: errMsg);
            }
            var serviceMains = new List<Da_Service_Main>();
            var serviceProducts = new List<Da_Service_Product>();
            var serviceMain = await _serviceMainRepository.FetchAsync(x => x, x => x.Id == mainid);
            var serviceProductList = await _serviceProductRepository.Where(x => x.CompanyID == serviceMain.CompanyID && x.ServiceInfoID == mainid).ToListAsync();
            for (var i = 0; i < num; i++)
            {
                var item = new Da_Service_Main();
                ConditionalMap(serviceMain, item);
                item.Id = IdGenerater.GetNextId();
                item.IsFinish = 0;
                item.AcceptanceDate = DateTime.Now;
                item.AppointmentDate = null;
                item.AppointmentPeriod = "";
                item.ServiceStatus = "1";
                item.FactoryNO = "";
                item.GuaranteedIncomeBusiness = 0;
                item.GuaranteedIncomeManufactor = 0;
                item.GuarantMaterialcost = 0;
                item.OffbailIncome = 0;
                item.ServiceProfit = 0;
                item.ServiceCommission = 0;
                item.OrderTaker = ordertaker.ToString();
                item.OffbailMaterialcost = 0;
                item.Disbursement = 0;
                serviceMains.Add(item);
                serviceProductList.ForEach(y =>
                {
                    var product = new Da_Service_Product();
                    ConditionalMap(y, product);
                    product.Id = IdGenerater.GetNextId();
                    product.ServiceInfoID = item.Id;
                    serviceProducts.Add(product);
                });
            }
            try
            {
                await _serviceMainRepository.InsertRangeAsync(serviceMains);
                await _serviceProductRepository.InsertRangeAsync(serviceProducts);
                await _unitOfWork.CommitAsync();
                return serviceMains.Select(x => x.Id).ToArray();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("复制服务客户档案出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CopyServiceMainBatch", "04");
                return Problem(HttpStatusCode.BadRequest, "复制服务客户档案出现意外错误", instance: errMsg, title: ex.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long>> CreateAsync(ServiceMainCreationDto input)
        {
            input.TrimStringFields();
            if(input.CompanyID == 0 || input.OrderTaker.IsNullOrWhiteSpace() || input.Products.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司信息、接单人、服务客户产品明细不正确");
            }
            _unitOfWork.BeginTransaction();
            if (input.FactoryNO.IsNotNullOrWhiteSpace())
            {
                var isExists = await _serviceMainRepository.AnyAsync(x => x.CompanyID == input.CompanyID && x.FactoryNO.Equals(input.FactoryNO));
                if (isExists)
                    return Problem(HttpStatusCode.BadRequest, "厂家单号重复");
            }
            var serviceMain = Mapper.Map<Da_Service_Main>(input);
            serviceMain.Id = IdGenerater.GetNextId();
            serviceMain.AcceptanceDate = DateTime.Now;
            serviceMain.ServiceStatus = "1";
            var serviceProductList = Mapper.Map<List<Da_Service_Product>>(input.Products);
            serviceProductList.ForEach(x =>
            {
                x.Id = IdGenerater.GetNextId();
                x.ServiceInfoID = serviceMain.Id;
                x.CompanyID = serviceMain.CompanyID;
            });
            try
            {
                if((serviceMain.Lat == 0 || serviceMain.Lng == 0) && serviceMain.Address.IsNotNullOrWhiteSpace())
                {
                    //string key = "oMOtvWiDCWz5bpNC870hNgsQ";
                    //https://fssms.fstorch.com/gps.asmx/Get_GPS_LocationByAddress_New?Address=%E7%91%9E%E6%B3%B0%E9%94%A6%E5%9F%8E&City=%E5%9B%9B%E5%B7%9D&Key=
                    var requestUrl = "https://fssms.fstorch.com/gps.asmx/Get_GPS_LocationByAddress_New?City=" + serviceMain.Province+ serviceMain.City+ serviceMain.Street + serviceMain.Village + "&Address=" + serviceMain.Address + "&Key=";

                    var client = _httpClientFactory.CreateClient();
                    var response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    responseString = XElement.Parse(responseString).Value;
                    /*var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
                    var locationObject = (dynamic)responseObject["result"];*/
                    if(!string.IsNullOrWhiteSpace(responseString))
                    {
                        string[] parts = responseString.Split('|');
                        var coordinates = parts[0].Split(',');
                        if (coordinates[0].IsNotNullOrWhiteSpace() && coordinates[1].IsNotNullOrWhiteSpace())
                        {
                            var lat = double.Parse(coordinates[0]);
                            var lng = double.Parse(coordinates[1]);
                            serviceMain.Lat = lat;
                            serviceMain.Lng = lng;
                        }
                    }
                }
                await _serviceMainRepository.InsertAsync(serviceMain);
                await _serviceProductRepository.InsertRangeAsync(serviceProductList);
                await _unitOfWork.CommitAsync();
                return serviceMain.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError("创建服务客户档案出现意外错误:{message},innerErr:{inner}", ex.Message,ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "创建服务客户档案出现意外错误", instance: errMsg, title : ex.Message +",内部详细错误:"+ ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long[]>> CreateBatchAsync(ServiceMainCreationBatchDto input)
        {
            input.TrimStringFields();
            
            if (input.ServiceMains.Any(x => x.CompanyID == 0) || input.ServiceMains.Any(x => x.OrderTaker.IsNullOrWhiteSpace()) || input.ServiceMains.Any(x => x.Products.Count == 0))
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateBatchAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司信息、接单人、服务客户产品明细不正确");
            }
            _unitOfWork.BeginTransaction();
            if (input.ServiceMains.Any(x => x.FactoryNO.IsNotNullOrWhiteSpace()))
            {
                var factoryNos = input.ServiceMains.Select(x => x.FactoryNO).ToArray();
                var isExists = await _serviceMainRepository.AnyAsync(x => x.CompanyID == input.ServiceMains[0].CompanyID && factoryNos.Contains(x.FactoryNO));
                if (isExists)
                    return Problem(HttpStatusCode.BadRequest, "厂家单号重复");
            }
            var serviceMains = new List<Da_Service_Main>();
            var serviceProductList = new List<Da_Service_Product>();
            input.ServiceMains.ForEach(x =>
            {
                serviceMains = Mapper.Map<List<Da_Service_Main>>(x);
                serviceMains.ForEach(y =>
                {
                    y.Id = IdGenerater.GetNextId();
                    y.AcceptanceDate = DateTime.Now;
                    serviceProductList = Mapper.Map<List<Da_Service_Product>>(x.Products);
                    serviceProductList.ForEach(z =>
                    {
                        z.Id = IdGenerater.GetNextId();
                        z.ServiceInfoID = y.Id;
                        x.CompanyID = y.CompanyID;
                    });
                });
            });
            try
            {
                await _serviceMainRepository.InsertRangeAsync(serviceMains);
                await _serviceProductRepository.InsertRangeAsync(serviceProductList);
                await _unitOfWork.CommitAsync();
                return serviceMains.Select(x => x.Id).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("创建服务客户档案出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateAsync", "03");
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, "创建服务客户档案出现意外错误", instance: errMsg, title: ex.Message + ",内部详细错误:" + ex.InnerException.Message);
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult<long>> CreateServiceProductAsync(ServiceProductCreationDto input)
        {
            input.TrimStringFields();
            if(input.ServiceInfoID == 0 || input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateServiceProductAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案或公司不正确", instance: errMsg);
            }
            var isExists = await _serviceMainRepository.AnyAsync(x => x.Id == input.ServiceInfoID);
            if(!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateServiceProductAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案不存在或已删除", instance: errMsg);
            }
            var serviceProduct = Mapper.Map<Da_Service_Product>(input);
            serviceProduct.Id = IdGenerater.GetNextId();
            try
            {
                await _serviceProductRepository.InsertAsync(serviceProduct);
                return serviceProduct.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("创建服务客户产品出现意外错误:{message},innerErr:{inner}", ex.Message, ex.InnerException.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "CreateServiceProductAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "创建服务客户产品出现意外错误", instance: errMsg, title: ex.Message + ",内部详细错误:" + ex.InnerException.Message);
            }
        }

        public async Task<AppSrvResult> DeleteServiceProductAsync(long id)
        {
            if(id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "DeleteServiceProductAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户产品不正确", instance: errMsg);
            }
            var serviceInfoId = await _serviceProductRepository.FetchAsync(x => x.ServiceInfoID, x => x.Id ==  id);
            var count = await _serviceProductRepository.CountAsync(x => x.ServiceInfoID == serviceInfoId);
            if(count <= 1)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "DeleteServiceProductAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "最少需要保留一条明细", instance: errMsg);
            }
            await _serviceProductRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<PageModelDto<ServiceMainDto>>> GetPagedCustomAsync(ServiceMainSearchPagedCustomDto search)
        {
            search.TrimStringFields();
            if (search.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "GetWorkOrderPagedAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                var countQuery = string.Format(
                    @"select count(DISTINCT a.id) 
              from da_service_main a, da_service_product b 
              where a.id = b.serviceInfoid
              and a.companyid = {0} {1}",
                    search.CompanyId, search.SqlWhere);

                var total = await connection.ExecuteScalarAsync<int>(countQuery);

                if (total == 0)
                {
                    return new PageModelDto<ServiceMainDto>(search);
                }
                var orderBy = "a.Id desc";
                if (search.OrderBy.IsNotNullOrWhiteSpace())
                    orderBy = search.OrderBy;
                var dataQuery = string.Format(
                    @"select DISTINCT a.*
              from da_service_main a, da_service_product b 
              where a.id = b.serviceInfoid 
              and a.companyid = {0} {1}
              ORDER BY {2}
              LIMIT {3}  OFFSET {4}",
                    search.CompanyId, search.SqlWhere, orderBy, search.PageSize, search.SkipRows());

                var entities = (await connection.QueryAsync<ServiceMainDto>(dataQuery)).ToList();


                try
                {
                    var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(search.CompanyId);
                    if (staffResponse.IsSuccessStatusCode)
                    {
                        var staffList = staffResponse.Content;
                        foreach (var item in entities)
                        {
                            if (item.OrderTaker.IsNotNullOrEmpty())
                                item.OrderTakerName = staffList.FirstOrDefault(x => x.Id == (long)item.OrderTaker.ToLong()).StaffName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("调用Adnc.Fstorch.Manager.Api服务获取接单人员出错");
                }
                return new PageModelDto<ServiceMainDto>(search, entities, total);
            }
         }

        public async Task<PageModelDto<ServiceMainDto>> GetPagedAsync(ServiceMainSearchPagedDto search)
        {
            search.TrimStringFields();
            if(search.CompanyID == 0)
                return new PageModelDto<ServiceMainDto>(search);
            var userTypes = search.UserType.ToArray();
            var businessTypes = search.BusinessType.ToArray();
            var serviceStatus = search.ServiceStatus.ToArray();
            var serviceProcess = search.ServiceProcess.ToArray();
            var productTypes = search.ProductType.ToArray(); 
            var brandings = search.Branding.ToArray();
            var brandedContents = search.BrandedContent.ToArray();
            var serviceTypes = search.ServiceTypeID.ToArray();
            var purchaseStores = search.PurchaseStore.ToArray();
            var expressionServiceMain = ExpressionCreator
                .New<Da_Service_Main>()
                .And(x => x.CompanyID == search.CompanyID)
                .AndIf(search.BranchId != null && search.BranchId > 0, x => x.BranchID == search.BranchId)
                .AndIf(search.ProviderId != null && search.ProviderId > 0, x =>  x.ProviderID == search.ProviderId)
                .AndIf(search.DocumentNumber.IsNotNullOrWhiteSpace(), x => x.DocumentNumber.Contains(search.DocumentNumber))
                .AndIf(search.ReceivingProviderID.IsNotNullOrWhiteSpace(), x => x.ReceivingProviderID.Equals(search.ReceivingProviderID))
                .AndIf(search.MemberCode.IsNotNullOrWhiteSpace(), x => x.MemberCode.Equals(search.MemberCode))
                .AndIf(search.UserName.IsNotNullOrWhiteSpace(), x => x.UserName.Contains(search.UserName))
                .AndIf(search.ContactPerson.IsNotNullOrWhiteSpace(), x => x.ContactPerson.Contains(search.ContactPerson))
                .AndIf(search.Telephone.IsNotNullOrWhiteSpace(), x => x.Telephone.Contains(search.Telephone))
                .AndIf(search.RegionCode.IsNotNullOrWhiteSpace(), x => x.RegionCode.Equals(search.RegionCode))
                .AndIf(search.Province.IsNotNullOrWhiteSpace(), x => x.Province.Equals(search.Province))
                .AndIf(search.City.IsNotNullOrWhiteSpace(), x => x.City.Equals(search.City))
                .AndIf(search.Street.IsNotNullOrWhiteSpace(), x => x.Street.Equals(search.Street))
                .AndIf(search.Village.IsNotNullOrWhiteSpace(), x => x.Village.Equals(search.Village))
                .AndIf(search.Address.IsNotNullOrWhiteSpace(), x => x.Address.Contains(search.Address))
                .AndIf(userTypes.Length > 0, x => userTypes.Contains(x.UserType))
                .AndIf(businessTypes.Length > 0, x => businessTypes.Contains(x.BusinessType))
                .AndIf(search.ServiceRequest.IsNotNullOrWhiteSpace(), x => x.ServiceRequest.Contains(search.ServiceRequest))
                .AndIf(search.AcceptanceDateS != null, x => x.AcceptanceDate >= search.AcceptanceDateS)
                .AndIf(search.AcceptanceDateE != null, x => x.AcceptanceDate <= search.AcceptanceDateE)
                .AndIf(search.OrderTaker.IsNotNullOrWhiteSpace(), x => x.OrderTaker.Equals(search.OrderTaker))
                .AndIf(search.AppointmentDateS != null, x => x.AppointmentDate >= search.AppointmentDateS)
                .AndIf(search.AppointmentDateE != null, x => x.AppointmentDate <= search.AppointmentDateE)
                .AndIf(search.AppointmentPeriod.IsNotNullOrWhiteSpace(), x => x.AppointmentPeriod.Equals(search.AppointmentPeriod))
                .AndIf(search.ContractNo.IsNotNullOrWhiteSpace(), x => x.ContractNo.Contains(search.ContractNo))
                .AndIf(search.SalesNO.IsNotNullOrWhiteSpace(), x => x.SalesNO.Contains(search.SalesNO))
                .AndIf(search.FactoryNO.IsNotNullOrWhiteSpace(), x => x.FactoryNO.Contains(search.FactoryNO))
                .AndIf(serviceStatus.Length > 0, x => serviceStatus.Contains(x.ServiceStatus))
                .AndIf(search.Servicenotes.IsNotNullOrWhiteSpace(), x => x.Servicenotes.Contains(search.Servicenotes))
                .AndIf(search.IsFinish > 0, x => x.IsFinish == search.IsFinish);
            var serviceMain = _serviceMainRepository.Where(expressionServiceMain);
            var expressionServiceProduct = ExpressionCreator
                .New<Da_Service_Product>()
                .And(x => x.CompanyID == search.CompanyID)
                .AndIf(serviceProcess.Length > 0, x => serviceProcess.Contains(x.ServiceProcess))
                .AndIf(productTypes.Length > 0, x => productTypes.Contains(x.ProductType))
                .AndIf(brandings.Length > 0, x => brandings.Contains(x.Branding))
                .AndIf(brandedContents.Length > 0, x => brandedContents.Contains(x.BrandedContent))
                .AndIf(search.Model.IsNotNullOrWhiteSpace(), x => x.Model.Contains(search.Model))
                .AndIf(serviceTypes.Length > 0, x => serviceTypes.Contains(x.ServiceTypeID))
                .AndIf(purchaseStores.Length > 0, x => purchaseStores.Contains(x.PurchaseStore))
                .AndIf(search.PurchaseDateS != null, x => x.PurchaseDate >= search.PurchaseDateS)
                .AndIf(search.PurchaseDateE != null, x => x.PurchaseDate <= search.PurchaseDateE)
                .AndIf(search.Barcode1.IsNotNullOrWhiteSpace(), x => x.Barcode1.Contains(search.Barcode1))
                .AndIf(search.Barcode2.IsNotNullOrWhiteSpace(), x => x.Barcode2.Contains(search.Barcode2));
            var serviceProductIds = await _serviceProductRepository.Where(expressionServiceProduct).Select(x => x.ServiceInfoID).ToArrayAsync();
            //var total = await serviceMain.LeftJoin(serviceProduct, x => x.Id, y => y.ServiceInfoID, (x, y) => new Da_Service_Main { Id = x.Id}).CountAsync();
            if (serviceProductIds.Length == 0)
                return new PageModelDto<ServiceMainDto>(search);
            expressionServiceMain = expressionServiceMain.And(x => serviceProductIds.Contains(x.Id));
            var total = await _serviceMainRepository.CountAsync(expressionServiceMain);
            if(total == 0)
                return new PageModelDto<ServiceMainDto>(search);
            var entities = await _serviceMainRepository.Where(expressionServiceMain)
            .OrderByDescending(x => x.Id)
            .Skip(search.SkipRows())
            .Take(search.PageSize)
            .ToListAsync();
            var data = Mapper.Map<List<ServiceMainDto>>(entities);
            try
            {
                var staffResponse = await _staffRestClient.GetCodeBmStaffListAsync(search.CompanyID);
                if (staffResponse.IsSuccessStatusCode)
                {
                    var staffList = staffResponse.Content;
                    foreach(var item in data)
                    {
                        if (item.OrderTaker.IsNotNullOrEmpty())
                            item.OrderTakerName = staffList.FirstOrDefault(x => x.Id == (long)item.OrderTaker.ToLong()).StaffName;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("调用Adnc.Fstorch.Manager.Api服务获取接单人员出错");
            }

            return new PageModelDto<ServiceMainDto>(search, data, total);
        }

        public async Task<AppSrvResult<List<ServiceProductDto>>> GetServiceProductAsync(long companyid, long id)
        {
            if(id == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "GetServiceProductAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司信息或服务客户档案不正确");
            }
            var serviceProductList = await _serviceProductRepository
                .Where(x => x.CompanyID == companyid && x.ServiceInfoID == id)
                .OrderBy(x => x.ProductSerial)
                .ToListAsync();
            var serviceProcessIds = Array.Empty<string>();
            var serviceProcessIdList = new List<string>();
            try
            {
                //var processResponse = await _restClient.GetCodeBmServiceProcessAsync(companyid);
                var processRelationResponse = await _restClient.GetCodeBmRelationComparisonAsync(companyid);
                if (processRelationResponse != null && processRelationResponse.IsSuccessStatusCode)
                {
                    var processRelationList = processRelationResponse.Content;
                    var products = serviceProductList.GroupBy(x => new { x.Branding, x.BrandedContent }).Select(x => new { x.Key.Branding, x.Key.BrandedContent }).ToList();
                    foreach (var item in products)
                    {
                        //
                        var processList = processRelationList.Where(x => 
                        (x.ServiceBrandID.Equals(item.Branding)  && x.BrandContentName.Equals(item.BrandedContent))
                        || (x.ServiceBrandID.Equals("") && x.BrandContentName.Equals(item.BrandedContent))
                        || (x.ServiceBrandID.Equals(item.Branding) && x.BrandContentName.Equals(""))
                        || (x.ServiceBrandID.Equals("") && x.BrandContentName.Equals("")))
                            .ToList();
                        if (processList != null && processList.Count > 0)
                        {
                            if (processList.Any(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNotNullOrEmpty()))
                            {
                                //品牌+品牌内容全匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNotNullOrEmpty()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else if(processList.Any(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNotNullOrEmpty()))
                            {
                                //品牌内容单匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNotNullOrEmpty()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else if(processList.Any(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNullOrWhiteSpace()))
                            {
                                //品牌单匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNullOrWhiteSpace()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else
                            {
                                //都为空 通用
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNullOrWhiteSpace()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                        }
                    }
                    serviceProcessIds = serviceProcessIdList.ToArray();
                }
            }
            catch (Exception ex)
            {
                serviceProcessIds = Array.Empty<string>();
            }
            var productSerials = serviceProductList.Select(x => x.ProductSerial).ToArray();
            /*if (serviceProcessIds.Length == 0)
                serviceProcessIds = new[] { "01", "02", "03", "04", "05","11" };*/
            //var serviceProcessIds = serviceProductList.Select(x => x.ServiceProcess).Distinct().ToArray();
            //var serviceOrderResp = _orderRepository.Where(x => x.ServiceInfoID == id && serviceProcess.Contains(x.ServiceprocessID));
            var serviceOrderResp = _orderRepository.Where(x => !x.ServiceOrderStatus.Equals("C") && x.ServiceInfoID == id && x.CompanyID == companyid && serviceProcessIds.Contains(x.ServiceprocessID));
            /*var serviceOrderProdResp = _orderProductRepository.Where(x => x.CompanyId == companyid && productSerials.Contains(x.ProductSerial) && serviceProcess.Contains(x.ServiceProcessId) && x.ServiceInfoId == id);*/
            var serviceOrderProdResp = _orderProductRepository.Where(x => productSerials.Contains(x.ProductSerial));
            var statusList = await (from s in serviceOrderResp
                                  join p in serviceOrderProdResp
                                  on s.ServiceOrderID equals p.ServiceOrderId
                                  select new { s.Id, s.ServiceInfoID,s.ServiceOrderStatus, s.ProcessingStartTime, s.ProcessingEndTime, s.ServicestaffName, p.ProductSerial, s.ServiceprocessID}
                                 ).ToListAsync();
            var serviceProductDtos = Mapper.Map<List<ServiceProductDto>>(serviceProductList);
            if (statusList.Count > 0)
            {
                var ServiceProductDtoList = new List<ServiceProductDto>();
                serviceProductDtos.ForEach(x =>
                {
                    var statusOrder = statusList.FirstOrDefault(y => y.ServiceInfoID == x.ServiceInfoID && y.ProductSerial == x.ProductSerial && y.ServiceprocessID.Equals(x.ServiceProcess));

                    var otherStatusOrders = statusList.Where(y => y.ServiceInfoID == x.ServiceInfoID && y.ProductSerial == x.ProductSerial && !y.ServiceprocessID.Equals(x.ServiceProcess)).ToList();
                    x.ServiceOrderStatus = statusOrder == null ? "" : statusOrder.ServiceOrderStatus;
                    x.ProcessingStartTime = statusOrder == null ? null : statusOrder.ProcessingStartTime;
                    x.ProcessingEndTime = statusOrder == null ? null : statusOrder.ProcessingEndTime;
                    x.OrderId = statusOrder == null ? null : statusOrder.Id;
                    x.StaffNames = statusOrder == null ? "" : statusOrder.ServicestaffName;
                    if(otherStatusOrders.Count > 0)
                    {
                        foreach( var otherStatusOrder in otherStatusOrders)
                        {
                            var ServiceProductDto = serviceProductDtos.FirstOrDefault(y => y.ServiceInfoID == otherStatusOrder.ServiceInfoID && y.ProductSerial == otherStatusOrder.ProductSerial);

                            var ServiceProductDtoNew = new ServiceProductDto()
                            {
                                Id = ServiceProductDto.Id,
                                ServiceInfoID = ServiceProductDto.ServiceInfoID,
                                ProductSerial = ServiceProductDto.ProductSerial,
                                OrderId = otherStatusOrder.Id,
                                CompanyID = ServiceProductDto.CompanyID,
                                ServiceProcess = otherStatusOrder.ServiceprocessID,
                                ServiceOrderStatus = otherStatusOrder.ServiceOrderStatus,
                                ProcessingStartTime = otherStatusOrder.ProcessingStartTime,
                                ProcessingEndTime = otherStatusOrder.ProcessingEndTime,
                                StaffNames = otherStatusOrder.ServicestaffName,
                                ProductType = ServiceProductDto.ProductType,
                                Branding = ServiceProductDto.Branding,
                                BrandedContent = ServiceProductDto.BrandedContent,
                                Model = ServiceProductDto.Model,
                                ModelNumber = ServiceProductDto.ModelNumber,
                                ServiceTypeID = ServiceProductDto.ServiceTypeID,
                                PurchaseDate = ServiceProductDto.PurchaseDate,
                                PurchasePrice = ServiceProductDto.PurchasePrice,
                                PurchaseStore = ServiceProductDto.PurchaseStore,
                                GuaranteedIncomeBusiness = ServiceProductDto.GuaranteedIncomeBusiness,
                                GuaranteedIncomeManufactor = ServiceProductDto.GuaranteedIncomeManufactor,
                                Barcode1 = ServiceProductDto.Barcode1,
                                Barcode2 = ServiceProductDto.Barcode2
                            };
                            ServiceProductDtoList.Add(ServiceProductDtoNew);
                        }
                    }
                });
                if(ServiceProductDtoList.Count > 0)
                    serviceProductDtos.AddRange(ServiceProductDtoList);
            }
            return serviceProductDtos;
        }

        public async Task<AppSrvResult<bool>> ServiceMainIsExistsAsnyc(ServiceMainSearchExistsDto search)
        {
            search.TrimStringFields();
            if(search.Companyid == 0)
                return false;
            /*var expressionMain = ExpressionCreator
                .New<Da_Service_Main, Da_Service_Product>((x, y) => x.Id == y.ServiceInfoID && x.CompanyID == y.CompanyID)
                .And((x, y) => x.CompanyID == search.Companyid)
                .AndIf(search.UserType.IsNotNullOrWhiteSpace(), (x, y) => x.UserType.Equals(search.UserType))
                .AndIf(search.BusinessType.IsNotNullOrWhiteSpace(), (x, y) => x.BusinessType.Equals(search.BusinessType))
                .AndIf(search.Serviceprocess.IsNotNullOrWhiteSpace(), (x, y) => y.ServiceProcess.Equals(search.Serviceprocess))
                .AndIf(search.ProductType.IsNotNullOrWhiteSpace(), (x, y) => y.ProductType.Equals(search.ProductType))
                .AndIf(search.Branding.IsNotNullOrWhiteSpace(), (x, y) => y.Branding.Equals(search.Branding))
                .AndIf(search.BrandedContent.IsNotNullOrWhiteSpace(), (x, y) => y.BrandedContent.Equals(search.BrandedContent))
                .AndIf(search.ServiceTypeID.IsNotNullOrWhiteSpace(), (x, y) => y.ServiceTypeID.Equals(search.ServiceTypeID))
                .AndIf(search.PurchaseStore.IsNotNullOrWhiteSpace(), (x, y) => y.PurchaseStore.Equals(search.PurchaseStore));

            Expression<Func<Da_Service_Main, object>> entityPropertyExpression = x => x.Id;
            Expression<Func<Da_Service_Product, object>> secondEntityPropertyExpression = y => y.ServiceInfoID;
            var isExists = await _serviceMainRepository.AnyAsync(expressionMain, entityPropertyExpression, secondEntityPropertyExpression);*/
            var expressionMain = ExpressionCreator
                .New<Da_Service_Main>()
                .And(x => x.CompanyID == search.Companyid)
                .AndIf(search.UserType.IsNotNullOrWhiteSpace(), x => x.UserType.Equals(search.UserType))
                .AndIf(search.BusinessType.IsNotNullOrWhiteSpace(), x => x.BusinessType.Equals(search.BusinessType));
            var expressionProduct = ExpressionCreator
                .New<Da_Service_Product>()
                .AndIf(search.Serviceprocess.IsNotNullOrWhiteSpace(), y => y.ServiceProcess.Equals(search.Serviceprocess))
                .AndIf(search.ProductType.IsNotNullOrWhiteSpace(), y => y.ProductType.Equals(search.ProductType))
                .AndIf(search.Branding.IsNotNullOrWhiteSpace(), y => y.Branding.Equals(search.Branding))
                .AndIf(search.BrandedContent.IsNotNullOrWhiteSpace(), y => y.BrandedContent.Equals(search.BrandedContent))
                .AndIf(search.ServiceTypeID.IsNotNullOrWhiteSpace(), y => y.ServiceTypeID.Equals(search.ServiceTypeID))
                .AndIf(search.PurchaseStore.IsNotNullOrWhiteSpace(), y => y.PurchaseStore.Equals(search.PurchaseStore));
            var mainResp = _serviceMainRepository.Where(expressionMain);
            var productResp = _serviceProductRepository.Where(expressionProduct);
            var isExists = await (from s in mainResp
                                  join p in productResp
                                  on s.Id equals p.ServiceInfoID
                                  select s.Id
                                 ).AnyAsync();

            return isExists;

        }

        public async Task<AppSrvResult> UpdateAsync(ServiceMainUpdationDto input)
        {
            input.TrimStringFields();
            if(input.Id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案信息不正确", instance : errMsg);
            }
            var isExists = await _serviceMainRepository.AnyAsync(x => x.Id ==  input.Id);
            if (!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户档案信息不存在或已删除", instance: errMsg);
            }
            var serviceMain = await _serviceMainRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking: false);
            ConditionalMap(input, serviceMain);
            try
            {

                await _serviceMainRepository.UpdateAsync(serviceMain);
                return AppSrvResult();
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateAsync", "03");
                _logger.LogError("更新服务客户档案信息发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "更新服务客户档案信息发生意外错误", instance: errMsg, title : ex.Message);
            }
        }


        public async Task<AppSrvResult> UpdateServiceProductAsync(ServiceProductUpdationDto input)
        {
            input.TrimStringFields();
            if(input.Id == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateServiceProductAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务客户产品信息不正确", instance: errMsg);
            }
            var isExists = await _serviceProductRepository.AnyAsync(x => x.Id == input.Id);
            if(!isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateServiceProductAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务客户产品信息不存在或已删除", instance: errMsg);
            }
            var serviceProduct = await _serviceProductRepository.FetchAsync(x => x, x => x.Id == input.Id, noTracking:false);
            var serviceOrderProduct = await _orderProductRepository.Where(x => x.ServiceInfoId == serviceProduct.ServiceInfoID && x.ProductSerial == serviceProduct.ProductSerial, noTracking : false).ToListAsync();
            if (serviceOrderProduct.Count > 0)
            {
                var orderIds = serviceOrderProduct.Select(x => x.ServiceOrderId).ToArray();
                var serviceOrders = await _orderRepository.Where(x => x.ServiceInfoID == serviceProduct.ServiceInfoID && orderIds.Contains(x.ServiceOrderID), noTracking : false).ToListAsync();
                if (serviceOrders.Any(x => x.ServiceOrderStatus.Equals("B")))
                {
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateServiceProductAsync", "04");
                    return Problem(HttpStatusCode.BadRequest, "当前产品存在已完工工单，无法更改产品", instance: errMsg);
                }
                if(serviceOrders.Count > 1)
                {
                    var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateServiceProductAsync", "05");
                    return Problem(HttpStatusCode.BadRequest, "当前产品已派工多个流程工单，无法更改产品", instance: errMsg);
                }

                var serviceProcessIds = Array.Empty<string>();
                var serviceProcessIdList = new List<string>();
                try
                {
                    //var processResponse = await _restClient.GetCodeBmServiceProcessAsync(companyid);
                    var processRelationResponse = await _restClient.GetCodeBmRelationComparisonAsync(serviceProduct.CompanyID);
                    if (processRelationResponse != null && processRelationResponse.IsSuccessStatusCode)
                    {
                        var processRelationList = processRelationResponse.Content;
                        //
                        var processList = processRelationList.Where(x =>
                        (x.ServiceBrandID.Equals(serviceProduct.Branding) && x.BrandContentName.Equals(serviceProduct.BrandedContent))
                        || (x.ServiceBrandID.Equals("") && x.BrandContentName.Equals(serviceProduct.BrandedContent))
                        || (x.ServiceBrandID.Equals(serviceProduct.Branding) && x.BrandContentName.Equals(""))
                        || (x.ServiceBrandID.Equals("") && x.BrandContentName.Equals("")))
                            .ToList();
                        if (processList != null && processList.Count > 0)
                        {
                            if (processList.Any(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNotNullOrEmpty()))
                            {
                                //品牌+品牌内容全匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNotNullOrEmpty()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else if (processList.Any(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNotNullOrEmpty()))
                            {
                                //品牌内容单匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNotNullOrEmpty()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else if (processList.Any(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNullOrWhiteSpace()))
                            {
                                //品牌单匹
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNotNullOrEmpty() && x.BrandContentName.IsNullOrWhiteSpace()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                            else
                            {
                                //都为空 通用
                                var serviceProcessIdsTemp = processList.Where(x => x.ServiceBrandID.IsNullOrWhiteSpace() && x.BrandContentName.IsNullOrWhiteSpace()).Select(x => x.ServiceProcessID).ToList();
                                serviceProcessIdList.AddRange(serviceProcessIdsTemp);
                            }
                        }
                        serviceProcessIds = serviceProcessIdList.ToArray();
                    }


                    //验证更换产品后流程是否相同
                    if (input.ServiceProcessIds.IsNotNullOrWhiteSpace())
                    {
                        var oldServiceProcessIds = input.ServiceProcessIds.Split(',');
                        var areEqual = serviceProcessIds.SequenceEqual(oldServiceProcessIds.OrderBy(x => x));
                        if (!areEqual)
                        {
                            serviceProduct.ServiceProcess = serviceProcessIds[0];
                            serviceOrderProduct.ForEach(x => x.ServiceProcessId = serviceProcessIds[0]);
                            await _orderProductRepository.UpdateRangeAsync(serviceOrderProduct);
                            serviceOrders.ForEach(x => x.ServiceprocessID = serviceProcessIds[0]);
                            await _orderRepository.UpdateRangeAsync(serviceOrders);
                        }
                    }
                }
                catch (Exception ex)
                {
                    serviceProcessIds = Array.Empty<string>();
                }
                serviceProcessIds = serviceProcessIds.OrderBy(x => x).ToArray();
            }
            ConditionalMap(input, serviceProduct);
            try
            {
                await _serviceProductRepository.UpdateAsync(serviceProduct);
                return AppSrvResult();
            }
            catch(Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _restClient, "UpdateServiceProductAsync", "03");
                _logger.LogError("更新服务客户产品信息发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "更新服务客户产品信息发生意外错误", instance: errMsg, title : ex.Message);
            }
        }
    }
}
