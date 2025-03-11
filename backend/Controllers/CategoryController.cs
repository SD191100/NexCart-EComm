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

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Route("")]
    [HttpGet]
    
    public async Task<IActionResult> getAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [Route("{id}")]
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> getById(int categoryID)
    {
        return Ok(await _categoryService.GetCategoryByIdAsync(categoryID));
    }

    //[Route("getAllCategories")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDTO categoryRequest)
    {
        await _categoryService.AddCategoryAsync(categoryRequest);
        return Ok("Category added successfully.");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
    {
        await _categoryService.UpdateCategoryAsync(id, categoryRequest);
        return Ok("Category updated successfully.");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return Ok("Category deleted successfully.");
    }
}
