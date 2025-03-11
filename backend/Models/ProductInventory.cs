using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NexCart.Models
{
    public class ProductInventory
    {
        [Key]
        public int ProductInventoryId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

