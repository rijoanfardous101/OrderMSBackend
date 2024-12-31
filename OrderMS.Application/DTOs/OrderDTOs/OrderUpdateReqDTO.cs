using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.OrderDTOs
{
    public class OrderUpdateReqDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CustomerName { get; set; } = string.Empty;

        public string? CustomerPhone { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string CustomerAddress { get; set; } = string.Empty;

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
