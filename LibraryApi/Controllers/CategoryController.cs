using Library.Core.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryFiltersModel>>> GetAll([FromQuery] CategoryFiltersModel model, CancellationToken ct)
    {
        var result = await _categoryService.GetAllAsync(model, ct);
        return Ok(result);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCountByFilters([FromQuery] CategoryFiltersModel model, CancellationToken ct)
    {
        var count = await _categoryService.GetCountByFiltersAsync(model, ct);
        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryEntity>> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetByIdAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryEntity category, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdCategory = await _categoryService.CreateAsync(category, cancellationToken);
        return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryEntity updatedCategory, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _categoryService.UpdateAsync(id, updatedCategory, cancellationToken);
        if (result == null)
        {
            return NotFound("Category not found.");
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}