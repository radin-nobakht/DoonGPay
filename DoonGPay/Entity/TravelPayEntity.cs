using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("Pay")]
    public class TravelCostEntity
    {
        [Key]
        public int id {  get; set; }
        public string Reason { get; set; }
        public int Fare { get; set; }
    }
}
