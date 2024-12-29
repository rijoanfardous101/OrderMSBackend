
using System.ComponentModel.DataAnnotations;

namespace OrderMS.Domain.Entities
{
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string CompanyName { get; set; }
        public string? CompanyDescription { get; set; }

        [StringLength(255)]
        public string? CompanyAddress { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? PicturePath { get; set; }

        public virtual List<ApplicationUser> Users { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
