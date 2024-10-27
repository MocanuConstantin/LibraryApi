using Library.Data;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class BookRepository : IBookRepository
{
    private readonly LibraryDb _context;

    public BookRepository(LibraryDb context)
    {
        _context = context;
    }

    public async Task<List<BookEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.BookEntities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BookEntity>> GetAllAsync(BookFiltersModel model, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(model);
        query = SortBy(query, model.SortBy?.ToLower() ?? "");

        return await query
            .AsNoTracking()
            .Skip(model.Offset ?? 0)
            .Take(model.Count ?? 150)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByFiltersAsync(BookFiltersModel filters, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(filters);
        return await query.CountAsync(cancellationToken);
    }

    private IQueryable<BookEntity> GetQueryByFilters(BookFiltersModel filters)
    {
        var query = _context.BookEntities.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Title))
        {
            query = query.Where(b => b.Title.Contains(filters.Title));
        }

        if (filters.PublicationDate.HasValue)
        {
            query = query.Where(b => b.PublicationDate == filters.PublicationDate.Value);
        }

        if (!string.IsNullOrEmpty(filters.Description))
        {
            query = query.Where(b => b.Description.Contains(filters.Description));
        }

        if (filters.AuthorId > 0)
        {
            query = query.Where(b => b.AuthorId == filters.AuthorId);
        }

        if (filters.CategoryId > 0)
        {
            query = query.Where(b => b.CategoryId == filters.CategoryId);
        }

        return query;
    }

    private static IQueryable<BookEntity> SortBy(IQueryable<BookEntity> query, string sortBy)
    {
        return sortBy switch
        {
            "id-desc" => query.OrderByDescending(x => x.Id),
            "id-asc" => query.OrderBy(x => x.Id),
            "title-desc" => query.OrderByDescending(x => x.Title),
            "title-asc" => query.OrderBy(x => x.Title),
            "pages-desc" => query.OrderByDescending(x => x.Pages),
            "pages-asc" => query.OrderBy(x => x.Pages),
            "publicationdate-desc" => query.OrderByDescending(x => x.PublicationDate),
            "publicationdate-asc" => query.OrderBy(x => x.PublicationDate),
            "description-desc" => query.OrderByDescending(x => x.Description),
            "description-asc" => query.OrderBy(x => x.Description),
            "authorid-desc" => query.OrderByDescending(x => x.AuthorId),
            "authorid-asc" => query.OrderBy(x => x.AuthorId),
            "categoryid-desc" => query.OrderByDescending(x => x.CategoryId),
            "categoryid-asc" => query.OrderBy(x => x.CategoryId),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}