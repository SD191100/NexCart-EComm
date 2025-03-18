namespace NexCart.DTOs.Checkout
{
    public class CheckoutRequestDTO
    {
       
        public List<CartItemDto> CartItems { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
    }

    //public class CartItemDTO
    //{
    //    public int ProductId { get; set; }
    //    public int Quantity { get; set; }
    //}
}
