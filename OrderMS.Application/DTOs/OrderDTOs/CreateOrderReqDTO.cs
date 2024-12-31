using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMS.Domain.Entities;
using OrderMS.Domain.Enums;

namespace OrderMS.Application.DTOs.OrderDTOs
{
    public class CreateOrderReqDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string CustomerAddress { get; set; } = string.Empty;

        [Required]
        public List<OrderItemsAddDTO> Items { get; set; }
    }
}
