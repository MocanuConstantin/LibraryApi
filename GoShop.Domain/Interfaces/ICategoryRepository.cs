using Library.Domain.Entities;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(CategoryFiltersModel model, CancellationToken cancellationToken);
}
