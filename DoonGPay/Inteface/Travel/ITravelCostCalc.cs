using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface ITravelCostCalc
    {
        void Calc(int travelCostId, int TravelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends);

    }
}
