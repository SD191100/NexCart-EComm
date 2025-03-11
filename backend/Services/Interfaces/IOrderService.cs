using NexCart.DTOs;
using NexCart.DTOs.Checkout;
using NexCart.DTOs.Order;
using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface IOrderService
    {
        OrderDto GetOrderById(int orderId);
        IEnumerable<OrderDto> GetOrdersByUserId(int userId);
        void PlaceOrder(CreateOrderDto createOrderDto);
        Task<List<OrderDTO>> GetUserOrderHistoryAsync(int userId);
        Task<OrderResponseDTO> CheckoutAsync(CheckoutRequestDTO checkoutRequest);
        Task<bool> ProcessPaymentAsync(PaymentRequestDTO paymentRequest);
        Task<OrderResponseDTO> ConfirmOrderAsync(OrderConfirmationDTO confirmationRequest);
    }
}