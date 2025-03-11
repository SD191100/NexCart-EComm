using System.ComponentModel.DataAnnotations;

namespace NexCart.DTOs.Category
{
    public class CategoryRequestDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
