using AutoMapper;
using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.Service
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<TravelDto,TravelEntity >().ReverseMap();
            CreateMap<TravelFriendDto, TravelFriendEntity>().ReverseMap();
            CreateMap<TravelCostDto, TravelCostEntity>().ReverseMap();

        }
}
}
