using AutoMapper;
using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.Service
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UsersEntity>().ReverseMap();
            CreateMap<TravelDto, TravelEntity>().ReverseMap();
            CreateMap<TravelFellowTravelerDto, TravelFellowTravelerEntity>().ReverseMap();
            CreateMap<TravelCostDto, TravelCostEntity>().ReverseMap();

        }
}
}
