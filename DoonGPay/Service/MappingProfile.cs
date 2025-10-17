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
            CreateMap<UserDto, UserEntity>()
                .ForMember(dest => dest.TravelImage, opt => opt.Ignore());
            CreateMap<TravelDto, TravelEntity>().ReverseMap();
            CreateMap<TravelFriendDto, TravelFriendEntity>().ReverseMap();
            CreateMap<TravelCostDto, TravelCostEntity>().ReverseMap();

            CreateMap<TravelCostFriendDto, TravelCostFriendEntity>().ReverseMap();
            CreateMap<UsuallyFriendDto, TravelFriendDto>().ReverseMap();
            CreateMap<UsuallyFriendDto, UsuallyFrinedEntity>().ReverseMap();

        }
    }

}
