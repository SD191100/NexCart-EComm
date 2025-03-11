using NexCart.DTOs.Adderss;
namespace NexCart.DTOs.Seller
{
    public class SellerRequestDTO
    {
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSC { get; set; }
        public int UserId { get; set; } // Link to the user
        public AddressRequestDTO? Address { get; set; } // Nested DTO for seller's address
    }
}
