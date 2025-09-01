using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("UsallyFriend")]
    public class UsuallyFrinedEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
