using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexCart.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string? FirstName { get; set; }

        [Required, MaxLength(100)]
        public string? LastName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public string? ContactNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public string Role { get; set; } = "User";

       //// Navigation property for Seller
        //public int? SellerId { get; set; } // Nullable because not all users are sellers

        //[ForeignKey("SellerId")]
        //public Seller? Seller { get; set; }

        // Navigation properties
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Address { get; set; }
    }
}

