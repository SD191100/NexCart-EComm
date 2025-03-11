using NexCart.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using NexCart.Data;
using NexCart.DTOs.Product;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using NexCart.DTOs.Sales;

namespace NexCart.Services.Interfaces
{
    public interface IProductService
    {
        Product GetProductById(int productId);
        IEnumerable<Product> GetAllProducts();
        void AddProduct(AddProductReq product);
        void UpdateProduct(UpdateProductReq product);
        void UpdateProduct(UpdateStockDTO stock);
        void DeleteProduct(int productId);

        Task<IEnumerable<ProductResponseDTO>> GetProductsBySellerAsync(int sellerId);
        Task<IEnumerable<SalesReportDTO>> GenerateSalesReportAsync(int sellerId, DateTime startDate, DateTime endDate);
        Task<AnalyticsDTO> GetSellerAnalyticsAsync(int sellerId, DateTime startDate, DateTime endDate);
    }
}