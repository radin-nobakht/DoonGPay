using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("TravelCost")]
    public class TravelCostEntity
    {
        [Key]
        public int id {  get; set; }
        public string Reason { get; set; }
        public int Cost { get; set; }
    }
}
