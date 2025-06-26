using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("TravelFriend")]
    public class TravelFriendEntity : BaseEntity<int>
    {
        public int TravelId {  get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Share { get; set; }
    }
}
