using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface ItravelService
    {
        void AddFellowtraveler(TravelFellowtravelerDto  travelFellowtraveler);
        void Addtravel(TravelDto travel);
        void AddCost(TravelCostDto travelCost);
        void DeleteFellowtraveler(int id);
        void DeleteCost(int id);
        void Deletetravel(int id);
        TravelDto GetByIdTravel(int id);
        TravelFellowtravelerDto GetByIdFellowtraveler(int id);
        TravelCostDto GetByIdCost(int id);
        List<TravelFellowtravelerDto> Fellowtravelers();
        List<TravelCostDto> Costs();
        List<TravelDto> travels();
        void Updatetravel(TravelDto travel);
        void UpdateFellowtraveler(TravelFellowtravelerDto travelFellowtravelers);
        void UpdateCost(TravelCostDto travelCost);
    }
}