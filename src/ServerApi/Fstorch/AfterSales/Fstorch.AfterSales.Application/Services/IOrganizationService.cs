

namespace Fstorch.AfterSales.Application.Services
{
    /// <summary>
    /// 组织架构管理
    /// </summary>
    public interface IOrganizationService : IAppService
    {
        /// <summary>
        /// 创建服务类型[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTypeListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceTypeAsync(ServiceTypeCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新服务类型[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务类型名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTypeListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceTypeAsync(ServiceTypeUpdationDto input, [CachingParam] long companyid);


        /// <summary>
        /// 删除服务类型[多语言返回=01:服务类型或公司不正确]
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTypeListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceTypeAsync(long typeid, [CachingParam]long companyid);

        /// <summary>
        /// 复制服务类型
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceTypeAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取企业服务类型[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceTypeListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceTypeDto>>> GetServiceTypeListAsync([CachingParam]long companyid);


        /// <summary>
        /// 创建服务性质[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceNatureListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceNatureAsync(ServiceNatureCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新服务性质[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务性质名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceNatureListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceNatureAsync(ServiceNatureUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除服务性质[多语言返回=01:服务性质或公司不正确]
        /// </summary>
        /// <param name="natureid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceNatureListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceNatureAsync(long natureid, [CachingParam]long companyid);


        /// <summary>
        /// 复制服务性质
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceNatureAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取企业服务性质[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceNatureListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceNatureDto>>> GetServiceNatureListAsync([CachingParam]long companyid);


        /// <summary>
        /// 创建企业售后用户类型[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserTypeListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateUserTypeAsync(UserTypeCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新企业售后用户类型[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的用户类型名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserTypeListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateUserTypeAsync(UserTypeUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除企业售后用户类型[多语言返回=01:用户类型或公司不正确]
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserTypeListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteUserTypeAsync(long typeid, [CachingParam]long companyid);


        /// <summary>
        /// 复制用户类型
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyUserTypeAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取企业售后用户类型[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.UserTypeListCacheKeyPrefix)]
        Task<AppSrvResult<List<UserTypeDto>>> GetUserTypeListAsync([CachingParam]long companyid);

        /// <summary>
        /// 创建信息来源[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.InfoSourceListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateInfoSourceAsync(InformationSourceCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新信息来源[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的信息来源名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.InfoSourceListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateInfoSourceAsync(InformationSourceUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除信息来源[多语言返回=01:信息来源或公司不正确]
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.InfoSourceListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteInfoSourceAsync(long infoid, [CachingParam]long companyid);


        /// <summary>
        /// 复制信息来源
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyInfoSourceAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取企业信息来源[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.InfoSourceListCacheKeyPrefix)]
        Task<AppSrvResult<List<InformationSourceDto>>> GetInfoSourceListAsync([CachingParam]long companyid);


        /// <summary>
        /// 创建购机商场[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.MarketListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateMarketAsync(MarketCreationDto input, [CachingParam]long companyid);

        /// <summary>
        /// 更新购机商场[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的购机商场名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.MarketListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateMarketAsync(MarketUpdationDto input, [CachingParam]long companyid);

        /// <summary>
        /// 删除购机商场[多语言返回=01:购机商场或公司不正确]
        /// </summary>
        /// <param name="marketid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.MarketListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteMarketAsync(long marketid, [CachingParam]long companyid);

        /// <summary>
        /// 复制购机商场
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyMarketAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取购机商场列表[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.MarketListCacheKeyPrefix)]
        Task<AppSrvResult<List<MarketDto>>> GetMarketListAsync([CachingParam]long companyid);

        /// <summary>
        /// 创建服务品牌[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceBrandListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceBrandAsync(ServiceBrandCreationDto input, [CachingParam] long companyid);


        /// <summary>
        /// 更新服务品牌[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务品牌名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceBrandListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceBrandAsync(ServiceBrandUpdationDto input, [CachingParam] long companyid);


        /// <summary>
        /// 删除服务品牌[多语言返回=01:服务品牌或公司不正确]
        /// </summary>
        /// <param name="brandid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceBrandListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceBrandAsync(long brandid, [CachingParam]long companyid);

        /// <summary>
        /// 复制服务品牌
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceBrandAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取服务品牌[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceBrandListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceBrandDto>>> GetServiceBrandListAsync([CachingParam]long companyid);


        /// <summary>
        /// 创建品牌内容[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.BrandContentListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateBrandContentAsync(BrandContentCreationDto input, [CachingParam] long companyid);


        /// <summary>
        /// 更新品牌内容[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的品牌内容名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.BrandContentListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateBrandContentAsync(BrandContentUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除品牌内容[多语言返回=01:服务品牌内容或公司不正确]
        /// </summary>
        /// <param name="contentid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.BrandContentListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteBrandContentAsync(long contentid, [CachingParam]long companyid);

        /// <summary>
        /// 复制品牌内容
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyBrandContentAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取品牌内容[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.BrandContentListCacheKeyPrefix)]
        Task<AppSrvResult<List<BrandContentDto>>> GetBrandContentListAsync([CachingParam]long companyid);


        /// <summary>
        /// 创建结算代码[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.SettlementListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateSettlementAsync(SettlementCodeCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新结算代码[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的结算代码名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.SettlementListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateSettlementAsync(SettlementCodeUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除结算代码[多语言返回=01:结算代码或公司不正确]
        /// </summary>
        /// <param name="settlementid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.SettlementListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteSettlementAsync(long settlementid, [CachingParam]long companyid);

        /// <summary>
        /// 复制结算代码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopySettlementAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取结算代码[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.SettlementListCacheKeyPrefix)]
        Task<AppSrvResult<List<SettlementCodeDto>>> GetSettlementListAsync([CachingParam] long companyid);


        /// <summary>
        /// /创建服务需求[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceRequestListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceRequestAsync(ServiceRequestCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新服务需求[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务请求名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceRequestListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceRequestAsync(ServiceRequestUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除服务需求[多语言返回=01:服务需求或公司不正确]
        /// </summary>
        /// <param name="requestid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceRequestListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceRequestAsync(long requestid, [CachingParam] long companyid);

        /// <summary>
        /// 复制服务需求
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceRequestAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取服务请求[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceRequestListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceRequestDto>>> GetServiceRequestListAsync([CachingParam] long companyid);

        /// <summary>
        /// 创建故障原因[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceCauseListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceCauseAsync(ServiceCauseCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新故障原因[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的故障原因名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceCauseListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceCauseAsync(ServiceCauseUpdationDto input, [CachingParam] long companyid);


        /// <summary>
        /// 删除故障原因[多语言返回=01:故障原因或公司不正确]
        /// </summary>
        /// <param name="causeid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceCauseListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceCauseAsync(long causeid, [CachingParam] long companyid);

        /// <summary>
        /// 复制故障原因
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceCauseAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取故障原因[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceCauseListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceCauseDto>>> GetServiceCauseListAsync([CachingParam] long companyid);

        /// <summary>
        /// 创建服务回访编码[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceFollowListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceFollowAsync(ServiceFollowCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新服务回访编码[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务回访名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceFollowListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceFollowAsync(ServiceFollowUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 删除服务回访编码[多语言返回=01:回访编码或公司不正确]
        /// </summary>
        /// <param name="followid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceFollowListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceFollowAsync(long followid, [CachingParam] long companyid);


        /// <summary>
        /// 复制回访编码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceFollowAsync(long companyid, long targetcompanyid);

        /// <summary>
        /// 获取服务回访编码[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceFollowListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceFollowDto>>> GetServiceFollowListAsync([CachingParam] long companyid);

        /// <summary>
        /// 创建服务标注[多语言返回=01:公司不正确,02:重复创建,03:创建错误]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTagListCacheKeyPrefix)]
        Task<AppSrvResult<long>> CreateServiceTagAsync(ServiceTagCreationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 更新服务标注[多语言返回=01:内容不存在或已删除,02:更新错误,03:已存在相同的服务标注名称]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTagListCacheKeyPrefix)]
        Task<AppSrvResult> UpdateServiceTagAsync(ServiceTagUpdationDto input, [CachingParam] long companyid);

        /// <summary>
        /// 复制服务标注
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        Task<AppSrvResult> CopyServiceTagAsync(long companyid, long targetcompanyid);


        /// <summary>
        /// 删除服务标注[多语言返回=01:服务标注或公司不正确]
        /// </summary>
        /// <param name="tagid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingEvict(CacheKeyPrefix = CachingConsts.ServiceTagListCacheKeyPrefix)]
        Task<AppSrvResult> DeleteServiceTagAsync(long tagid, [CachingParam] long companyid);


        /// <summary>
        /// 获取服务标注[多语言返回=01:公司不正确]
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceTagListCacheKeyPrefix)]
        Task<AppSrvResult<List<ServiceTagDto>>> GetServiceTagListAsync([CachingParam] long companyid);



        [CachingAble(CacheKeyPrefix = CachingConsts.ServiceRequestListCacheKeyPrefix + ":test")]
        Task<AppSrvResult<ServiceRequestDto>> GetServiceRequestFirstTest([CachingParam]long companyid);


    }
}
