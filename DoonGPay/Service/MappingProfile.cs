using AutoMapper;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity;
using DoonGPay.Entity.Travel;

namespace DoonGPay.Service
{
   public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, UserEntity>().ReverseMap();
        CreateMap<TravelDto, TravelEntity>().ReverseMap();
        CreateMap<TravelFriendDto, TravelFriendEntity>().ReverseMap();
        CreateMap<TravelCostFriendDto, TravelCostFriendEntity>().ReverseMap();

        CreateMap<TravelCostDto, TravelCostEntity>()
            .ForMember(dest => dest.TravelCostFriends, opt => opt.MapFrom(src => src.CostFriend))
            .ForMember(dest => dest.Travel, opt => opt.MapFrom(src => src.Travel))
            // اینجا **نباید** ForMember برای CostTypes باشه چون در TravelCostEntity وجود نداره
            .ForSourceMember(src => src.CostTypes, opt => opt.DoNotValidate()); // نادیده گرفتن پراپرتی CostTypes در سمت DTO

        CreateMap<TravelCostEntity, TravelCostDto>()
            .ForMember(dest => dest.CostTypes, opt => opt.Ignore());
    }
}

}
