using System.ComponentModel.DataAnnotations;

namespace DoonGPay.Entity
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id {  get; set; }
    }
}
