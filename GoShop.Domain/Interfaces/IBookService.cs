using Library.Domain.Entities;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces;
public interface IBookService
{
    Task<List<BookEntity>> GetAllAsync(BookFiltersModel model, CancellationToken cancellationToken);
    Task<int> GetCountByFiltersAsync(BookFiltersModel model, CancellationToken cancellationToken);
}