using DoonGPay.Dto.Travel;

namespace DoonGPay.Models
{
    public class EditCostViewModel
    {
        public TravelCostDto TravelCost { get; set; }
        public List<TravelCostFriendDto> TravelCostFriend { get; set; }
        public List<TravelFriendDto> TravelFriend { get; set; }
    }
}
