using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class ActivityLog
    {
        [Key]
        public int ActivityLogId { get; set; }

        public int SellerId { get; set; }

        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        [Required]
        public string Activity { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
