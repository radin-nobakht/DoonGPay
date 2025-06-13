using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface ItravelService
    {
        void AddFellowtraveler(TravelFellowtravelerDto  travelFellowtraveler);
        void Addtravel(TravelDto travel);
        void AddPay(TravelCostDto travelCost);
        void DeleteFellowtraveler(int id);
        void DeletePay(int id);
        void Deletetravel(int id);
        TravelDto GetById(int id);
        TravelFellowtravelerDto GetByIdFellowtraveler(int id);
        TravelCostDto GetByIdPay(int id);
        List<TravelFellowtravelerDto> Fellowtravelers();
        List<TravelCostDto> Pays();
        List<TravelDto> travels();
        void Updatetravel(TravelDto travel);
        void UpdateFellowtraveler(TravelFellowtravelerDto travelFellowtravelers);
        void UpdatePay(TravelCostDto travelCost);
    }
}