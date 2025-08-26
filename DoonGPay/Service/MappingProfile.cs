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

            CreateMap<TravelCostDto, TravelCostEntity>().ReverseMap();
            CreateMap<UsuallyFriendDto, UsuallyFrinedEntity>().ReverseMap();

        }
    }

}
