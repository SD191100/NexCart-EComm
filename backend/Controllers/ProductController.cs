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


    public ProductController(IProductService productService, IProductRepository productRepository)
    {
        _productService = productService;
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = "Seller")]
    public IActionResult AddProduct([FromBody] AddProductReq product)
    {
        _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProducts), product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Seller")]
    public IActionResult UpdateProduct(int id, [FromBody] UpdateProductReq product)
    {
        if (id != product.Id) return BadRequest(new { Message = "Product ID mismatch" });

        _productService.UpdateProduct(product);
        return Ok(new { Message = "Product updated successfully" });
    }

    [HttpPut("/stock/{id}")]
    [Authorize(Roles = "User")]
    public IActionResult UpdateStock(int id, [FromBody] UpdateStockDTO stock)
    {
        if (id != stock.ProductId) return BadRequest(new { Message = "Product ID mismatch" });

        _productService.UpdateProduct(stock);
        return Ok(new { Message = "Product updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Seller")]
    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return Ok(new { Message = "Product deleted successfully" });
    }

    [Route("browse")]
    [HttpGet()]
    public IActionResult BrowseProducts([FromQuery] string? searchQuery, [FromQuery] int? categoryId,
                                    [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
                                    [FromQuery] string sortOption = "price_asc", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var products = _productRepository.GetProducts(searchQuery, categoryId, minPrice, maxPrice, sortOption, page, pageSize);
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public IActionResult GetProductDetails([FromRoute] int productId)
    {
        var product = _productRepository.GetProductById(productId);

        if (product == null)
            return NotFound($"Product with ID {productId} not found.");

        return Ok(product);
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductsBySeller()
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        var products = await _productService.GetProductsBySellerAsync(sellerId);
        return Ok(products);
    }

    [HttpGet("sales-report")]
    public async Task<ActionResult<IEnumerable<SalesReportDTO>>> GenerateSalesReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        var report = await _productService.GenerateSalesReportAsync(sellerId, startDate, endDate);
        return Ok(report);
    }

    [HttpGet("analytics")]
    public async Task<ActionResult<AnalyticsDTO>> GetSellerAnalytics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var sellerId = int.Parse(User.FindFirst("UserId")?.Value);
        var analytics = await _productService.GetSellerAnalyticsAsync(sellerId, startDate, endDate);
        return Ok(analytics);
    }

}
