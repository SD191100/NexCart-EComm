namespace NexCart.DTOs.Product
{
    public class AddProductReq
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int? SellerId { get; set; }
        public string? MainImage { get; set; }
        public string? SecondImage { get; set; }
        public string? ThirdImage { get; set; }
        public string? Details { get; set; }
    }
}
