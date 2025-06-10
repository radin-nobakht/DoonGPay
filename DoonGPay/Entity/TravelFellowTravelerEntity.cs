using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("TravelFellowTraveler")]
    public class TravelFellowTravelerEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public int PhoneNumber { get; set; }
        public int Share { get; set; }

    }
}
