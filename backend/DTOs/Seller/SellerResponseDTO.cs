using NexCart.DTOs.Adderss;
namespace NexCart.DTOs.Seller
{
    public class SellerResponseDTO
    {
        public int SellerId { get; set; }
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSC { get; set; }
        public int UserId { get; set; } // Associated user
        public string UserName { get; set; } // Optional, derived from user
        public AddressRequestDTO? Address { get; set; } // Nested address DTO
    }
}
