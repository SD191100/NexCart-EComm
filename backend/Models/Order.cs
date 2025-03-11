using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public decimal? TotalAmount { get; set; }
        

        public DateTime OrderDate { get; set; }

        // Navigation
        public ICollection<OrderDetail> OrderDetails { get; set; }

        [Required]
        public int PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payment Payments { get; set; }
    }
}

