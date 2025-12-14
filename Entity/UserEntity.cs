using DoonGPay.Entity.Travel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("User")]
    public class UserEntity : BaseEntity<int>
    {
        public required string UserName { get; set; }

        [MaxLength(50)]
        public required string FristName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        [MaxLength(11)]
        public required string Password { get; set; }
        public string UserAvatarStr { get; set; }
        public byte[] TravelImage { get; set; }
        public required string PhoneNumber { get; set; }
        public virtual ICollection<TravelEntity> Travels { get; set; }
        public virtual ICollection<UsuallyFrinedEntity> UsuallyFrineds { get; set; }

    }
}
