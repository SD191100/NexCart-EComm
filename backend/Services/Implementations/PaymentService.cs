using NexCart.DTOs.Checkout;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _paymentRepository.GetPaymentById(paymentId);
        }

        public Payment GetPaymentsByOrderId(int orderId)
        {
            return _paymentRepository.GetPaymentByOrderId(orderId);
        }

        public void AddPayment(Payment payment)
        {
            _paymentRepository.AddPayment(payment);
        }

        public Payment ProcessPaymentAsync(PaymentRequestDTO paymentRequest)
        {
            // Simulate payment processing logic
            bool isPaymentSuccessful = true; // Simulating success

            
                var payment = new Payment
                {
                    Amount = paymentRequest.Amount,
                    Status = "Completed", // Assume success for now
                    PaymentMethod = paymentRequest.PaymentMethod,
                    PaymentDate = DateTime.UtcNow
                };

                 _paymentRepository.AddPayment(payment);
                return payment;
            

            //return false;
        }
    }
}
