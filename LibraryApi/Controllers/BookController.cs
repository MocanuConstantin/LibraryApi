using Library.Core.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookFiltersModel>>> GetAll([FromQuery] BookFiltersModel model, CancellationToken ct)
    {
        var result = await _bookService.GetAllAsync(model, ct);
        return Ok(result);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCountByFilters([FromQuery] BookFiltersModel model, CancellationToken ct)
    {
        var count = await _bookService.GetCountByFiltersAsync(model, ct);
        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookEntity>> GetBookById(int id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetByIdAsync(id, cancellationToken);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookEntity book, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdBook = await _bookService.CreateAsync(book, cancellationToken);
        return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookEntity updatedBook, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _bookService.UpdateAsync(id, updatedBook, cancellationToken);
        if (result == null)
        {
            return NotFound("Book not found.");
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
    {
        var result = await _bookService.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}