namespace NexCart.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Seller, or User
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? GSTNumber { get; set; }
    }
}
