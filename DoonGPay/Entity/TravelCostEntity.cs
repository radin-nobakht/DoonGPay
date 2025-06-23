using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoonGPay.Entity
{
    [Table("TravelCost")]
    public class TravelCostEntity : BaseEntity<int>
    {

        public string Title { get; set; }
        public int Value { get; set; }
    }
}
