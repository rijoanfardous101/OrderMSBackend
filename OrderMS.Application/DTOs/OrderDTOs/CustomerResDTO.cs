using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.OrderDTOs
{
    public class CustomerResDTO
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        public string CustomerAddress { get; set; }
    }
}
