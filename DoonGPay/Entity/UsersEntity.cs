using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("Users")]
    public class UsersEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LName  { get; set; }
        public int PhoneNumber { get; set; }
    }
}
