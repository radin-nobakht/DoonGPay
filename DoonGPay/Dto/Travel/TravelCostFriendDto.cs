using DoonGPay.Entity;

namespace DoonGPay.Dto.Travel
{
    public class TravelCostFriendDto:BaseEntity<int>
    {
        public int CostId { get; set; }
        public int FriendId { get; set; }
        
        public string Value { get; set; }
        public virtual TravelFriendDto TravelFriend { get; set; }
        public virtual TravelCostDto TravelCost { get; set; }


        public string FriendName {  get; set; }
    }
}
