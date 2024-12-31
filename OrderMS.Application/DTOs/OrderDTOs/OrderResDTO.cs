using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMS.Domain.Enums;

namespace OrderMS.Application.DTOs.OrderDTOs
{
    public class OrderResDTO
    {
        public Guid OrderId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItemResDTO> Items { get; set; }
        public CustomerResDTO Customer { get; set; }
    }
}
