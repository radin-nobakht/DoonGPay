using DoonGPay.Dto.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Inteface.Travel
{
    public interface ITravelService
    {
   
        void SaveTravelFriend(TravelFriendDto travelFriends);
        void SaveTravel(TravelDto travel);
        void SaveTravelCostFriend(TravelCostFriendDto travelCostFriend);
        List<TravelDto> AllTravel(int travelId);
        void DeleteTravelCost(int id);
        void DeleteTravel(int id);
        void DeleteTravelFriend(int id);
        TravelFriendDto TravelFriend(int FriendId);

        List<TravelFriendDto> TravelFriends(int travelId);
        TravelDto Travel(int id);
        TravelCostDto TravelCost(int? travelCostId, int travelId);
        List<TravelCostFriendDto> TravelCostFriends(int CostId);
        List<TravelCostDto> TravelCosts(int travelId);
        List<TravelDto> Travels();
        void SaveTravelCost(TravelCostDto travelCost);
        
    }
}