using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;
public interface IBookService
{
    Task<List<BookEntity>> GetAllAsync(BookFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(BookFiltersModel model, CancellationToken cancellationToken);
    Task<BookEntity> CreateAsync(BookEntity dto, CancellationToken cancellationToken);
    Task<BookEntity> UpdateAsync(int id, BookEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<BookEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
}