using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Application.DTOs.ProductDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.WebAPI.Controllers.V1.Base;

namespace OrderMS.WebAPI.Controllers.V1
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductRepository _productRepository;
        
        public ProductController(UserManager<ApplicationUser> userManager, IProductRepository productRepository) 
        {
            _userManager = userManager;
            _productRepository = productRepository;
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddUpdateReqDTO reqDTO)
        {
            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var companyId = User.FindFirst("CompanyId")?.Value;
            if(companyId == null)
                return Unauthorized();

            var product = await _productRepository.AddProductAsync(reqDTO, Guid.Parse(companyId));

            if (product == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetProduct), new { productId = product.ProductId }, product);
        }


        [HttpGet]
        [Route("{productId:Guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid productId)
        {
            var res = await _productRepository.GetProductByIdAsync(productId);

            if (res == null)
                return NotFound();

            return Ok(res);
        }
        

        [HttpGet]
        [Route("AllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var companyId = User.FindFirst("CompanyId")?.Value;
            if (companyId == null)
                return NotFound();

            var res = await _productRepository.GetAllProductsAsync(Guid.Parse(companyId));

            if (res == null)
                return Ok(new List<ProductResDTO>());

            return Ok(res);
        }


        [HttpPut]
        [Route("Update/{productId:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductAddUpdateReqDTO reqDTO, [FromRoute] Guid productId)
        {
            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var companyId = User.FindFirst("CompanyId")?.Value;
            if (companyId == null)
                return NotFound();

            var res = await _productRepository.UpdateProductAsync(reqDTO, productId, Guid.Parse(companyId));

            if (res == null)
                return BadRequest();

            return Ok(res);
        }
    }
}
