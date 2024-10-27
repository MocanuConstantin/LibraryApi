using Library.Domain.Entities;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<List<AuthorEntity>> GetAllAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(AuthorFiltersModel model, CancellationToken cancellationToken);
}
