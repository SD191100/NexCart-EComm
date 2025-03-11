using NexCart.DTOs.Checkout;
using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface IPaymentService
    {
        Payment GetPaymentById(int paymentId);
        Payment GetPaymentsByOrderId(int orderId);
        void AddPayment(Payment payment);
        public Payment ProcessPaymentAsync(PaymentRequestDTO paymentRequest);
    }
}
