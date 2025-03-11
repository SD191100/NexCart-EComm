using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Payment GetPaymentById(int paymentId);
        public Payment GetPaymentByOrderId(int orderId);
        void AddPayment(Payment payment);
    }
}
