using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("TravelCost")]
    public class TravelCostEntity : BaseEntity<int>
    {
        public int TravelId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public string? Description { get; set; }
    }
}
