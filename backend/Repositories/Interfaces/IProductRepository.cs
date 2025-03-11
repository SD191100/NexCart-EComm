using NexCart.DTOs.Sales;
using NexCart.Models;
using System.Threading.Tasks;

namespace NexCart.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(int productId);
        IEnumerable<Product> GetAllProducts();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);

        IEnumerable<Product> GetProducts(string? searchQuery = null, int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null,
                                      string sortOption = null, int page = 1, int pageSize = 10);
        //Product GetProductByIdNew(int productId);
        Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId);
        Task<IEnumerable<SalesReportDTO>> GetSalesReportAsync(int sellerId, DateTime startDate, DateTime endDate);
        Task<AnalyticsDTO> GetSellerAnalyticsAsync(int sellerId, DateTime startDate, DateTime endDate);
    }
}
