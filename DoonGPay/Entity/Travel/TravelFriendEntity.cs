using DoonGPay.Dto.Travel;
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
        public int Person {  get; set; }
        public decimal? Share { get; set; }
        [ForeignKey("TravelId")]
        public virtual TravelEntity Travel { get; set; }
        [NotMapped]
        public virtual ICollection<TravelCostFriendDto> CostFriend { get; set; }

    }
}

