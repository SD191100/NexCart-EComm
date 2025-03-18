using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Category;
using NexCart.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
// Authorization for ADMIN role
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILoggerManager _logger;

    public CategoryController(ICategoryService categoryService, ILoggerManager logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInfo("Fetching all categories");
        var categories = await _categoryService.GetAllCategoriesAsync();
        _logger.LogInfo("Categories retrieved successfully");
        return Ok(categories);
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> getById(int categoryID)
    {
        _logger.LogInfo($"Fetching category with ID: {categoryID}");
        var category = await _categoryService.GetCategoryByIdAsync(categoryID);
        if (category == null)
        {
            _logger.LogWarn($"Category not found for ID: {categoryID}");
            return NotFound("Category not found");
        }
        _logger.LogInfo($"Category retrieved successfully for ID: {categoryID}");
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDTO categoryRequest)
    {
        _logger.LogInfo("Adding a new category");
        await _categoryService.AddCategoryAsync(categoryRequest);
        _logger.LogInfo("Category added successfully");
        return Ok("Category added successfully.");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
    {
        _logger.LogInfo($"Updating category with ID: {id}");
        await _categoryService.UpdateCategoryAsync(id, categoryRequest);
        _logger.LogInfo($"Category updated successfully for ID: {id}");
        return Ok("Category updated successfully.");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        _logger.LogInfo($"Deleting category with ID: {id}");
        await _categoryService.DeleteCategoryAsync(id);
        _logger.LogInfo($"Category deleted successfully for ID: {id}");
        return Ok("Category deleted successfully.");
    }
}
