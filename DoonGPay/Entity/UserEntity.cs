using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("User")]
    public class UserEntity : BaseEntity<int>
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        [MaxLength(11)]
        [MinLength(11)]
        public required string PhoneNumber { get; set; }
    }
}
