using NexCart.DTOs.Product;
using NexCart.DTOs.Sales;
using NexCart.Models;
using NexCart.Repositories.Implementations;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public void AddProduct(AddProductReq product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                SellerId = product.SellerId,
                MainImage = product.MainImage,
                SecondImage = product.SecondImage,
                ThirdImage = product.ThirdImage,
                Details = product.Details
            };
            _productRepository.Add(newProduct);
        }

        public void UpdateProduct(UpdateProductReq product)
        {
            var oldProduct = _productRepository.GetProductById(product.Id);
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Stock = product.Stock;
            _productRepository.Update(oldProduct);
        }

        public void DeleteProduct(int productId)
        {
            _productRepository.Delete(productId);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsBySellerAsync(int sellerId)
        {
            var products = await _productRepository.GetProductsBySellerAsync(sellerId);
            return products.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Stock,
                Description = p.Description
            });
        }

        public async Task<IEnumerable<SalesReportDTO>> GenerateSalesReportAsync(int sellerId, DateTime startDate, DateTime endDate)
        {
            return await _productRepository.GetSalesReportAsync(sellerId, startDate, endDate);
        }

        public async Task<AnalyticsDTO> GetSellerAnalyticsAsync(int sellerId, DateTime startDate, DateTime endDate)
        {
            return await _productRepository.GetSellerAnalyticsAsync(sellerId, startDate, endDate);
        }

        public void UpdateProduct(UpdateStockDTO stock)
        {
            var oldProduct = _productRepository.GetProductById(stock.ProductId);
            oldProduct.Stock -=  stock.Stock;
            _productRepository.Update(oldProduct);
        }
    }
    
}
