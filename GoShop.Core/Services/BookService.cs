using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository repository, ILogger<BookService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<BookEntity>> GetAllAsync(BookFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetAllAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all BookEntities");
            return new List<BookEntity>();
        }
    }

    public async Task<int> GetCountByFiltersAsync(BookFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetCountByFiltersAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get count by filters for BookEntities");
            return 0;
        }
    }
}