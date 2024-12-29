using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.ProductDTOs
{
    public class ProductAddUpdateReqDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string ProductName { get; set; } = string.Empty;

        public string? ProductDescription { get; set; }

        public string? ProductImageUrl { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public float Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be greater than or equal to 0.")]
        public int StockQuantity { get; set; }
    }
}
