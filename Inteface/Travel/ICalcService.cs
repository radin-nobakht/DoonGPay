using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface ICalcService
    {
        void SaveTravelCostsFriend(TravelCostFriendDto travelCostFriend);
        decimal CostValue(int id);
        List<TravelFriendDto> Friends(int travelId);
        List<TravelCostFriendDto> GetTravelFriendCost(int travelCostId);
        int Persons(int travelId);
    }
}