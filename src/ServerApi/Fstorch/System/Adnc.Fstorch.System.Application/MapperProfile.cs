
using Adnc.Fstorch.System.Application.Dtos.CodeBase;
using Adnc.Fstorch.System.Application.Dtos.Group;
using Adnc.Fstorch.System.Application.Dtos.Other;
using Adnc.Fstorch.System.Application.Dtos.Setting;
using Adnc.Fstorch.System.Repository.Entities;
using AutoMapper;

namespace Adnc.Fstorch.System.Application
{
    public sealed class MapperProfile : Profile
    {

        public MapperProfile()
        {
            //映射配置，需要用到Map方法的映射都需要在这里进行统一注册，否则无法正常使用
            //CreateMap(传入的类，目标类)
            CreateMap<Bm_DepartMentDto, Bm_DepartMent>();
            CreateMap<Bm_PostDto, Bm_Post>();
            CreateMap<Bm_Public_BaseDto, Bm_Public_Base>();
            CreateMap<Bm_Public_PersonnelDto, Bm_Public_Personnel>();
            CreateMap<Bm_Service_ProcessDto, Bm_Service_Process>();
            CreateMap<Bm_Service_SpecificationsDto, Bm_Service_Specifications>();
            CreateMap<Bm_Relation_ComparisonDto, Bm_Relation_Comparison>();
            CreateMap<Bm_Sms_TemplateDto, Bm_Sms_Template>();
            CreateMap<Bm_WX_TemplateDto, Bm_WX_Template>();
            CreateMap<GroupInfoDto, GroupInfo>();
            CreateMap<GroupMenuInfoDto, GroupMenuInfo>();
            CreateMap<GroupUserInfoDto, GroupUserInfo>();
            CreateMap<GroupWareHouseInfoDto, GroupWareHouseInfo>();
            CreateMap<Da_Modify_HistoryDto, Da_Modify_History>();
            CreateMap<Da_Print_TemplateDto, Da_Print_Template>();
            CreateMap<Da_StaffDto, Da_Staff>();
            CreateMap<SystemCompanyInfoDto, SystemCompanyInfo>();
        }
    }
}
