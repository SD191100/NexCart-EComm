namespace NexCart.DTOs.Checkout
{
    public class PaymentRequestDTO
    {
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }

}
