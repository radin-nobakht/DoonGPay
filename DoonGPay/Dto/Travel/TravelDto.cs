using DoonGPay.Entity.Travel;

namespace DoonGPay.Dto.Travel
{
    public class TravelDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Date { get; set; }

        public DateTime InsertDate { get; set; }
        public virtual ICollection<TravelCostDto> TravelCosts { get; set; }
        public virtual ICollection<TravelFriendDto> TravelFriends { get; set; }

        public int AllPerson => (TravelFriends != null && TravelFriends.Count > 0)
            ? TravelFriends.Sum(x => x.Person)
            : 0;
        public decimal AllCost => (TravelCosts != null && TravelCosts.Count > 0)
            ? TravelCosts.Sum(x => x.Value)
            : 0m;

    }
}
