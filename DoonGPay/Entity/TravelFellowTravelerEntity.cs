using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("travelFellowtraveler")]
    public class TravelFellowtravelerEntity : BaseEntity<int>
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
