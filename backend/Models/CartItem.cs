using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        //[Required]
        //public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User User { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int? CartId { get; set; }
        public Cart cart { get; set; }
    }
}

