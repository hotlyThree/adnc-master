

using Fstorch.AfterSales.Application.Dtos.ServiceCause;
using Fstorch.AfterSales.Application.Dtos.ServiceFollow;
using Fstorch.AfterSales.Application.Dtos.ServiceOrderEngineer;
using Fstorch.AfterSales.Application.Dtos.ServiceOrderFollow;
using Fstorch.AfterSales.Application.Dtos.ServiceRequest;
using MongoDB.Bson;

namespace Fstorch.AfterSales.Application
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //bm_service_type
            CreateMap<ServiceTypeCreationDto, Bm_Service_Type>();
            CreateMap<ServiceTypeUpdationDto, Bm_Service_Type>();
            CreateMap<Bm_Service_Type, ServiceTypeDto>();

            //bm_service_nature
            CreateMap<ServiceNatureCreationDto, Bm_Service_Nature>();
            CreateMap<ServiceNatureUpdationDto, Bm_Service_Nature>();
            CreateMap<Bm_Service_Nature, ServiceNatureDto>();

            //bm_user_type
            CreateMap<UserTypeCreationDto, Bm_User_Type>();
            CreateMap<UserTypeUpdationDto, Bm_User_Type>();
            CreateMap<Bm_User_Type, UserTypeDto>();

            //bm_information_source
            CreateMap<InformationSourceCreationDto, Bm_Information_Source>();
            CreateMap<InformationSourceUpdationDto, Bm_Information_Source>();
            CreateMap<Bm_Information_Source, InformationSourceDto>();

            //bm_buy_market
            CreateMap<MarketCreationDto, Bm_Buy_Market>();
            CreateMap<MarketUpdationDto, Bm_Buy_Market>();
            CreateMap<Bm_Buy_Market, MarketDto>();

            //bm_service_brand
            CreateMap<ServiceBrandCreationDto, Bm_Service_Brand>();
            CreateMap<ServiceBrandUpdationDto, Bm_Service_Brand>();
            CreateMap<Bm_Service_Brand, ServiceBrandDto>();

            //bm_brand_content
            CreateMap<BrandContentCreationDto, Bm_Brand_Content>();
            CreateMap<BrandContentUpdationDto, Bm_Brand_Content>();
            CreateMap<Bm_Brand_Content, BrandContentDto>();

            //bm_settlement_code
            CreateMap<SettlementCodeCreationDto, Bm_Settlement_Code>();
            CreateMap<SettlementCodeUpdationDto, Bm_Settlement_Code>();
            CreateMap<Bm_Settlement_Code, SettlementCodeDto>();

            //bm_service_request
            CreateMap<ServiceRequestCreationDto, Bm_Service_Request>();
            CreateMap<ServiceRequestUpdationDto, Bm_Service_Request>();
            CreateMap<Bm_Service_Request, ServiceRequestDto>();

            //bm_service_cause
            CreateMap<ServiceCauseCreationDto, Bm_Service_Cause>();
            CreateMap<ServiceCauseUpdationDto, Bm_Service_Cause>();
            CreateMap<Bm_Service_Cause, ServiceCauseDto>();

            //bm_service_follow
            CreateMap<ServiceFollowCreationDto, Bm_Service_Follow>();
            CreateMap<ServiceFollowUpdationDto, Bm_Service_Follow>();
            CreateMap<Bm_Service_Follow, ServiceFollowDto>();

            //bm_service_tag
            CreateMap<ServiceTagCreationDto, Bm_Service_Tag>();
            CreateMap<ServiceTagUpdationDto, Bm_Service_Tag>();
            CreateMap<Bm_Service_Tag, ServiceTagDto>();

            //da_service_main
            CreateMap<ServiceMainCreationDto, Da_Service_Main>();
            CreateMap<ServiceMainUpdationDto, Da_Service_Main>();
            CreateMap<Da_Service_Main, ServiceMainDto>();

            //da_service_product
            CreateMap<ServiceProductCreationDto, Da_Service_Product>();
            CreateMap<ServiceProductUpdationDto, Da_Service_Product>();
            CreateMap<Da_Service_Product, ServiceProductDto>();

            //da_service_order_process
            CreateMap<ServiceOrderProcessCreationDto, Da_Service_Order_Process>();
            CreateMap<ServiceOrderProcessUpdationDto, Da_Service_Order_Process>();
            CreateMap<Da_Service_Order_Process, ServiceOrderProcessDto>();


            //da_service_order
            CreateMap<ServiceOrderCreationDto, Da_Service_Order>();
            CreateMap<ServiceOrderDispatchBatchDto, Da_Service_Order>();
            CreateMap<ServiceOrderUpdationDto, Da_Service_Order>();
            CreateMap<Da_Service_Order, ServiceOrderDto>();
            CreateMap<Da_Service_Order, ServiceOrderCreationDto>();

            //da_service_order_produc
            CreateMap<Da_Service_Order_Produc, ServiceOrderProductDto>();
            

            //da_service_order_cope
            CreateMap<ServiceOrderCopeCreationDto, Da_Service_Order_Cope>();
            CreateMap<ServiceOrderCopeUpdationDto, Da_Service_Order_Cope>();
            CreateMap<Da_Service_Order_Cope, ServiceOrderCopeDto>();

            //da_service_order_rec
            CreateMap<ServiceOrderRecCreationDto, Da_Service_Order_Rec>();
            CreateMap<ServiceOrderRecUpdationDto, Da_Service_Order_Rec>();
            CreateMap<Da_Service_Order_Rec, ServiceOrderRecDto>();

            //da_service_order_engineer
            CreateMap<ServiceOrderEngineerUpdationDto, Da_Service_Order_Engineer>();
            CreateMap<Da_Service_Order_Engineer, ServiceOrderEngineerDto>();

            //da_service_follow
            CreateMap<ServiceOrderFollowCreationDto, Da_Service_Followup>();
            CreateMap<Da_Service_Followup, ServiceOrderFollowDto>();

            //da_service_eng_grant
            CreateMap<ServiceOrderEngineerGrantCreationDto, Da_Service_Eng_Grant>();
            CreateMap<Da_Service_Eng_Grant, ServiceOrderEngineerGrantDto>();
        }
    }
}
