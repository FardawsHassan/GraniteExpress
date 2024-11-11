using AutoMapper;
using GraniteExpress.DtoModels;
using GraniteExpress.Models;

namespace CarerHub.Services.ModelMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<AccountTypeDto, AccountType>().ReverseMap();
            CreateMap<CashFlowDto, CashFlow>().ReverseMap();
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<ClientypeDto, Clientype>().ReverseMap();
            CreateMap<CurrencyDto, Currency>().ReverseMap();
            CreateMap<DocumentTypeDto, DocumentType>().ReverseMap();
            CreateMap<JournalDetailDto, JournalDetail>().ReverseMap();
            CreateMap<JournalDto, Journal>().ReverseMap();
            //CreateMap<JournalDto, Journal>().ForMember(dest => dest.User, opt => opt.Ignore());
            //CreateMap<Journal, JournalDto>().ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<TemplateDetailsDto, TemplateDetails>().ReverseMap();
            CreateMap<TemplateDto, Template>().ReverseMap();
            //CreateMap<User, UserDto>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //    .ForMember(dest => dest.UserRole, opt => opt.Ignore())
            //    .ForMember(dest => dest.Claims, opt => opt.Ignore());

            //CreateMap<UserDto, User>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}