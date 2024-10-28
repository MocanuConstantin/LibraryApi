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
}