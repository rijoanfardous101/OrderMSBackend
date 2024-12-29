using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderMS.Domain.Enums;

namespace OrderMS.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public float TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }


        public virtual Company Company { get; set; }
        

        public virtual Customer Customer { get; set; }

        public virtual List<OrderItem> Items { get; set; }

    }
}
