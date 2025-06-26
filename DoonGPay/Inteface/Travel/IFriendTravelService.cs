using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface IFriendTravelService
    {

        void AddFriend(TravelFriendDto travelFriends);
        void DeleteFriend(int id);
        TravelFriendDto Friend(int FriendId);
        List<TravelFriendDto> Friends(int travelId);
        List<TravelFriendDto> FriendsbyPerson(int travelId);
            void UpdateFriend(TravelFriendDto travelFriends);
    }
}