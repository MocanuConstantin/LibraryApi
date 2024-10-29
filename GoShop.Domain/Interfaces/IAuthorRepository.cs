using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<List<AuthorEntity>> GetAllAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
    Task<AuthorEntity> CreateAuthorAsync(AuthorEntity dto, CancellationToken cancellationToken);
    Task<AuthorEntity> UpdateAuthorAsync(int id, AuthorEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
    Task<AuthorEntity> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
}