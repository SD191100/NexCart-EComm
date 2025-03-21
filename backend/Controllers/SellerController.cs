using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.Models;
using NexCart.Services.Interfaces;
using NexCart.Data;
using NexCart.DTOs.Seller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Seller")]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;
    private readonly NexCartDBContext _context;
    private readonly ILoggerManager _logger;

    public SellerController(ISellerService sellerService, NexCartDBContext context, ILoggerManager logger)
    {
        _sellerService = sellerService;
        _context = context;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetSeller(int id)
    {
        _logger.LogInfo($"Fetching seller with ID: {id}");
        var seller = _sellerService.GetSellerById(id);
        if (seller == null)
        {
            _logger.LogWarn($"Seller with ID {id} not found");
            return NotFound(new { Message = "Seller not found" });
        }

        _logger.LogInfo($"Seller with ID {id} retrieved successfully");
        return Ok(seller);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetSellerByUserId(int userId)
    {
        _logger.LogInfo($"Fetching seller for User ID: {userId}");
        var seller = _sellerService.GetSellerByUserId(userId);
        if (seller == null)
        {
            _logger.LogWarn($"Seller for User ID {userId} not found");
            return NotFound(new { Message = "Seller not found" });
        }

        _logger.LogInfo($"Seller for User ID {userId} retrieved successfully");
        return Ok(seller);
    }

    [HttpPost]
    public IActionResult AddSeller([FromBody] Seller seller)
    {
        _logger.LogInfo($"Adding new seller: {seller.SellerId}");
        _sellerService.AddSeller(seller);
        _logger.LogInfo($"Seller added successfully with ID {seller.SellerId}");
        return CreatedAtAction(nameof(GetSeller), new { id = seller.SellerId }, seller);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSeller(int id, [FromBody] Seller seller)
    {
        if (id != seller.SellerId)
        {
            _logger.LogWarn($"Seller update failed due to ID mismatch: {id} vs {seller.SellerId}");
            return BadRequest(new { Message = "ID mismatch" });
        }

        _logger.LogInfo($"Updating seller with ID: {id}");
        _sellerService.UpdateSeller(seller);
        _logger.LogInfo($"Seller ID {id} updated successfully");
        return Ok(new { Message = "Seller updated successfully" });
    }

    [HttpGet("seller/{sellerId}/order-details")]
    public async Task<IActionResult> GetOrderDetailsForSeller(int sellerId)
    {
        _logger.LogInfo($"Fetching order details for Seller ID: {sellerId}");
        var orderDetails = _context.OrderDetails
            .Where(od => od.Product.SellerId == sellerId)
            .Select(od => new SellerOrderDetailDTO
            {
                OrderId = od.OrderId,
                OrderDate = od.Order.OrderDate,
                ProductName = od.Product.Name,
                Quantity = od.Quantity,
                Price = od.Price
            })
            .ToList();

        if (!orderDetails.Any())
        {
            _logger.LogWarn($"No orders found for Seller ID: {sellerId}");
            return NotFound(new { message = "No orders found for this seller." });
        }

        _logger.LogInfo($"Retrieved {orderDetails.Count} orders for Seller ID: {sellerId}");
        return Ok(orderDetails);
    }

    [HttpGet("seller/{sellerId}/products")]
    public async Task<IActionResult> GetProductsBySeller(int sellerId)
    {
        _logger.LogInfo($"Fetching products for Seller ID: {sellerId}");
        var products = _context.Products
            .Where(p => p.SellerId == sellerId)
            .Select(p => new SellerProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                MainImage = p.MainImage,
                CategoryName = p.Category.Name
            })
            .ToList();

        if (!products.Any())
        {
            _logger.LogWarn($"No products found for Seller ID: {sellerId}");
            return NotFound(new { message = "No products found for this seller." });
        }

        _logger.LogInfo($"Retrieved {products.Count} products for Seller ID: {sellerId}");
        return Ok(products);
    }
}
