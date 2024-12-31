using Microsoft.EntityFrameworkCore;
using OrderMS.Application.DTOs.OrderDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.Domain.Enums;
using OrderMS.Persistence.DatabaseContext;

namespace OrderMS.Persistence.Services
{
    public class OrderService : IOrderRepository
    {
        private readonly OrderMSDbContext _dbContext;
        public OrderService(OrderMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderResDTO?> CreateOrderAsync(CreateOrderReqDTO reqDTO, Guid companyId)
        {
            // Create a new Customer for this Order.
            Customer customer = new Customer()
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = reqDTO.CustomerName,
                CustomerEmail = reqDTO.CustomerEmail,
                CustomerPhone = reqDTO.CustomerPhone,
                CustomerAddress = reqDTO.CustomerAddress,
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            // Create a new order
            Order order = new Order()
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                CompanyId = companyId,
                OrderStatus = OrderStatus.Processing.ToString(),
                PaymentStatus = PaymentStatus.Pending.ToString(),
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            float totalAmount = 0;

            foreach (var productItem in reqDTO.Items)
            {
                var product = await _dbContext.Products.FindAsync(productItem.ProductId);
                if (product != null && product.StockQuantity >= productItem.Quantity)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderItemId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                        UnitPrice = product.Price,
                        Quantity = productItem.Quantity,
                        Total = product.Price * productItem.Quantity,
                    };
                    order.Items.Add(orderItem);

                    totalAmount += orderItem.Total;
                    product.StockQuantity -= productItem.Quantity;
                }
            }

            if (!order.Items.Any())
                return null;

            order.TotalAmount = totalAmount;

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync(); // Saves order, items, and customer in a single transaction

            return ToOrderResDTO(order);
        }

        public static OrderResDTO ToOrderResDTO(Order order)
        {
            return new OrderResDTO()
            {
                OrderId = order.OrderId,
                CompanyId = order.CompanyId,
                CustomerId = order.CustomerId,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                Items = ToOrderItemResDTOs(order.Items),
                Customer = ToCustomerResDTO(order.Customer),
            };
        }

        public static List<OrderItemResDTO> ToOrderItemResDTOs(List<OrderItem> items)
        {
            List<OrderItemResDTO> orderItems = new List<OrderItemResDTO>();
            foreach (var item in items)
            {
                orderItems.Add(ToOrderItemResDTO(item));
            }
            return orderItems;
        }

        public static OrderItemResDTO ToOrderItemResDTO(OrderItem item)
        {
            return new OrderItemResDTO()
            {
                OrderItemId = item.OrderItemId,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Total = item.Total,
            };
        }

        public static CustomerResDTO ToCustomerResDTO(Customer customer)
        {
            return new CustomerResDTO()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress,
            };
        }

        public async Task<OrderResDTO?> GetOrderAsync(Guid orderId)
        {
            var order = await _dbContext.Orders.Include(o => o.Customer).Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                return null;

            return ToOrderResDTO(order);
        }

        public async Task<List<OrderResDTO>> GetAllOrderAsync(Guid companyId)
        {
            var allOrders = await _dbContext.Orders.Include(o => o.Customer).Include(o => o.Items).Where(o => o.CompanyId == companyId).ToListAsync();

            var allOrdersDTO = new List<OrderResDTO>();

            foreach (var o in allOrders)
            {
                allOrdersDTO.Add(ToOrderResDTO(o));
            }

            return allOrdersDTO;
        }

        public async Task<OrderResDTO?> UpdateOrderAsync(OrderUpdateReqDTO reqDTO, Guid orderId, Guid companyId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null || order.CompanyId != companyId)
                return null;

            if (reqDTO.OrderStatus != null && (reqDTO.OrderStatus.Equals(OrderStatus.Processing.ToString())
                || reqDTO.OrderStatus.Equals(OrderStatus.Completed.ToString())
                || reqDTO.OrderStatus.Equals(OrderStatus.Cancelled.ToString())))
            {
                order.OrderStatus = reqDTO.OrderStatus;
            }

            if (reqDTO.PaymentStatus != null && (reqDTO.PaymentStatus.Equals(PaymentStatus.Pending.ToString())
                || reqDTO.PaymentStatus.Equals(PaymentStatus.Paid.ToString())))
            {
                order.PaymentStatus = reqDTO.PaymentStatus;
            }

            var customer = await _dbContext.Customers.FindAsync(order.CustomerId);

            if (customer == null) return null;

            customer.CustomerName = reqDTO.CustomerName;
            customer.CustomerAddress = reqDTO.CustomerAddress;
            customer.CustomerPhone = reqDTO.CustomerPhone;

            await _dbContext.SaveChangesAsync();

            return await GetOrderAsync(orderId);
        }
    }
}
