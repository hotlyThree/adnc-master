using Adnc.Infra.Repository.EfCore.Repositories;
using System.Runtime.CompilerServices;

namespace Fstorch.AfterSales.Application.Services.Implements
{
    public class OrganizationService : AbstractAppService, IOrganizationService
    {
        private readonly IEfRepository<Bm_Service_Nature> _natureRepository;
        private readonly IEfRepository<Bm_Service_Type> _typeRepository;
        private readonly IEfRepository<Bm_User_Type> _userRepository;
        private readonly IEfRepository<Bm_Information_Source> _infoRepository;
        private readonly IEfRepository<Bm_Buy_Market> _marketRepository;
        private readonly IEfRepository<Bm_Service_Brand> _brandRepository;
        private readonly IEfRepository<Bm_Brand_Content> _contentRepository;
        private readonly IEfRepository<Bm_Settlement_Code> _settlementCodeRepository;
        private readonly IEfRepository<Bm_Service_Request> _requestRepository;
        private readonly IEfRepository<Bm_Service_Cause> _causeRepository;
        private readonly IEfRepository<Bm_Service_Follow> _followRepository;
        private readonly IEfRepository<Bm_Service_Tag> _tagRepository;
        private readonly IEfRepository<Da_Service_Order> _orderRepository;
        private readonly IEfRepository<Da_Service_Product> _productRepository;
        private readonly IEfRepository<Da_Service_Followup> _followupRepository;
        private readonly IEfRepository<Da_Service_Main> _mainRepository;
        private readonly ISystemRestClient _systemRestClient;
        private readonly ILogger<OrganizationService> _logger;
        private readonly CacheService _cacheService;
        public OrganizationService(IEfRepository<Bm_Service_Nature> natureRepository, IEfRepository<Bm_Service_Type> typeRepository, IEfRepository<Bm_User_Type> userRepository, IEfRepository<Bm_Information_Source> infoRepository, ISystemRestClient systemRestClient, ILogger<OrganizationService> logger, CacheService cacheService, IEfRepository<Bm_Buy_Market> marketRepository, IEfRepository<Bm_Service_Brand> brandRepository, IEfRepository<Bm_Brand_Content> contentRepository, IEfRepository<Bm_Settlement_Code> settlementCodeRepository, IEfRepository<Bm_Service_Request> requestRepository, IEfRepository<Bm_Service_Cause> causeRepository, IEfRepository<Bm_Service_Follow> followRepository, IEfRepository<Bm_Service_Tag> tagRepository, IEfRepository<Da_Service_Order> orderRepository, IEfRepository<Da_Service_Followup> followupRepository, IEfRepository<Da_Service_Product> productRepository,
            IEfRepository<Da_Service_Main> mainRepository)
        {
            this._natureRepository = natureRepository;
            this._typeRepository = typeRepository;
            this._userRepository = userRepository;
            this._infoRepository = infoRepository;
            this._systemRestClient = systemRestClient;
            this._logger = logger;
            this._cacheService = cacheService;
            this._marketRepository = marketRepository;
            this._brandRepository = brandRepository;
            this._contentRepository = contentRepository;
            this._settlementCodeRepository = settlementCodeRepository;
            this._requestRepository = requestRepository;
            this._causeRepository = causeRepository;
            this._followRepository = followRepository;
            this._tagRepository = tagRepository;
            this._orderRepository = orderRepository;
            this._followupRepository = followupRepository;
            this._productRepository = productRepository;
            this._mainRepository = mainRepository;
        }

        public async Task<AppSrvResult> CopyBrandContentAsync(long companyid, long targetcompanyid)
        {
            if(companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyBrandContentAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var brandContentList = await _contentRepository.Where(x => x.CompanyId == targetcompanyid, writeDb : true).ToListAsync();
            if (brandContentList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立品牌内容", instance: errMsg);
            }
            foreach(var content in brandContentList)
            {
                content.Id = IdGenerater.GetNextId();
                content.CompanyId = companyid;
            }
            try
            {
                await _contentRepository.InsertRangeAsync(brandContentList);
            }
            catch(Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyBrandContentAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制品牌内容时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyInfoSourceAsync(long companyid, long targetcompanyid)
        {
            if(companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyInfoSourceAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var infoSourceList = await _infoRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if(infoSourceList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyInfoSourceAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立信息来源", instance: errMsg);
            }
            foreach (var info in infoSourceList)
            {
                info.Id = IdGenerater.GetNextId();
                info.CompanyId = companyid;
            }
            try
            {
                await _infoRepository.InsertRangeAsync(infoSourceList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyInfoSourceAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制信息来源时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyMarketAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyMarketAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var infoSourceList = await _infoRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (infoSourceList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyMarketAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立购机商场", instance: errMsg);
            }
            foreach (var info in infoSourceList)
            {
                info.Id = IdGenerater.GetNextId();
                info.CompanyId = companyid;
            }
            try
            {
                await _infoRepository.InsertRangeAsync(infoSourceList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyMarketAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制购机商场时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceBrandAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceBrandAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var brandList = await _brandRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (brandList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceBrandAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立服务品牌", instance: errMsg);
            }
            foreach (var brand in brandList)
            {
                brand.Id = IdGenerater.GetNextId();
                brand.CompanyId = companyid;
            }
            try
            {
                await _brandRepository.InsertRangeAsync(brandList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceBrandAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制服务品牌时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceCauseAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceCauseAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var causeList = await _causeRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (causeList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceCauseAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立故障原因", instance: errMsg);
            }
            foreach (var cause in causeList)
            {
                cause.Id = IdGenerater.GetNextId();
                cause.CompanyId = companyid;
            }
            try
            {
                await _causeRepository.InsertRangeAsync(causeList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceCauseAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制故障原因时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceFollowAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceFollowAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var followList = await _followRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (followList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceFollowAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立回访编码", instance: errMsg);
            }
            foreach (var follow in followList)
            {
                follow.Id = IdGenerater.GetNextId();
                follow.CompanyId = companyid;
            }
            try
            {
                await _followRepository.InsertRangeAsync(followList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceFollowAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制回访编码时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceNatureAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceNatureAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var natureList = await _natureRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (natureList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceNatureAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立服务性质", instance: errMsg);
            }
            foreach (var nature in natureList)
            {
                nature.Id = IdGenerater.GetNextId();
                nature.CompanyId = companyid;
            }
            try
            {
                await _natureRepository.InsertRangeAsync(natureList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceNatureAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制服务性质时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceRequestAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceRequestAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var requestList = await _requestRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (requestList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceRequestAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立服务需求编码", instance: errMsg);
            }
            foreach (var request in requestList)
            {
                request.Id = IdGenerater.GetNextId();
                request.CompanyId = companyid;
            }
            try
            {
                await _requestRepository.InsertRangeAsync(requestList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceRequestAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制服务需求时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceTagAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTagAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var tagList = await _tagRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (tagList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTagAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立服务标注", instance: errMsg);
            }
            foreach (var tag in tagList)
            {
                tag.Id = IdGenerater.GetNextId();
                tag.CompanyId = companyid;
            }
            try
            {
                await _tagRepository.InsertRangeAsync(tagList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTagAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制服务标注时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyServiceTypeAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var typeList = await _typeRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (typeList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立服务类型", instance: errMsg);
            }
            foreach (var type in typeList)
            {
                type.Id = IdGenerater.GetNextId();
                type.CompanyId = companyid;
            }
            try
            {
                await _typeRepository.InsertRangeAsync(typeList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyServiceTypeAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制服务类型时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopySettlementAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopySettlementAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var settlementList = await _settlementCodeRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (settlementList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopySettlementAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立结算代码规则", instance: errMsg);
            }
            foreach (var code in settlementList)
            {
                code.Id = IdGenerater.GetNextId();
                code.CompanyId = companyid;
            }
            try
            {
                await _settlementCodeRepository.InsertRangeAsync(settlementList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopySettlementAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制结算代码时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> CopyUserTypeAsync(long companyid, long targetcompanyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyUserTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var userList = await _userRepository.Where(x => x.CompanyId == targetcompanyid, writeDb: true).ToListAsync();
            if (userList.Count == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyUserTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "该公司未建立用户类型", instance: errMsg);
            }
            foreach (var usertype in userList)
            {
                usertype.Id = IdGenerater.GetNextId();
                usertype.CompanyId = companyid;
            }
            try
            {
                await _userRepository.InsertRangeAsync(userList);
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CopyUserTypeAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "复制用户类型时发生错误", instance: errMsg, title: ex.InnerException.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult<long>> CreateBrandContentAsync(BrandContentCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateBrandContentAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _contentRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.BrandContentName.Equals(input.BrandContentName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的信息来源编码", instance: errMsg);
            }
            try
            {
                var brandContent = Mapper.Map<Bm_Brand_Content>(input);
                brandContent.Id = IdGenerater.GetNextId();
                await _contentRepository.InsertAsync(brandContent);
                return brandContent.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("创建品牌内容出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateBrandContentAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "创建品牌内容出现意外错误", instance: errMsg, title: ex.Message);
            }
        }


        public async Task<AppSrvResult<long>> CreateInfoSourceAsync(InformationSourceCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateInfoSourceAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance : errMsg);
            }
            var exists = await _infoRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.InfoSourceName.Equals(input.InfoSourceName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateInfoSourceAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的信息来源编码", instance: errMsg);
            }
            try
            {
                var infoSource = Mapper.Map<Bm_Information_Source>(input);
                infoSource.Id = IdGenerater.GetNextId();
                await _infoRepository.InsertAsync(infoSource);
                return infoSource.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("创建信息来源出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateInfoSourceAsync", "03");
                return Problem(HttpStatusCode.BadRequest, "创建信息来源出现意外错误", instance: errMsg, title : ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateMarketAsync(MarketCreationDto input, [CachingParam] long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateMarketAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _marketRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.BuyMarketName.Equals(input.BuyMarketName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateMarketAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的购机商场编码", instance: errMsg);
            }
            try
            {
                var market = Mapper.Map<Bm_Buy_Market>(input);
                market.Id = IdGenerater.GetNextId();
                await _marketRepository.InsertAsync(market);
                return market.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateMarketAsync", "03");
                _logger.LogError("创建购机商场发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建购机商场发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceBrandAsync(ServiceBrandCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceBrandAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _brandRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceBrandName.Equals(input.ServiceBrandName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceBrandAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务品牌编码", instance: errMsg);
            }
            try
            {
                var serviceBrand = Mapper.Map<Bm_Service_Brand>(input);
                serviceBrand.Id = IdGenerater.GetNextId();
                await _brandRepository.InsertAsync(serviceBrand);
                return serviceBrand.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceBrandAsync", "03");
                _logger.LogError("创建服务品牌发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务品牌发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceCauseAsync(ServiceCauseCreationDto input,long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceCauseAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _causeRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.CausemalFunction.Equals(input.CausemalFunction), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceCauseAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的故障原因编码", instance: errMsg);
            }
            try
            {
                var serviceCause = Mapper.Map<Bm_Service_Cause>(input);
                serviceCause.Id = IdGenerater.GetNextId();
                await _causeRepository.InsertAsync(serviceCause);
                return serviceCause.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceCauseAsync", "03");
                _logger.LogError("创建故障原因发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建故障原因发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceFollowAsync(ServiceFollowCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceFollowAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _followRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.FollowName.Equals(input.FollowName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceFollowAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务回访编码", instance: errMsg);
            }
            try
            {
                var serviceFollow = Mapper.Map<Bm_Service_Follow>(input);
                serviceFollow.Id = IdGenerater.GetNextId();
                await _followRepository.InsertAsync(serviceFollow);
                return serviceFollow.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceFollowAsync", "03");
                _logger.LogError("创建服务回访发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务回访发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceNatureAsync(ServiceNatureCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceNatureAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance : errMsg);
            }
            var exists = await _natureRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceNatureName.Equals(input.ServiceNatureName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceNatureAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务性质编码", instance : errMsg);
            }
            try
            {
                var serviceNature = Mapper.Map<Bm_Service_Nature>(input);
                serviceNature.Id = IdGenerater.GetNextId();
                await _natureRepository.InsertAsync(serviceNature);
                return serviceNature.Id;
            }
            catch(Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceNatureAsync", "03");
                _logger.LogError("创建服务性质发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务性质发生意外错误", instance : errMsg, title : ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceRequestAsync(ServiceRequestCreationDto input,long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceRequestAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _requestRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.RequestDescribe.Equals(input.RequestDescribe), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceRequestAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务需求编码", instance: errMsg);
            }
            try
            {
                var serviceRequest = Mapper.Map<Bm_Service_Request>(input);
                serviceRequest.Id = IdGenerater.GetNextId();
                await _requestRepository.InsertAsync(serviceRequest);
                return serviceRequest.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceRequestAsync", "03");
                _logger.LogError("创建服务需求发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务需求发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceTagAsync(ServiceTagCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTagAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _tagRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceTag.Equals(input.ServiceTag), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTagAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务标注", instance: errMsg);
            }
            try
            {
                var serviceTag = Mapper.Map<Bm_Service_Tag>(input);
                serviceTag.Id = IdGenerater.GetNextId();
                await _tagRepository.InsertAsync(serviceTag);
                return serviceTag.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTagAsync", "03");
                _logger.LogError("创建服务标注发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务标注发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateServiceTypeAsync(ServiceTypeCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance : errMsg);
            }
            var exists = await _typeRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceTypeName.Equals(input.ServiceTypeName), writeDb : true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的服务类型", instance : errMsg);
            }
            try
            {
                var serviceType = Mapper.Map<Bm_Service_Type>(input);
                serviceType.Id = IdGenerater.GetNextId();
                await _typeRepository.InsertAsync(serviceType);
                return serviceType.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateServiceTypeAsync", "03");
                _logger.LogError("创建服务类型发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建服务类型发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateSettlementAsync(SettlementCodeCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateSettlementAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _settlementCodeRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.SettlementDescribe.Equals(input.SettlementDescribe), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateSettlementAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的结算代码", instance: errMsg);
            }
            try
            {
                var settlement = Mapper.Map<Bm_Settlement_Code>(input);
                settlement.Id = IdGenerater.GetNextId();
                await _settlementCodeRepository.InsertAsync(settlement);
                return settlement.Id;
            }
            catch (Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateSettlementAsync", "03");
                _logger.LogError("创建结算代码发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建结算代码发生意外错误", instance: errMsg, title: ex.Message);
            }
        }

        public async Task<AppSrvResult<long>> CreateUserTypeAsync(UserTypeCreationDto input, long companyid)
        {
            input.TrimStringFields();
            if (input.CompanyId == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateUserTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "公司不正确", instance: errMsg);
            }
            var exists = await _userRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.UserTypeName.Equals(input.UserTypeName), writeDb: true);
            if (exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateUserTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "存在相同的用户类型编码", instance: errMsg);
            }
            try
            {
                var userType = Mapper.Map<Bm_User_Type>(input);
                userType.Id = IdGenerater.GetNextId();
                await _userRepository.InsertAsync(userType);
                return userType.Id;
            }
            catch(Exception ex)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "CreateUserTypeAsync", "03");
                _logger.LogError("创建用户类型发生意外错误:{message}", ex.Message);
                return Problem(HttpStatusCode.BadRequest, "创建用户类型发生意外错误", instance: errMsg, title : ex.Message);
            }
        }

        public async Task<AppSrvResult> DeleteBrandContentAsync(long contentid, long companyid)
        {
            if (contentid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "品牌内容或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _productRepository.AnyAsync(x => x.CompanyID == companyid && x.BrandedContent.Equals(contentid.ToString()), writeDb: true);
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "当前品牌内容已在业务中使用", instance: errMsg);
            }
            await _contentRepository.DeleteAsync(contentid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteInfoSourceAsync(long infoid, long companyid)
        {
            if(infoid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteInfoSourceAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "信息来源或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _mainRepository.AnyAsync(x => x.CompanyID == companyid && x.UserSource.Equals(infoid.ToString()), writeDb: true);
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "当前信息来源已在业务中使用", instance: errMsg);
            }
            await _infoRepository.DeleteAsync(infoid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteMarketAsync(long marketid, long companyid)
        {
            if (marketid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteMarketAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "购机商场或公司不正确", instance: errMsg);
            }
            //验证是否被使用过


            await _marketRepository.DeleteAsync(marketid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceBrandAsync(long brandid, long companyid)
        {
            if (brandid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceBrandAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务品牌或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _productRepository.AnyAsync(x => x.CompanyID == brandid && x.Branding.Equals(brandid.ToString()), writeDb: true);
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "当前品牌已在业务中使用", instance: errMsg);
            }

            await _brandRepository.DeleteAsync(brandid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceCauseAsync(long causeid, long companyid)
        {
            if (causeid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceCauseAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "故障原因或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _orderRepository.AnyAsync(x => x.CompanyID == companyid && x.FailureCause.Equals(causeid.ToString()), writeDb: true);
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "当前故障原因已在业务中使用", instance: errMsg);
            }

            await _causeRepository.DeleteAsync(causeid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceFollowAsync(long followid, long companyid)
        {
            if (followid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceFollowAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务回访编码或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _followupRepository.AnyAsync(x => x.CompanyId == companyid && x.FollowupId.Equals(followid.ToString()), writeDb: true);
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "当前服务回访编码已在业务中使用", instance: errMsg);
            }

            await _followRepository.DeleteAsync(followid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceNatureAsync(long natureid, long companyid)
        {
            if (natureid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceNatureAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务性质或公司不正确", instance: errMsg);
            }
            //验证是否被使用过

            await _natureRepository.DeleteAsync(natureid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceRequestAsync(long requestid, long companyid)
        {
            if (requestid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceRequestAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务需求或公司不正确", instance: errMsg);
            }
            //验证是否被使用过
            var isExists = await _mainRepository.AnyAsync(x => x.CompanyID == companyid && x.ServiceRequest.Equals(requestid.ToString()));
            if (isExists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceRequestAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "服务性质或公司不正确", instance: errMsg);
            }
            await _requestRepository.DeleteAsync(requestid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceTagAsync(long tagid, long companyid)
        {
            if (tagid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceTagAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务标注或公司不正确", instance: errMsg);
            }
            //验证是否被使用过

            await _tagRepository.DeleteAsync(tagid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteServiceTypeAsync(long typeid, long companyid)
        {
            if (typeid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteServiceTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务类型或公司不正确", instance: errMsg);
            }
            //验证是否被使用过

            await _typeRepository.DeleteAsync(typeid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteSettlementAsync(long settlementid, long companyid)
        {
            if (settlementid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteSettlementAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "结算代码或公司不正确", instance: errMsg);
            }
            //验证是否被使用过

            await _settlementCodeRepository.DeleteAsync(settlementid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteUserTypeAsync(long typeid, long companyid)
        {
            if (typeid == 0 || companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "DeleteUserTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "用户类型或公司不正确", instance: errMsg);
            }
            //验证是否被使用过

            await _userRepository.DeleteAsync(typeid);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<List<BrandContentDto>>> GetBrandContentListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetBrandContentListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }

            var brandContentDtos = await _cacheService.GetBrandContentListFromCacheAsync(companyid);
            return brandContentDtos;
        }

        public async Task<AppSrvResult<List<InformationSourceDto>>> GetInfoSourceListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetInfoSourceListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            //var informationSourceList = await _infoRepository.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
            var informationSourceDtos = await _cacheService.GetInfoSourceListFromCacheAsync(companyid);
            return informationSourceDtos;
        }

        public async Task<AppSrvResult<List<MarketDto>>> GetMarketListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetMarketListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var marketDtos = await _cacheService.GetMarketListFromCacheAsync(companyid);
            return marketDtos;
        }

        public async Task<AppSrvResult<List<ServiceBrandDto>>> GetServiceBrandListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceBrandListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceBrandDtos = await _cacheService.GetServiceBrandListFromCacheAsync(companyid);
            return serviceBrandDtos;
        }

        public async Task<AppSrvResult<List<ServiceCauseDto>>> GetServiceCauseListAsync([CachingParam] long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceCauseListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceCauseDtos = await _cacheService.GetServiceCauseListFromCacheAsync(companyid);
            return serviceCauseDtos;
        }

        public async Task<AppSrvResult<List<ServiceFollowDto>>> GetServiceFollowListAsync([CachingParam] long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceFollowListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceFollowDtos = await _cacheService.GetServiceFollowListFromCacheAsync(companyid);
            return serviceFollowDtos;
        }

        public async Task<AppSrvResult<List<ServiceNatureDto>>> GetServiceNatureListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetInfoSourceListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            //var serviceNatureList = await _natureRepository.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
            var serviceNatureDtos = await _cacheService.GetServiceNatureListFromCacheAsync(companyid);
            return serviceNatureDtos;
        }

        public async Task<AppSrvResult<ServiceRequestDto>> GetServiceRequestFirstTest(long companyid)
        {
            var serviceRequestDto = await _cacheService.GetServiceRequestListFromCacheAsyncTest(companyid);
            return serviceRequestDto;
        }

        public async Task<AppSrvResult<List<ServiceRequestDto>>> GetServiceRequestListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceRequestListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceRequestDtos = await _cacheService.GetServiceRequestListFromCacheAsync(companyid);
            return serviceRequestDtos;
        }

        public async Task<AppSrvResult<List<ServiceTagDto>>> GetServiceTagListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceTagListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceTagDtos = await _cacheService.GetServiceTagListFromCacheAsync(companyid);
            return serviceTagDtos;
        }

        public async Task<AppSrvResult<List<ServiceTypeDto>>> GetServiceTypeListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetServiceTypeListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            //var serviceTypeList = await _typeRepository.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
            var serviceTypeDtos = await _cacheService.GetServiceTypeListFromCacheAsync(companyid);
            return serviceTypeDtos;
        }

        public async Task<AppSrvResult<List<SettlementCodeDto>>> GetSettlementListAsync(long companyid)
        {
            if (companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetSettlementListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance: errMsg);
            }
            var serviceTypeDtos = await _cacheService.GetSettlementListFromCacheAsync(companyid);
            return serviceTypeDtos;
        }

        public async Task<AppSrvResult<List<UserTypeDto>>> GetUserTypeListAsync(long companyid)
        {
            if(companyid == 0)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "GetUserTypeListAsync", "01");
                return Problem(HttpStatusCode.NotFound, "公司不正确", instance : errMsg);
            }
            //var userTypeList = await _userRepository.Where(x => x.CompanyId == companyid).OrderBy(x => x.DisplayNumber).ToListAsync();
            var userTypeDtos = await _cacheService.GetUserTypeListFromCacheAsync(companyid);
            return userTypeDtos;
        }

        public async Task<AppSrvResult> UpdateBrandContentAsync(BrandContentUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _contentRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateBrandContentAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "品牌内容不存在或已删除", instance: errMsg);
            }
            var exists2 = await _contentRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.BrandContentName.Equals(input.BrandContentName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber && x.ServiceBrandId.Equals(input.ServiceBrandId), writeDb:true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var brandContent = Mapper.Map<Bm_Brand_Content>(input);
                await _contentRepository.UpdateAsync(brandContent, UpdatingProps<Bm_Brand_Content>(x => x.BrandContentName, x => x.IsValid, x => x.DisplayNumber, x => x.ServiceBrandId));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新品牌内容出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateBrandContentAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新信息来源出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }


        public async Task<AppSrvResult> UpdateInfoSourceAsync(InformationSourceUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _infoRepository.AnyAsync(x => x.Id == input.Id);
            if(!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateInfoSourceAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "信息来源不存在或已删除", instance: errMsg);
            }
            var exists2 = await _infoRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.InfoSourceName.Equals(input.InfoSourceName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var infoSource = Mapper.Map<Bm_Information_Source>(input);
                await _infoRepository.UpdateAsync(infoSource, UpdatingProps<Bm_Information_Source>(x => x.InfoSourceName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新信息来源出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateInfoSourceAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新信息来源出现意外错误", instance: errMsg, title : ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateMarketAsync(MarketUpdationDto input,  long companyid)
        {
            input.TrimStringFields();
            var exists = await _marketRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateMarketAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "购机商场不存在或已删除", instance: errMsg);
            }
            var exists2 = await _marketRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.BuyMarketName.Equals(input.BuyMarketName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var infoSource = Mapper.Map<Bm_Buy_Market>(input);
                await _marketRepository.UpdateAsync(infoSource, UpdatingProps<Bm_Buy_Market>(x => x.BuyMarketName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新购机商场出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateMarketAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新购机商场出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceBrandAsync(ServiceBrandUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _brandRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceBrandAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务品牌不存在或已删除", instance: errMsg);
            }
            var exists2 = await _brandRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceBrandName.Equals(input.ServiceBrandName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceBrand = Mapper.Map<Bm_Service_Brand>(input);
                await _brandRepository.UpdateAsync(serviceBrand, UpdatingProps<Bm_Service_Brand>(x => x.ServiceBrandName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新服务品牌出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceBrandAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务品牌出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceCauseAsync(ServiceCauseUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _causeRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceCauseAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "故障原因不存在或已删除", instance: errMsg);
            }
            var exists2 = await _causeRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.CausemalFunction.Equals(input.CausemalFunction) && x.IsValid.Equals(input.IsValid) && x.Branding.Equals(input.Branding) && x.BrandingContent.Equals(input.BrandingContent) && x.RequestLabel.Equals(input.RequestLabel) && x.SolutionArticle.Equals(input.SolutionArticle) && x.SolutionAttachment.Equals(input.SolutionAttachment), writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceCause = Mapper.Map<Bm_Service_Cause>(input);
                await _causeRepository.UpdateAsync(serviceCause, UpdatingProps<Bm_Service_Cause>(x => x.CausemalFunction, x => x.IsValid, x => x.Branding, x => x.BrandingContent, x => x.RequestLabel, x => x.SolutionArticle, x => x.SolutionAttachment));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新故障原因出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceCauseAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新故障原因出现意外错误", instance: errMsg, title: ex.Message);
            }

            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceFollowAsync(ServiceFollowUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _followRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceFollowAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务回访编码不存在或已删除", instance: errMsg);
            }
            var exists2 = await _followRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.FollowName.Equals(input.FollowName) && x.IsValid.Equals(input.IsValid) && x.FollowType.Equals(input.FollowType) && x.FollowNumber == input.FollowNumber && x.ServiceTypeId.Equals(input.ServiceTypeId), writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceFollow = Mapper.Map<Bm_Service_Follow>(input);
                await _followRepository.UpdateAsync(serviceFollow, UpdatingProps<Bm_Service_Follow>(x => x.FollowName, x => x.IsValid, x => x.FollowType, x => x.FollowNumber, x => x.ServiceTypeId));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新服务回访出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceFollowAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务回访出现意外错误", instance: errMsg, title: ex.Message);
            }

            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceNatureAsync(ServiceNatureUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _natureRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceNatureAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务性质不存在或已删除", instance: errMsg);
            }
            var exists2 = await _natureRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceNatureName.Equals(input.ServiceNatureName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceNature = Mapper.Map<Bm_Service_Nature>(input);
                await _natureRepository.UpdateAsync(serviceNature, UpdatingProps<Bm_Service_Nature>(x => x.ServiceNatureName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch(Exception ex)
            {
                _logger.LogError("更新服务性质出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceNatureAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务性质出现意外错误", instance: errMsg, title: ex.Message);
            }
            
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceRequestAsync(ServiceRequestUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _requestRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceRequestAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务需求不存在或已删除", instance: errMsg);
            }
            var exists2 = await _requestRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.RequestDescribe.Equals(input.RequestDescribe) && x.IsValid.Equals(input.IsValid) && x.Branding.Equals(input.Branding) && x.BrandingContent.Equals(input.BrandingContent), writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceRequest = Mapper.Map<Bm_Service_Request>(input);
                await _requestRepository.UpdateAsync(serviceRequest, UpdatingProps<Bm_Service_Request>(x => x.IsValid, x => x.Branding, x => x.BrandingContent, x => x.RequestDescribe));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新服务需求出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceRequestAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务需求出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceTagAsync(ServiceTagUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _tagRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceTagAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务标注不存在或已删除", instance: errMsg);
            }
            var exists2 = await _tagRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceTag.Equals(input.ServiceTag) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber && x.ServiceStatusId.Equals(input.ServiceStatusId) && x.ServiceTypeId.Equals(input.ServiceTypeId), writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceTag = Mapper.Map<Bm_Service_Tag>(input);
                await _tagRepository.UpdateAsync(serviceTag, UpdatingProps<Bm_Service_Tag>(x => x.ServiceTag, x => x.IsValid, x => x.DisplayNumber, x => x.ServiceStatusId, x => x.ServiceTypeId));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新服务标注出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceTagAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务标注出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateServiceTypeAsync(ServiceTypeUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _typeRepository.AnyAsync(x => x.Id==input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "服务类型不存在或已删除", instance: errMsg);
            }
            var exists2 = await _typeRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.ServiceTypeName.Equals(input.ServiceTypeName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if(exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var serviceType = Mapper.Map<Bm_Service_Type>(input);
                await _typeRepository.UpdateAsync(serviceType, UpdatingProps<Bm_Service_Type>(x => x.ServiceTypeName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch(Exception ex)
            {
                _logger.LogError("更新服务类型出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateServiceTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新服务类型出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateSettlementAsync(SettlementCodeUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _settlementCodeRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateSettlementAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "结算代码不存在或已删除", instance: errMsg);
            }
            var exists2 = await _settlementCodeRepository.AnyAsync(x => x.CompanyId == companyid && x.SettlementDescribe.Equals(input.SettlementDescribe) && x.IsValid.Equals(input.IsValid), writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var settlement = Mapper.Map<Bm_Settlement_Code>(input);
                await _settlementCodeRepository.UpdateAsync(settlement, UpdatingProps<Bm_Settlement_Code>(x => x.ServiceProviderId, x => x.IsValid, x => x.SettlementDescribe, x => x.SettlementFormula, x => x.SettlementCondition, x => x.SettlementConditionDescribe, x => x.ProviderCommission,
                    x => x.EngineerCommission, x => x.Receivable));
            }
            catch (Exception ex)
            {
                _logger.LogError("更新结算代码出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateSettlementAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新结算代码出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> UpdateUserTypeAsync(UserTypeUpdationDto input, long companyid)
        {
            input.TrimStringFields();
            var exists = await _userRepository.AnyAsync(x => x.Id == input.Id);
            if (!exists)
            {
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateUserTypeAsync", "01");
                return Problem(HttpStatusCode.BadRequest, "用户类型不存在或已删除", instance : errMsg);
            }
            var exists2 = await _userRepository.AnyAsync(x => x.CompanyId == input.CompanyId && x.UserTypeName.Equals(input.UserTypeName) && x.IsValid.Equals(input.IsValid) && x.DisplayNumber == input.DisplayNumber, writeDb: true);
            if (exists2)
            {
                return AppSrvResult();
            }
            try
            {
                var userType = Mapper.Map<Bm_User_Type>(input);
                await _userRepository.UpdateAsync(userType, UpdatingProps<Bm_User_Type>(x => x.UserTypeName, x => x.IsValid, x => x.DisplayNumber));
            }
            catch(Exception ex)
            {
                _logger.LogError("更新用户类型出现意外错误:{message}", ex.Message);
                var errMsg = await LanguageMsgService.GetMsg(_logger, _systemRestClient, "UpdateUserTypeAsync", "02");
                return Problem(HttpStatusCode.BadRequest, "更新用户类型出现意外错误", instance: errMsg, title: ex.Message);
            }
            return AppSrvResult();
        }

    }
}
