using NexCart.DTOs.Category;
using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(CategoryRequestDTO categoryRequest);
        Task UpdateCategoryAsync(int categoryId, CategoryRequestDTO categoryRequest);
        Task DeleteCategoryAsync(int categoryId);
    }

}
