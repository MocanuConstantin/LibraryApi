using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public List<BookEntity> Books { get; set; } = new List<BookEntity>();  // Initialize to avoid null
}