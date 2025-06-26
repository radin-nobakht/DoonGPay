using DoonGPay.Entity;
using System.Security.Cryptography;

namespace DoonGPay.Dto.Travel
{
    public class TravelFriendDto:BaseEntity<int>
    {
        public int TravelId { get; set; }
        public  string FristName { get; set; }
        public  string LastName { get; set; }
        public  string PhoneNumber { get; set; }
        public int Person {  get; set; }
        public decimal Share { get; set; }
    }
}
