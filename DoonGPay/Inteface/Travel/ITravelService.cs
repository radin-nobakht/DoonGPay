using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface ITravelService
    {
        void AddCost(TravelCostDto travelCost);
        void AddFriend(TravelFriendDto travelFriends);
        void Addtravel(TravelDto travel);
        void DeleteCost(int id);
        void DeleteFriend(int id);
        void Deletetravel(int id);
        TravelFriendDto Friend(int FriendId);
        List<TravelFriendDto> Friends(int travelId);
        TravelCostDto GetByIdCost(int id);
        TravelDto Travel(int id);
        TravelCostDto TravelCost(int travelCostId);
        List<TravelCostDto> TravelCosts(int travelId);
        List<TravelDto> Travels();
        void UpdateCost(TravelCostDto travelCost);
        void UpdateFriend(TravelFriendDto travelFriends);
        void Updatetravel(TravelDto travel);
    }
}