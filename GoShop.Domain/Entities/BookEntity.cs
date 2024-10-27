using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int Pages { get; set; }
    public DateTime? PublicationDate { get; set; }              
    public string? Description { get; set; } 
    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; } 
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } 
}