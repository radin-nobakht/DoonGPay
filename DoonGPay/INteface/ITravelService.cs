using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface ITravelService
    {
        void AddFellowTraveler(TravelFellowTravelerDto  travelFellowTraveler);
        void AddTravel(TravelDto travel);
        void AddPay(TravelCostDto travelPay);
        void DeleteFellowTraveler(int id);
        void DeletePay(int id);
        void DeleteTravel(int id);
        TravelDto GetById(int id);
        TravelFellowTravelerDto GetByIdFellowTraveler(int id);
        TravelCostDto GetByIdPay(int id);
        List<TravelFellowTravelerDto> ShowFellowTraveler();
        List<TravelCostDto> ShowPay();
        List<TravelDto> ShowTravel();
        void UpdateTravel(TravelDto travel);
        void UpdateFellowTraveler(TravelFellowTravelerDto TravelFellowTraveler);
        void UpdatePay(TravelCostDto travelPay);
    }
}