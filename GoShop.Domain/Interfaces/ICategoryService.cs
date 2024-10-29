using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryEntity>> GetAllAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
    Task<CategoryEntity> CreateAsync(CategoryEntity dto, CancellationToken cancellationToken);
    Task<CategoryEntity> UpdateAsync(int id, CategoryEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<CategoryEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
}