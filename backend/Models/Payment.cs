using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        //[Required]
        //public int OrderId { get; set; }

        //[ForeignKey("OrderId")]
        //public Order Order { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string? Status { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}

