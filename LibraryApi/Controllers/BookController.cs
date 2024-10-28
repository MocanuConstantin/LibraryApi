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


}