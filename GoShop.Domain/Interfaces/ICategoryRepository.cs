using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
    Task<CategoryEntity> CreateCategoryAsync(CategoryEntity dto, CancellationToken cancellationToken);
    Task<CategoryEntity> UpdateCategoryAsync(int id, CategoryEntity dto, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
    Task<CategoryEntity> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
}