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
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<AuthorEntity>> GetAllAsync(AuthorFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetAllAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all AuthorEntities");
            return new List<AuthorEntity>();
        }
    }

    public async Task<int> GetCountByFiltersAsync(AuthorFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetCountByFiltersAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get count by filters for AuthorEntities");
            return 0;
        }
    }
}