using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("travelFellowtraveler")]
    public class TravelFellowtravelerEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
