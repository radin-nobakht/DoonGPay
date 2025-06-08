using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("mainTable")]
    public class MainTableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Share { get; set; }
    }
}
