using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface IFriendTravelService
    {

        void AddFriend(TravelFriendDto travelFriends);
        void DeleteFriend(int id);
        TravelFriendDto Friend(int FriendId);
        List<TravelFriendDto> Friends_ShareByRow(int travelId);
        List<TravelFriendDto> Friends_ShareByPerson(int travelId);
            void UpdateFriend(TravelFriendDto travelFriends);
    }
}