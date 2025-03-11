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

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetCart(int userId)
    {
        var cart = _cartService.GetCartByUserId(userId);
        if (cart == null) return NotFound(new { Message = "Cart not found" });

        return Ok(cart);
    }

    [HttpPost("{userId}/add")]
    public IActionResult AddToCart(int userId, [FromBody] CartItemDto cartItemDto)
    {
        _cartService.AddToCart(userId, cartItemDto);
        return Ok(new { Message = "Item added to cart" });
    }

    [HttpPut("item/{cartItemId}")]
    public IActionResult UpdateCartItem(int cartItemId, int Quantity)
    {
        _cartService.UpdateCartItem(cartItemId, Quantity);
        return Ok(new { Message = "Cart item updated" });
    }

    [HttpDelete("item/{cartItemId}")]
    public IActionResult RemoveCartItem(int cartItemId)
    {
        _cartService.RemoveCartItem(cartItemId);
        return Ok(new { Message = "Cart item removed" });
    }

    [HttpDelete("{userId}/clear")]
    public IActionResult ClearCart(int userId)
    {
        _cartService.ClearCart(userId);
        return Ok(new { Message = "Cart cleared" });
    }
}
