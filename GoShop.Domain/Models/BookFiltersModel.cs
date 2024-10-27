using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models;

public class BookFiltersModel
{
    public int? Count { get; set; }
    public int? Offset { get; set; }
    public string? SortBy { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public int Pages { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string? Description { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}
