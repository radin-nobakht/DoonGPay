using DoonGPay.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Dto.Travel
{
    public class TravelCostDto
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public int Type { get; set; }
        public string TypeStr { get; set; }

        public List<SelectListItem> CostTypes { get; set; }
    }
}
