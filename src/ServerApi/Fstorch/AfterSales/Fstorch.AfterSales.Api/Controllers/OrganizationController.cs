using Adnc.Demo.Shared.Const.Permissions.Fstorch;
using Adnc.Shared.WebApi.Authorization;
using Fstorch.AfterSales.Application.Dtos.BrandContent;

namespace Fstorch.AfterSales.Api.Controllers
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [ApiController]
    [Route($"{RouteConsts.AfterSalesOrganization}")]
    public class OrganizationController : AdncControllerBase
    {
        private readonly IOrganizationService _organizationService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="organizationService"></param>
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// 创建服务类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/servicetype")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceTypeAsync([FromBody]ServiceTypeCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceTypeAsync(input, input.CompanyId));

        /// <summary>
        /// 更新服务类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/servicetype")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceTypeAsync([FromBody]ServiceTypeUpdationDto input)
            => Result(await _organizationService.UpdateServiceTypeAsync(input, input.CompanyId));


        /// <summary>
        /// 复制服务类型
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpPost("servicetype/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceTypeAsync([FromRoute] long companyid, [FromQuery] long targetcompanyid)
            => Result(await _organizationService.CopyServiceTypeAsync(companyid, targetcompanyid));


        /// <summary>
        /// 删除服务类型
        /// </summary>
        /// <param name="typeid">服务类型ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/servicetype/{typeid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceTypeAsync([FromRoute]long typeid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteServiceTypeAsync(typeid, companyid));

        /// <summary>
        /// 获取企业服务类型
        /// </summary>
        /// <param name="companyid">企业ID</param>
        /// <returns></returns>
        [HttpGet("servicetype/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceTypeDto>>> GetServiceTypeListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetServiceTypeListAsync(companyid));

        /// <summary>
        /// 创建服务性质
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/servicenature")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceNatureAsync([FromBody]ServiceNatureCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceNatureAsync(input, input.CompanyId));

        /// <summary>
        /// 更新服务性质
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/servicenature")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceNatureAsync([FromBody] ServiceNatureUpdationDto input)
            => Result(await _organizationService.UpdateServiceNatureAsync(input, input.CompanyId));



        /// <summary>
        /// 删除服务性质
        /// </summary>
        /// <param name="natureid">服务性质ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/servicenature/{natureid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceNatureAsync([FromRoute] long natureid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteServiceNatureAsync(natureid, companyid));


        /// <summary>
        /// 复制服务性质
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpPost("servicenature/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceNatureAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceNatureAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取企业服务性质
        /// </summary>
        /// <param name="companyid">企业ID</param>
        /// <returns></returns>
        [HttpGet("servicenature/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceNatureDto>>> GetServiceNatureListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetServiceNatureListAsync(companyid));

        /// <summary>
        /// 创建企业售后用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/usertype")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateUserTypeAsync([FromBody]UserTypeCreationDto input)
            => Result(await _organizationService.CreateUserTypeAsync(input, input.CompanyId));

        /// <summary>
        /// 更新企业售后用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/usertype")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateUserTypeAsync([FromBody]UserTypeUpdationDto input)
            => Result(await _organizationService.UpdateUserTypeAsync(input, input.CompanyId));

        /// <summary>
        /// 删除企业售后用户类型
        /// </summary>
        /// <param name="typeid">用户类型ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/usertype/{typeid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteUserTypeAsync([FromRoute]long typeid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteUserTypeAsync(typeid, companyid));

        /// <summary>
        /// 复制用户类型
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpPost("usertype/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyUserTypeAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyUserTypeAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取企业售后用户类型
        /// </summary>
        /// <param name="companyid">企业ID</param>
        /// <returns></returns>
        [HttpGet("usertype/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<UserTypeDto>>> GetUserTypeListAsync([FromQuery] long companyid)
            => Result(await _organizationService.GetUserTypeListAsync(companyid));


        /// <summary>
        /// 创建信息来源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/infosource")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateInfoSourceAsync([FromBody]InformationSourceCreationDto input)
            => CreatedResult(await _organizationService.CreateInfoSourceAsync(input, input.CompanyId));

        /// <summary>
        /// 更新信息来源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/infosource")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateInfoSourceAsync([FromBody]InformationSourceUpdationDto input)
            => Result(await _organizationService.UpdateInfoSourceAsync(input, input.CompanyId));

        /// <summary>
        /// 删除信息来源
        /// </summary>
        /// <param name="infoid">信息来源ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/infosource/{infoid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteInfoSourceAsync([FromRoute]long infoid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteInfoSourceAsync(infoid, companyid));

        /// <summary>
        /// 复制信息来源
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("infosource/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyInfoSourceAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyInfoSourceAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取企业信息来源
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("infosource/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<InformationSourceDto>>> GetInfoSourceListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetInfoSourceListAsync(companyid));


        /// <summary>
        /// 创建购机商场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/market")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateMarketAsync([FromBody] MarketCreationDto input)
            => CreatedResult(await _organizationService.CreateMarketAsync(input, input.CompanyId));

        /// <summary>
        /// 更新购机商场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/market")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateMarketAsync([FromBody]MarketUpdationDto input)
            => Result(await _organizationService.UpdateMarketAsync(input, input.CompanyId));

        /// <summary>
        /// 删除购机商场
        /// </summary>
        /// <param name="marketid">商场ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/market/{marketid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteMarketAsync([FromRoute]long marketid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteMarketAsync(marketid, companyid));

        /// <summary>
        /// 复制购机商场
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("market/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyMarketAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyMarketAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取购机商场列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        [HttpGet("market/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<MarketDto>>> GetMarketListAsync([FromQuery]long companyid)
            =>Result(await _organizationService.GetMarketListAsync(companyid));


        /// <summary>
        /// 创建服务品牌
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/brand")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceBrandAsync(ServiceBrandCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceBrandAsync(input, input.CompanyId));


        /// <summary>
        /// 更新服务品牌
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/brand")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceBrandAsync([FromBody]ServiceBrandUpdationDto input)
            => Result(await _organizationService.UpdateServiceBrandAsync(input, input.CompanyId));


        /// <summary>
        /// 删除服务品牌
        /// </summary>
        /// <param name="brandid">品牌ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/brand/{brandid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceBrandAsync([FromRoute]long brandid, [FromQuery]long companyid)
            =>Result(await _organizationService.DeleteServiceBrandAsync(brandid, companyid));

        /// <summary>
        /// 复制服务品牌
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("brand/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceBrandAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceBrandAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取服务品牌
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("brand/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceBrandDto>>> GetServiceBrandListAsync([FromQuery]long companyid)
            =>Result(await _organizationService.GetServiceBrandListAsync(companyid));


        /// <summary>
        /// 创建品牌内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/content")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateBrandContentAsync([FromBody]BrandContentCreationDto input)
            => CreatedResult(await _organizationService.CreateBrandContentAsync(input, input.CompanyId));


        /// <summary>
        /// 更新品牌内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/content")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateBrandContentAsync([FromBody]BrandContentUpdationDto input)
            => Result(await _organizationService.UpdateBrandContentAsync(input, input.CompanyId));

        /// <summary>
        /// 删除品牌内容
        /// </summary>
        /// <param name="contentid">品牌内容ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/content/{contentid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteBrandContentAsync([FromRoute]long contentid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteBrandContentAsync(contentid, companyid));

        /// <summary>
        /// 复制品牌内容
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("content/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyBrandContentAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyBrandContentAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取品牌内容
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("content/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<BrandContentDto>>> GetBrandContentListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetBrandContentListAsync(companyid));

        /// <summary>
        /// 创建结算代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/settlement")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateSettlementAsync([FromBody] SettlementCodeCreationDto input)
            => CreatedResult(await _organizationService.CreateSettlementAsync(input, input.CompanyId));

        /// <summary>
        /// 更新结算代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/settlement")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateSettlementAsync([FromBody]SettlementCodeUpdationDto input)
            => Result(await _organizationService.UpdateSettlementAsync(input, input.CompanyId));

        /// <summary>
        /// 删除结算代码
        /// </summary>
        /// <param name="settlementid">结算代码ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/settlement/{settlementid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteSettlementAsync([FromRoute]long settlementid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteSettlementAsync(settlementid, companyid));


        /// <summary>
        /// 复制结算代码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("settlement/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopySettlementAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopySettlementAsync(companyid, targetcompanyid));


        /// <summary>
        /// 获取结算代码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("settlement/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<SettlementCodeDto>>> GetSettlementListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetSettlementListAsync(companyid));


        /// <summary>
        /// 创建服务需求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/request")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceRequestAsync([FromBody]ServiceRequestCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceRequestAsync(input, input.CompanyId));

        /// <summary>
        /// 更新服务需求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/request")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceRequestAsync([FromBody]ServiceRequestUpdationDto input)
            => Result(await _organizationService.UpdateServiceRequestAsync(input, input.CompanyId));

        /// <summary>
        /// 删除服务需求
        /// </summary>
        /// <param name="requestid">服务需求ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/request/{requestid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceRequestAsync([FromRoute]long requestid, [FromQuery] long companyid)
            => Result(await _organizationService.DeleteServiceRequestAsync(requestid, companyid));

        /// <summary>
        /// 复制服务需求
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("request/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceRequestAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceRequestAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取服务请求
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("request/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceRequestDto>>> GetServiceRequestListAsync([FromQuery] long companyid)
            => Result(await _organizationService.GetServiceRequestListAsync(companyid));

        /// <summary>
        /// 创建故障原因
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/cause")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceCauseAsync([FromBody]ServiceCauseCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceCauseAsync(input, input.CompanyId));

        /// <summary>
        /// 更新故障原因
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/cause")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceCauseAsync([FromBody]ServiceCauseUpdationDto input)
            => Result(await _organizationService.UpdateServiceCauseAsync(input, input.CompanyId));


        /// <summary>
        /// 删除故障原因
        /// </summary>
        /// <param name="causeid">故障原因ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/cause/{causeid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceCauseAsync([FromRoute]long causeid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteServiceCauseAsync(causeid, companyid));

        /// <summary>
        /// 复制故障原因
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("cause/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceCauseAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceCauseAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取故障原因
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("cause/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceCauseDto>>> GetServiceCauseListAsync([FromQuery] long companyid)
            => Result(await _organizationService.GetServiceCauseListAsync(companyid));


        /// <summary>
        /// 创建服务回访编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/follow")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceFollowAsync([FromBody]ServiceFollowCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceFollowAsync(input, input.CompanyId));

        /// <summary>
        /// 更新服务回访编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/follow")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceFollowAsync([FromBody]ServiceFollowUpdationDto input)
            => Result(await _organizationService.UpdateServiceFollowAsync(input, input.CompanyId));

        /// <summary>
        /// 删除服务回访编码
        /// </summary>
        /// <param name="followid">服务回访ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/follow/{followid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceFollowAsync([FromRoute]long followid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteServiceFollowAsync(followid, companyid));

        /// <summary>
        /// 复制回访编码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("follow/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceFollowAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceFollowAsync(companyid, targetcompanyid));

        /// <summary>
        /// 获取服务回访编码
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("follow/list")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceFollowDto>>> GetServiceFollowListAsync([FromQuery]long companyid)
            => Result(await _organizationService.GetServiceFollowListAsync(companyid));

        /// <summary>
        /// 创建服务标注
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create/tag")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult<long>> CreateServiceTagAsync([FromBody] ServiceTagCreationDto input)
            => CreatedResult(await _organizationService.CreateServiceTagAsync(input, input.CompanyId));

        /// <summary>
        /// 更新服务标注
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/tag")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> UpdateServiceTagAsync([FromBody]ServiceTagUpdationDto input)
            => Result(await _organizationService.UpdateServiceTagAsync(input, input.CompanyId));


        /// <summary>
        /// 删除服务标注
        /// </summary>
        /// <param name="tagid">服务标注ID</param>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpDelete("remove/tag/{tagid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> DeleteServiceTagAsync([FromRoute]long tagid, [FromQuery]long companyid)
            => Result(await _organizationService.DeleteServiceTagAsync(tagid, companyid));

        /// <summary>
        /// 复制服务标注
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="targetcompanyid">目标公司ID</param>
        /// <returns></returns>
        [HttpDelete("tag/copy/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeSet)]
        public async Task<ActionResult> CopyServiceTagAsync([FromRoute]long companyid, [FromQuery]long targetcompanyid)
            => Result(await _organizationService.CopyServiceTagAsync(companyid, targetcompanyid));


        /// <summary>
        /// 获取服务标注
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        [HttpGet("tag/list")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<List<ServiceTagDto>>> GetServiceTagListAsync([FromQuery] long companyid)
            => Result(await _organizationService.GetServiceTagListAsync(companyid));

        [HttpGet("request/test/{companyid}")]
        [AdncAuthorize(PermissionConsts.AfterService.CodeQuery)]
        public async Task<ActionResult<ServiceRequestDto>> GetServiceRequestFirstTest([FromRoute] long companyid)
            =>Result(await _organizationService.GetServiceRequestFirstTest(companyid));

    }
}
