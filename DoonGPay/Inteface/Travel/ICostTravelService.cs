using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface ICostTravelService
    {
        void AddCost(TravelCostDto travelCost);
        void DeleteCost(int id);
        TravelCostDto GetByIdCost(int id);
        TravelCostDto TravelCost(int travelCostId);
        List<TravelCostDto> TravelCosts(int travelId);
        void UpdateCost(TravelCostDto travelCost);
     
    }
}