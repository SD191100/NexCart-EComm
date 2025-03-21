using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Product;
using NexCart.DTOs.Sales;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;
    private readonly ILoggerManager _logger;

    public ProductController(IProductService productService, IProductRepository productRepository, ILoggerManager logger)
    {
        _productService = productService;
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        _logger.LogInfo("Fetching all products");
        var products = _productService.GetAllProducts();
        _logger.LogInfo($"Retrieved {products.Count()} products");
        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = "Seller")]
    public IActionResult AddProduct([FromBody] AddProductReq product)
    {
        _logger.LogInfo($"Adding new product: {product.Name}");
        _productService.AddProduct(product);
        _logger.LogInfo($"Product {product.Name} added successfully");
        return CreatedAtAction(nameof(GetProducts), product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Seller")]
    public IActionResult UpdateProduct(int id, [FromBody] UpdateProductReq product)
    {
        if (id != product.Id)
        {
            _logger.LogWarn($"Product ID mismatch: {id} vs {product.Id}");
            return BadRequest(new { Message = "Product ID mismatch" });
        }

        _logger.LogInfo($"Updating product with ID: {id}");
        _productService.UpdateProduct(product);
        _logger.LogInfo($"Product ID {id} updated successfully");
        return Ok(new { Message = "Product updated successfully" });
    }

    [HttpPut("/stock/{id}")]
    [Authorize(Roles = "User")]
    public IActionResult UpdateStock(int id, [FromBody] UpdateStockDTO stock)
    {
        if (id != stock.ProductId)
        {
            _logger.LogWarn($"Stock update failed due to ID mismatch: {id} vs {stock.ProductId}");
            return BadRequest(new { Message = "Product ID mismatch" });
        }

        _logger.LogInfo($"Updating stock for product ID: {id}");
        _productService.UpdateProduct(stock);
        _logger.LogInfo($"Stock updated for product ID: {id}");
        return Ok(new { Message = "Product stock updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Seller")]
    public IActionResult DeleteProduct(int id)
    {
        _logger.LogInfo($"Deleting product with ID: {id}");
        _productService.DeleteProduct(id);
        _logger.LogInfo($"Product ID {id} deleted successfully");
        return Ok(new { Message = "Product deleted successfully" });
    }

    [Route("browse")]
    [HttpGet()]
    public IActionResult BrowseProducts([FromQuery] string? searchQuery, [FromQuery] int? categoryId,
                                    [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
                                    [FromQuery] string sortOption = "price_asc", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInfo($"Browsing products with filters: SearchQuery={searchQuery}, CategoryID={categoryId}, MinPrice={minPrice}, MaxPrice={maxPrice}, SortOption={sortOption}, Page={page}, PageSize={pageSize}");
        var products = _productRepository.GetProducts(searchQuery, categoryId, minPrice, maxPrice, sortOption, page, pageSize);
        _logger.LogInfo($"Retrieved {products.Count()} products from browse query");
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public IActionResult GetProductDetails([FromRoute] int productId)
    {
        _logger.LogInfo($"Fetching details for product ID: {productId}");
        var product = _productRepository.GetProductById(productId);

        if (product == null)
        {
            _logger.LogWarn($"Product with ID {productId} not found");
            return NotFound($"Product with ID {productId} not found.");
        }

        _logger.LogInfo($"Product details retrieved for ID: {productId}");
        return Ok(product);
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductsBySeller()
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        _logger.LogInfo($"Fetching products for Seller ID: {sellerId}");
        var products = await _productService.GetProductsBySellerAsync(sellerId);
        _logger.LogInfo($"Retrieved {products.Count()} products for Seller ID: {sellerId}");
        return Ok(products);
    }

    [HttpGet("sales-report")]
    public async Task<ActionResult<IEnumerable<SalesReportDTO>>> GenerateSalesReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        _logger.LogInfo($"Generating sales report for Seller ID: {sellerId}, StartDate={startDate}, EndDate={endDate}");
        var report = await _productService.GenerateSalesReportAsync(sellerId, startDate, endDate);
        _logger.LogInfo($"Sales report generated for Seller ID: {sellerId}");
        return Ok(report);
    }

    [HttpGet("analytics")]
    public async Task<ActionResult<AnalyticsDTO>> GetSellerAnalytics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        _logger.LogInfo($"Fetching analytics for Seller ID: {sellerId}, StartDate={startDate}, EndDate={endDate}");
        var analytics = await _productService.GetSellerAnalyticsAsync(sellerId, startDate, endDate);
        _logger.LogInfo($"Analytics retrieved for Seller ID: {sellerId}");
        return Ok(analytics);
    }
}
