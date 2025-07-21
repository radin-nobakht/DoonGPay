using DoonGPay.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Dto.Travel
{
    public class TravelCostDto
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public int Type { get; set; }
        public string TypeStr { get; set; }
        public virtual TravelDto Travel { get; set; }
        public List<SelectListItem> CostTypes { get; set; }
        public virtual ICollection<TravelCostFriendDto> TravelCostFriends { get; set; }

    }
}
