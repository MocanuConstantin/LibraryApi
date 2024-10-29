using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IBookRepository
{
    Task<List<BookEntity>> GetAllAsync(BookFiltersModel model,CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(BookFiltersModel model, CancellationToken cancellationToken);
    Task<BookEntity> CreateBookAsync(BookEntity dto, CancellationToken cancellationToken);
    Task<BookEntity> UpdateBookAsync(int id, BookEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
    Task<BookEntity> GetBookByIdAsync(int id, CancellationToken cancellationToken);
}