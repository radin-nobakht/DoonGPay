using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using System.Globalization;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Inteface.Travel
{
    public interface ITravelService
    {
        void DeleteUsuallyFriend(int id);
        void SaveUsualyFriend(UsuallyFriendDto usuallyFriend);
        List<UsuallyFriendDto> UsuallyFriends();
        UsuallyFriendDto UsuallyFriend(int usuallyFriendId);
        string ToPersianDateString( DateTime date);
        void SaveTravelFriend(TravelFriendDto travelFriends);
        void SaveTravel(TravelDto travel);
        List<TravelDto> AllTravel(int travelId);
        void DeleteTravelCost(int id);
        void DeleteTravel(int id);
        void DeleteTravelFriend(int id);
        TravelFriendDto TravelFriend(int FriendId);

        List<TravelFriendDto> TravelFriends(int travelId);
        TravelDto Travel(int? id);
        TravelCostDto TravelCost(int? travelCostId, int travelId);
        List<TravelCostDto> TravelCosts(int travelId);
        List<TravelDto> Travels();
        IBaseResult SaveTravelCost(TravelCostDto travelCost);
        
    }
}