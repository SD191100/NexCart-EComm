using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Checkout;
using NexCart.DTOs.Order;
using NexCart.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;

    public OrderController(IOrderService orderService, IPaymentService paymentService)
    {
        _orderService = orderService;
        _paymentService = paymentService;
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null) return NotFound(new { Message = "Order not found" });

        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetOrdersByUserId(int userId)
    {
        var orders = _orderService.GetOrdersByUserId(userId);
        return Ok(orders);
    }

    [HttpPost]
    public IActionResult PlaceSingleOrder([FromBody] CreateOrderDto createOrderDto)
    {
        _orderService.PlaceOrder(createOrderDto);
        return Ok(new { Message = "Order placed successfully" });
    }

    [HttpGet("user/{userId}/history")]
    [Authorize]  // Ensure only authenticated users can access
    public async Task<IActionResult> GetOrderHistory(int userId)
    {
        var orders = await _orderService.GetUserOrderHistoryAsync(userId);
        if (orders == null || !orders.Any())
        {
            return NotFound(new { message = "No orders found for this user." });
        }
        return Ok(orders);
    }

    [HttpPost("checkout")]
    [Authorize]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDTO request)
    {
        var response = await _orderService.CheckoutAsync(request);
        return Ok(response);
    }

    [HttpPost("process-payment")]
    [Authorize]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDTO request)
    {
        var newPayment =  _paymentService.ProcessPaymentAsync(request);
        

        //_orderService.

        return Ok(new { Message = "Payment successful", newPayment.PaymentId });
    }

    [HttpPost("confirm-order")]
    [Authorize]
    public async Task<IActionResult> ConfirmOrder([FromBody] OrderConfirmationDTO request)
    {
        var response = await _orderService.ConfirmOrderAsync(request);
        return Ok(response);
    }



}
