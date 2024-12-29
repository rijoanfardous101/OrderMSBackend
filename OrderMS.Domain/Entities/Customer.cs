
using System.ComponentModel.DataAnnotations;

namespace OrderMS.Domain.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 4)]
        public string CustomerAddress { get; set; }

        public virtual List<Order> Orders { get; set; }

    }
}
