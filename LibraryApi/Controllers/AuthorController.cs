using Microsoft.AspNetCore.Mvc;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Entities;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorEntity>> GetAuthorById(int id, CancellationToken cancellationToken)
    {
        var author = await _authorService.GetByIdAsync(id, cancellationToken);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorEntity author, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdAuthor = await _authorService.CreateAsync(author, cancellationToken);
        return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorEntity updatedAuthor, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authorService.UpdateAsync(id, updatedAuthor, cancellationToken);
        if (result == null)
        {
            return NotFound("Author not found.");
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        var result = await _authorService.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}