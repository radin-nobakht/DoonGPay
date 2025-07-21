using DoonGPay.Entity;

namespace DoonGPay.Dto.Travel
{
    public class TravelCostFriendDto
    {
        public int Id { get; set; }
        public int TravelCostId { get; set; }
        public int TravelFriendId { get; set; }

        public int Rate { get; set; }
        public decimal Value { get; set; }
        public virtual TravelFriendDto TravelFriend { get; set; }
        public virtual TravelCostDto TravelCost { get; set; }


        public string FriendName {  get; set; }
    }
}
