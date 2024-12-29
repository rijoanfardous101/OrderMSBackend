using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMS.Domain.Entities;

namespace OrderMS.Application.DTOs.ProductDTOs
{
    public class ProductResDTO
    {
        public Guid ProductId { get; set; }

        public Guid CompanyId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? ProductDescription { get; set; }

        public string? ProductImageUrl { get; set; }


        public float Price { get; set; }


        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
