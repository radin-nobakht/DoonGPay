using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity.Travel
{
    [Table("travel")]
    public class TravelEntity :BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime InsertDate { get; set; }
         
    }
}
