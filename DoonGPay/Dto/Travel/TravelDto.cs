using DoonGPay.Entity.Travel;

namespace DoonGPay.Dto.Travel
{
    public class TravelDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual ICollection<TravelCostDto> TravelCosts { get; set; }
        public virtual ICollection<TravelFriendDto> TravelFriends { get; set; }

    }
}
