using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }

    // Foreign Key for Author
    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; }  // Navigation property for Author

    // Foreign Key for Category
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }  // Navigation property for Category
}