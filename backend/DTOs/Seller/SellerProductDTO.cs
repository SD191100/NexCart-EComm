
namespace NexCart.DTOs.Seller
{
    public class SellerProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string MainImage { get; set; }
        public string CategoryName { get; set; }
    }
}
