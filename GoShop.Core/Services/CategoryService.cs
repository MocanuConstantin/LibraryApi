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
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryService> _logger;
    public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<List<CategoryEntity>> GetAllAsync(CategoryFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetAllAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all CategoryEntities");
            return new List<CategoryEntity>();
        }
    }

    public async Task<int> GetCountByFiltersAsync(CategoryFiltersModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.GetCountByFiltersAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get count by filters for CategoryEntities");
            return 0;
        }
    }
    public async Task<CategoryEntity> CreateAsync(CategoryEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.CreateCategoryAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create CategoryEntity");
            throw;
        }
    }
    public async Task<CategoryEntity> UpdateAsync(int id, CategoryEntity updatedEntity, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.UpdateCategoryAsync(id, updatedEntity, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update CategoryEntity");
            throw;
        }
    }
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.DeleteCategoryAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete CategoryEntity");
            throw;
        }
    }
    public async Task<CategoryEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetCategoryByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get category by ID {Id}", id);
            return null;
        }
    }
}