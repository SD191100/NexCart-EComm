using Microsoft.EntityFrameworkCore;
using NexCart.Data;
using NexCart.DTOs.Sales;
using NexCart.Models;
using NexCart.Repositories.Interfaces;

namespace NexCart.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly NexCartDBContext _context;

        public ProductRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int productId)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProducts(string? searchQuery = null, int? categoryId = null, decimal? minPrice = null,
                                             decimal? maxPrice = null, string sortOption = null, int page = 1, int pageSize = 10)
        {
            var products = _context.Products.AsQueryable();
            // Full-text search
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery) ||
                                               p.Description.Contains(searchQuery) ||
                                               p.Category.Name.Contains(searchQuery));
            }

            // Filter by category
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            // Sort products
            products = sortOption switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "newest" => products.OrderByDescending(p => p.ProductId), // Assuming ProductId increases over time
                "popularity" => products.OrderByDescending(p => p.Stock), // Example of popularity
                _ => products // Default sort
            };

            // Pagination
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            return products.ToList();
        }
        public async Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId)
        {
            return await _context.Products.Where(p => p.SellerId == sellerId).ToListAsync();
        }

        public async Task<IEnumerable<SalesReportDTO>> GetSalesReportAsync(int sellerId, DateTime startDate, DateTime endDate)
        {
            return await _context.Products
                .Where(p => p.SellerId == sellerId && p.OrderDetails.Any(od => od.Order.OrderDate >= startDate && od.Order.OrderDate <= endDate))
                .Select(p => new SalesReportDTO
                {
                    ProductName = p.Name,
                    QuantitySold = p.OrderDetails.Sum(od => od.Quantity),
                    TotalRevenue = p.OrderDetails.Sum(od => od.Quantity * od.Price)
                })
                .ToListAsync();
        }

        public async Task<AnalyticsDTO> GetSellerAnalyticsAsync(int sellerId, DateTime startDate, DateTime endDate)
        {
            // Fetch total sales
            var totalSales = await _context.OrderDetails
                .Where(od => od.Product.SellerId == sellerId && od.Order.OrderDate >= startDate && od.Order.OrderDate <= endDate)
                .SumAsync(od => od.Quantity * od.Product.Price);

            // Fetch total reviews linked to the seller's products
            var totalReviews = await _context.Reviews
                .Where(r => r.Product.SellerId == sellerId)
                .CountAsync();

            // Calculate average rating of the seller's products
            var averageRating = await _context.Reviews
                .Where(r => r.Product.SellerId == sellerId)
                .AverageAsync(r => (double?)r.Rating) ?? 0.0;

            return new AnalyticsDTO
            {
                TotalSales = totalSales,
                TotalRevenue = totalSales,
                TotalReviews = totalReviews,
                AverageRating = averageRating
            };
        }

    }
}
