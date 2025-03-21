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
    private readonly ILoggerManager _logger;

    public OrderController(IOrderService orderService, IPaymentService paymentService, ILoggerManager logger)
    {
        _orderService = orderService;
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        _logger.LogInfo($"Fetching order with ID: {id}");
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            _logger.LogWarn($"Order not found for ID: {id}");
            return NotFound(new { Message = "Order not found" });
        }

        _logger.LogInfo($"Order retrieved successfully for ID: {id}");
        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetOrdersByUserId(int userId)
    {
        _logger.LogInfo($"Fetching orders for User ID: {userId}");
        var orders = _orderService.GetOrdersByUserId(userId);
        _logger.LogInfo($"Orders retrieved successfully for User ID: {userId}");
        return Ok(orders);
    }

    [HttpPost]
    public IActionResult PlaceSingleOrder([FromBody] CreateOrderDto createOrderDto)
    {
        _logger.LogInfo("Placing a new order");
        _orderService.PlaceOrder(createOrderDto);
        _logger.LogInfo("Order placed successfully");
        return Ok(new { Message = "Order placed successfully" });
    }

    [HttpGet("user/{userId}/history")]
    [Authorize]
    public async Task<IActionResult> GetOrderHistory(int userId)
    {
        _logger.LogInfo($"Fetching order history for User ID: {userId}");
        var orders = await _orderService.GetUserOrderHistoryAsync(userId);
        if (orders == null || !orders.Any())
        {
            _logger.LogWarn($"No orders found for User ID: {userId}");
            return NotFound(new { message = "No orders found for this user." });
        }
        _logger.LogInfo($"Order history retrieved successfully for User ID: {userId}");
        return Ok(orders);
    }

    [HttpPost("checkout")]
    [Authorize]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDTO request)
    {
        _logger.LogInfo($"Processing checkout for User ID");
        var response = await _orderService.CheckoutAsync(request);
        _logger.LogInfo($"Checkout completed for User ID");
        return Ok(response);
    }

    [HttpPost("process-payment")]
    [Authorize]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDTO request)
    {
        _logger.LogInfo($"Processing payment for User ID");
        var newPayment = _paymentService.ProcessPaymentAsync(request);
        _logger.LogInfo($"Payment processed successfully with Payment ID");

        return Ok(new { Message = "Payment successful", newPayment.PaymentId });
    }

    [HttpPost("confirm-order")]
    [Authorize]
    public async Task<IActionResult> ConfirmOrder([FromBody] OrderConfirmationDTO request)
    {
        _logger.LogInfo($"Confirming order for Order ID");
        var response = await _orderService.ConfirmOrderAsync(request);
        _logger.LogInfo($"Order confirmed successfully for Order ID");
        return Ok(response);
    }
}
