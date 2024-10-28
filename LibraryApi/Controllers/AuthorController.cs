using Microsoft.AspNetCore.Mvc;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorFiltersModel>>> GetAll([FromQuery] AuthorFiltersModel model, CancellationToken ct)
    {
       var result = await _authorService.GetAllAsync(model, ct);
        return Ok(result);
    }
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCountByFilters([FromQuery] AuthorFiltersModel model, CancellationToken ct)
    {
        var count = await _authorService.GetCountByFiltersAsync(model, ct);
        return Ok(count);
    }
}