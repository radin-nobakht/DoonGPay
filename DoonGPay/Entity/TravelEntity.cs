using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("travel")]
    public class TravelEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}
