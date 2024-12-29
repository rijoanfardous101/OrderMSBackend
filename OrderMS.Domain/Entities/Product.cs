
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OrderMS.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        public Guid CompanyId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 4)]
        public string ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductImageUrl { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; }


        public virtual Company Company { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

    }
}
