namespace NexCart.DTOs.Checkout
{
    public class OrderConfirmationDTO
    {
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentID { get; set; }
    }

}
