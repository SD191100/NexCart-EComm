using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string? Details {
          get; set;
        }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? MainImage { get; set; }

        public string? SecondImage { get; set; }
        public string? ThirdImage { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int? SellerId { get; set; }

        [ForeignKey("SellerId")]
        public Seller? Seller { get; set; }

        //Navigation
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ProductInventory ProductInventory { get; set; }
    }
}

