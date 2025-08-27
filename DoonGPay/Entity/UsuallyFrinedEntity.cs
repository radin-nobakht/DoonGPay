using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("UsallyFriends")]
    public class UsuallyFrinedEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phonenumber { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
