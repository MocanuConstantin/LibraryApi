using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models;

public class AuthorFiltersModel
{
    public int? Count { get; set; }
    public int? Offset { get; set; }
    public string? SortBy { get; set; }      
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Country { get; set; }
}
