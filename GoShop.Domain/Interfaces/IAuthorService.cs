using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;
public interface IAuthorService
{
    Task<List<AuthorEntity>> GetAllAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
    Task<AuthorEntity> CreateAsync(AuthorEntity dto, CancellationToken cancellationToken);
    Task<AuthorEntity> UpdateAsync(int id, AuthorEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<AuthorEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
}