using Library.Data;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryDb _context;

    public CategoryRepository(LibraryDb context)
    {
        _context = context;
    }

    public async Task<List<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.CategoryEntities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CategoryEntity>> GetAllAsync(CategoryFiltersModel model, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(model);
        query = SortBy(query, model.SortBy?.ToLower() ?? "");

        return await query
            .AsNoTracking()
            .Skip(model.Offset ?? 0)
            .Take(model.Count ?? 150)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByFiltersAsync(CategoryFiltersModel filters, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(filters);
        return await query.CountAsync(cancellationToken);
    }

    private IQueryable<CategoryEntity> GetQueryByFilters(CategoryFiltersModel filters)
    {
        var query = _context.CategoryEntities.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Name))
        {
            query = query.Where(c => c.Name.Contains(filters.Name));
        }

        if (!string.IsNullOrEmpty(filters.Description))
        {
            query = query.Where(c => c.Description.Contains(filters.Description));
        }

        return query;
    }

    private static IQueryable<CategoryEntity> SortBy(IQueryable<CategoryEntity> query, string sortBy)
    {
        return sortBy switch
        {
            "id-desc" => query.OrderByDescending(x => x.Id),
            "id-asc" => query.OrderBy(x => x.Id),
            "name-desc" => query.OrderByDescending(x => x.Name),
            "name-asc" => query.OrderBy(x => x.Name),
            "description-desc" => query.OrderByDescending(x => x.Description),
            "description-asc" => query.OrderBy(x => x.Description),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}