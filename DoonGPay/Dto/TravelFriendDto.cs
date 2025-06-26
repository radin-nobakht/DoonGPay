using DoonGPay.Entity;

namespace DoonGPay.Dto
{
    public class TravelFriendDto:BaseEntity<int>
    {
        public int TravelId { get; set; }
        public  string FristName { get; set; }
        public  string LastName { get; set; }
        public  string PhoneNumber { get; set; }
        public int Share { get; set; }
    }
}
