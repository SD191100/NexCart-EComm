namespace NexCart.DTOs.Payment
{
    public class PaymentRequestDTO
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }

}
