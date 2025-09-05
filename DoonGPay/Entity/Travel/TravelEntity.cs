using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("Travel")]
    public class TravelEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
       
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime InsertDate { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        public virtual ICollection<TravelCostEntity> TravelCosts { get; set; }
        public virtual ICollection<TravelFriendEntity> TravelFriends { get; set; }
    }
}

