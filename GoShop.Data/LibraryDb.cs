using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class LibraryDb : DbContext
{
    public DbSet<BookEntity> BookEntities {  get; set; }
    public DbSet<AuthorEntity> AuthorEntities {  get; set; }
    public DbSet<CategoryEntity> CategoryEntities {  get; set; }
    public LibraryDb(DbContextOptions<LibraryDb> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
