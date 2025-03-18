using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs;
using NexCart.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILoggerManager _logger;

    public CartController(ICartService cartService, ILoggerManager logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public IActionResult GetCart(int userId)
    {
        _logger.LogInfo($"Fetching cart for User ID: {userId}");
        var cart = _cartService.GetCartByUserId(userId);
        if (cart == null)
        {
            _logger.LogWarn($"Cart not found for User ID: {userId}");
            return NotFound(new { Message = "Cart not found" });
        }

        _logger.LogInfo($"Cart retrieved successfully for User ID: {userId}");
        return Ok(cart);
    }

    [HttpPost("{userId}/add")]
    public IActionResult AddToCart(int userId, [FromBody] CartItemDto cartItemDto)
    {
        _logger.LogInfo($"Adding item to cart for User ID: {userId}");
        _cartService.AddToCart(userId, cartItemDto);
        _logger.LogInfo($"Item added to cart for User ID: {userId}");
        return Ok(new { Message = "Item added to cart" });
    }

    [HttpPut("item/{cartItemId}")]
    public IActionResult UpdateCartItem(int cartItemId, int Quantity)
    {
        _logger.LogInfo($"Updating cart item ID: {cartItemId} with Quantity: {Quantity}");
        _cartService.UpdateCartItem(cartItemId, Quantity);
        _logger.LogInfo($"Cart item ID: {cartItemId} updated successfully.");
        return Ok(new { Message = "Cart item updated" });
    }

    [HttpDelete("item/{cartItemId}")]
    public IActionResult RemoveCartItem(int cartItemId)
    {
        _logger.LogInfo($"Removing cart item ID: {cartItemId}");
        _cartService.RemoveCartItem(cartItemId);
        _logger.LogInfo($"Cart item ID: {cartItemId} removed successfully.");
        return Ok(new { Message = "Cart item removed" });
    }

    [HttpDelete("{userId}/clear")]
    public IActionResult ClearCart(int userId)
    {
        _logger.LogInfo($"Clearing cart for User ID: {userId}");
        _cartService.ClearCart(userId);
        _logger.LogInfo($"Cart cleared successfully for User ID: {userId}");
        return Ok(new { Message = "Cart cleared" });
    }
}
