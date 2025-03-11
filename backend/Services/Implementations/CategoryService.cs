using NexCart.DTOs.Category;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return categories.Select(c => new CategoryResponseDTO
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Description = c.Description
        });
    }

    public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
        if (category == null) return null;

        return new CategoryResponseDTO
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description
        };
    }

    public async Task AddCategoryAsync(CategoryRequestDTO categoryRequest)
    {
        var category = new Category
        {
            Name = categoryRequest.Name,
            Description = categoryRequest.Description,
        };
        await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task UpdateCategoryAsync(int categoryId, CategoryRequestDTO categoryRequest)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
        if (category != null)
        {
            category.Name = categoryRequest.Name;
            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await _categoryRepository.DeleteCategoryAsync(categoryId);
    }
}
