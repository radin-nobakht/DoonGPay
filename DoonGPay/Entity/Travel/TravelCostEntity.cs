using DoonGPay.Dto.Travel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("TravelCost")]
    public class TravelCostEntity : BaseEntity<int>
    {
        public int TravelId { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public int Type { get; set; }
        public string? Description { get; set; }
        [ForeignKey("TravelId")]
        public virtual TravelEntity Travel { get; set; }
        public virtual ICollection<TravelCostFriendEntity> TravelCostFriends { get; set; }

    }
}


