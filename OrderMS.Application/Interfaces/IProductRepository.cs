using OrderMS.Application.DTOs.ProductDTOs;

namespace OrderMS.Application.Interfaces
{
    public interface IProductRepository
    {
        public Task<ProductResDTO?> AddProductAsync(ProductAddUpdateReqDTO reqDTO, Guid companyId);
        public Task<ProductResDTO?> GetProductByIdAsync(Guid productId);
        public Task<List<ProductResDTO>?> GetAllProductsAsync(Guid compandId);
        public Task<ProductResDTO?> UpdateProductAsync(ProductAddUpdateReqDTO reqDTO, Guid productId, Guid companyId);
    }
}
