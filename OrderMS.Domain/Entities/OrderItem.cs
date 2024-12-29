
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMS.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Total {  get; set; }


        public virtual Order Order { get; set; }


        public virtual Product Product { get; set; }

    }
}
