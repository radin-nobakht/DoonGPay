using DoonGPay.Entity;

namespace DoonGPay.Dto.Travel
{
    public class TravelCostDto: BaseEntity<int>
    {
       
        public int TravelId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
    }
}
