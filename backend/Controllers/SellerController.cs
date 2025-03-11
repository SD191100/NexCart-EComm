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

    public SellerController(ISellerService sellerService, NexCartDBContext context)
    {
        _sellerService = sellerService;
        _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetSeller(int id)
    {
        var seller = _sellerService.GetSellerById(id);
        if (seller == null) return NotFound(new { Message = "Seller not found" });

        return Ok(seller);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetSellerByUserId(int userId)
    {
        var seller = _sellerService.GetSellerByUserId(userId);
        if (seller == null) return NotFound(new { Message = "Seller not found" });

        return Ok(seller);
    }

    [HttpPost]
    public IActionResult AddSeller([FromBody] Seller seller)
    {
        _sellerService.AddSeller(seller);
        return CreatedAtAction(nameof(GetSeller), new { id = seller.SellerId }, seller);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSeller(int id, [FromBody] Seller seller)
    {
        if (id != seller.SellerId) return BadRequest(new { Message = "ID mismatch" });

        _sellerService.UpdateSeller(seller);
        return Ok(new { Message = "Seller updated successfully" });
    }

    [HttpGet("seller/{sellerId}/order-details")]
    public async Task<IActionResult> GetOrderDetailsForSeller(int sellerId)
    {
        var orderDetails =  _context.OrderDetails
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
            return NotFound(new { message = "No orders found for this seller." });
        }

        return Ok(orderDetails);
    }

[HttpGet("seller/{sellerId}/products")]
    public async Task<IActionResult> GetProductsBySeller(int sellerId)
    {
        var products =  _context.Products
            .Where(p => p.SellerId == sellerId)
            .Select(p => new SellerProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                MainImage = p.MainImage,
                CategoryName = p.Category.Name // Include category name if needed
            })
            .ToList();

        if (!products.Any())
        {
            return NotFound(new { message = "No products found for this seller." });
        }

        return Ok(products);
    }
}
