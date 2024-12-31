
using OrderMS.Application.DTOs.OrderDTOs;

namespace OrderMS.Application.Interfaces
{
    public interface IOrderRepository
    {
        public Task<OrderResDTO?> CreateOrderAsync(CreateOrderReqDTO reqDTO, Guid companyId);

        public Task<OrderResDTO?> GetOrderAsync(Guid orderId);
        public Task<List<OrderResDTO>> GetAllOrderAsync(Guid companyId);

        public Task<OrderResDTO?> UpdateOrderAsync(OrderUpdateReqDTO reqDTO, Guid orderId, Guid companyId);
    }
}
