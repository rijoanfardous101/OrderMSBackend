using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Application.DTOs.OrderDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.WebAPI.Controllers.V1.Base;

namespace OrderMS.WebAPI.Controllers.V1
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public OrderController(UserManager<ApplicationUser> userManager, IOrderRepository orderRepository) 
        { 
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderReqDTO orderReq)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if(orderReq.Items.Count <= 0)
            {
                ModelState.AddModelError("Items", "Please add at least 1 item.");
                return ValidationProblem(ModelState);
            }

            var companyId = User.FindFirst("CompanyId")?.Value ?? throw new ArgumentNullException();

            var order = await _orderRepository.CreateOrderAsync(orderReq, Guid.Parse(companyId));

            if (order == null)
                return BadRequest("");

            return Ok(order);
        }

        [HttpGet]
        [Route("{orderId:Guid}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        [Route("AllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var companyId = User.FindFirst("CompanyId")?.Value ?? throw new ArgumentNullException();

            var res = await _orderRepository.GetAllOrderAsync(Guid.Parse(companyId));

            return Ok(res);
        }

        [HttpPut]
        [Route("Update/{orderId:Guid}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateReqDTO reqDTO, [FromRoute] Guid orderId)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var companyId = User.FindFirst("CompanyId")?.Value ?? throw new ArgumentNullException();

            var order = await _orderRepository.UpdateOrderAsync(reqDTO, orderId, Guid.Parse(companyId));

            if (order == null)
                return BadRequest("");

            return Ok(order);
        }
    }
}
