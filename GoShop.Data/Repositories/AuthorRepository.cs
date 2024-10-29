using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDb _context;
    public AuthorRepository(LibraryDb context)
    {
     _context = context;
    }
    public async Task<List<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.AuthorEntities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<List<AuthorEntity>> GetAllAsync(AuthorFiltersModel model, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(model);
        query = SortBy(query, model.SortBy?.ToLower() ?? "");

        return await query
            .AsNoTracking()
            .Skip(model.Offset ?? 0)
            .Take(model.Count ?? 150)
            .ToListAsync(cancellationToken);
    }
    public async Task<int> GetCountByFiltersAsync(AuthorFiltersModel filters, CancellationToken cancellationToken)
    {
        var query = GetQueryByFilters(filters);
        return await query.CountAsync(cancellationToken);
    }
    private IQueryable<AuthorEntity> GetQueryByFilters(AuthorFiltersModel filters)
    {
        var query = _context.AuthorEntities.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Name))
        {
            query = query.Where(a => a.Name.Contains(filters.Name));
        }

        if (filters.BirthDate.HasValue)
        {
            query = query.Where(a => a.BirthDate == filters.BirthDate.Value);
        }

        if (!string.IsNullOrEmpty(filters.Country))
        {
            query = query.Where(a => a.Country == filters.Country);
        }

        return query;
    }
    private static IQueryable<AuthorEntity> SortBy(IQueryable<AuthorEntity> query, string sortBy)
    {
        return sortBy switch
        {
            "id-desc" => query.OrderByDescending(x => x.Id),
            "id-asc" => query.OrderBy(x => x.Id),
            "name-desc" => query.OrderByDescending(x => x.Name),
            "name-asc" => query.OrderBy(x => x.Name),
            "birthdate-desc" => query.OrderByDescending(x => x.BirthDate),
            "birthdate-asc" => query.OrderBy(x => x.BirthDate),
            "country-desc" => query.OrderByDescending(x => x.Country),
            "country-asc" => query.OrderBy(x => x.Country),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
    public async Task<AuthorEntity> CreateAuthorAsync(AuthorEntity author, CancellationToken cancellationToken)
    {
        _context.AuthorEntities.Add(author);
        await _context.SaveChangesAsync(cancellationToken);
        return author;
    }
    public async Task<AuthorEntity?> UpdateAuthorAsync(int id, AuthorEntity updatedAuthor, CancellationToken cancellationToken)
    {
        var author = await _context.AuthorEntities.FindAsync(new object[] { id }, cancellationToken);
        if (author == null) return null;

        author.Name = updatedAuthor.Name;
        author.BirthDate = updatedAuthor.BirthDate;
        author.Country = updatedAuthor.Country;

        await _context.SaveChangesAsync(cancellationToken);
        return author;
    }
    public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _context.AuthorEntities.FindAsync(new object[] { id }, cancellationToken);
        if (author == null) return false;

        _context.AuthorEntities.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<AuthorEntity?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.AuthorEntities
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}