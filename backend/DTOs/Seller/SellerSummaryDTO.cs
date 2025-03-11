
namespace NexCart.DTOs.Seller
{
    public class SellerSummaryDTO
    {
        public int SellerId { get; set; }
        public string CompanyName { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalSales { get; set; }
    }
}
