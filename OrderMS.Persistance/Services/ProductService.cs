using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.Design;
using OrderMS.Application.DTOs.ProductDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace OrderMS.Persistence.Services
{
    public class ProductService : IProductRepository
    {
        private readonly OrderMSDbContext _dbContext;

        public ProductService(OrderMSDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<ProductResDTO?> AddProductAsync(ProductAddUpdateReqDTO reqDTO, Guid companyId)
        {
            Product product = new Product()
            {
                ProductId = Guid.NewGuid(),
                CompanyId = companyId,
                ProductName = reqDTO.ProductName,
                ProductDescription = reqDTO.ProductDescription,
                Price = reqDTO.Price,
                StockQuantity = reqDTO.StockQuantity,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return await GetProductByIdAsync(product.ProductId);
        }

        public async Task<ProductResDTO?> GetProductByIdAsync(Guid productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
                return null;

            return ToProductResDTO(product);
        }

        public async Task<List<ProductResDTO>?> GetAllProductsAsync(Guid companyId)
        {
            var products = await _dbContext.Products.Where(p => p.CompanyId.Equals(companyId)).ToListAsync();

            if (products.Count == 0)
                return null;

            List<ProductResDTO> productsRes = new List<ProductResDTO>();

            foreach (var product in products)
            {
                productsRes.Add(ToProductResDTO(product));
            }

            return productsRes;
        }

        public async Task<ProductResDTO?> UpdateProductAsync(ProductAddUpdateReqDTO reqDTO, Guid productId, Guid companyId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null || product.CompanyId != companyId)
                return null;

            product.ProductName = reqDTO.ProductName;
            product.ProductDescription = reqDTO.ProductDescription;
            product.Price = reqDTO.Price;
            product.StockQuantity = reqDTO.StockQuantity;

            //_dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return ToProductResDTO(product);
        }

        public static ProductResDTO ToProductResDTO(Product product)
        {
            return new ProductResDTO()
            {
                ProductId = product.ProductId,
                CompanyId = product.CompanyId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedAt = product.CreatedAt,
            };
        }

    }
}
