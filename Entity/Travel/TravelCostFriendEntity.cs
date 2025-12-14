using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("TravelCostFriend")]
    public class TravelCostFriendEntity:BaseEntity<int>
    {
        public int TravelCostId { get; set; }
        public int TravelFriendId { get; set; }
        public int Rate { get; set; }
        public int Value { get; set; }
        [ForeignKey("TravelCostId")]
        public virtual TravelCostEntity TravelCost { get; set; }
        [ForeignKey("TravelFriendId")]
        public virtual TravelFriendEntity TravelFriend { get; set; }
       
    }
}
