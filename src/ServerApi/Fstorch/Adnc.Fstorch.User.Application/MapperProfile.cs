using Adnc.Fstorch.User.Application.Dtos.CardCase;
using Adnc.Fstorch.User.Application.Dtos.CashOut;
using Adnc.Fstorch.User.Application.Dtos.ReadDetail;
using AutoMapper;

namespace Adnc.Fstorch.User.Application
{
    public sealed class MapperProfile : Profile
    {

        public MapperProfile()
        {
            //映射配置，需要用到Map方法的映射都需要在这里进行统一注册，否则无法正常使用
            //CreateMap(传入的类，目标类)
            CreateMap<UserCreationAndUpdationDto, Da_userinfo>();
            CreateMap<Da_userinfo, UserDto>();
            CreateMap<UserUpdation, Da_userinfo>();
            CreateMap<UserCreation, Da_userinfo>();
            CreateMap<AccountCreationDto, Sys_Account>();
            CreateMap<AccountUpdationDto, Sys_Account>();
            CreateMap<AccountPhotoUpdationDto, Sys_Account>();
            CreateMap<Sys_Account, AccountDto>();
            CreateMap<AccountDto, Sys_Account>();
            CreateMap<Da_companyinfo, CompanyDto>();
            CreateMap<CompanyCreation, Da_companyinfo>();
            CreateMap<CompanyUpdation, Da_companyinfo>();
            CreateMap<CompanyDto, Da_companyinfo>();
            CreateMap<Da_cardinfo, CardCaseDto>();
            CreateMap<CardCaseDto, Da_cardinfo>();
            CreateMap<CardCaseCreationDto, Da_cardinfo>();
            CreateMap<CardCaseUpdationDto, Da_cardinfo>();
            CreateMap<Da_msg, MessageDto>();
            CreateMap<MessageCreationDto, Da_msg>();
            CreateMap<MessageUpdationDto, Da_msg>();
            CreateMap<MessageCreationAndUpdationDto, Da_msg>();
            CreateMap<CommentCreationDto, Da_Comments>();
            CreateMap<HomeDto, Da_HomeInfo>();
            CreateMap<Da_ReadDetail, ReadDetailDto>();
            CreateMap<ReadCreationDto, Da_ReadDetail>();
            CreateMap<Sys_Style, StyleDto>();
            CreateMap<Sys_BackGround, BackGroundDto>();
            CreateMap<HomeCreationDto, Da_HomeInfo>();
            CreateMap<MemberTypeCreationDto, Bm_Member_Type>();
            CreateMap<MemberTypeUpdationDto,  Bm_Member_Type>();
            CreateMap<Bm_Member_Type, MemberTypeDto>();
            CreateMap<GiveAwayCreationDto, Da_Card_Giveaway>();
            CreateMap<Da_Card_Giveaway, GiveAwayDto>();
            CreateMap<PayDetailCreationDto, Da_Pay_Detail>();
            CreateMap<Da_Pay_Detail, PayDetailDto>();
            CreateMap<CashOutCreationDto, Da_Cash_Out>();
        }
    }
}
