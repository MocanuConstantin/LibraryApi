using Library.Data;
using Library.Data.Repositories;
using Library.Domain.Entities;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Test.TestRepositories;

public class AuthorRepositoryTests
{
    private LibraryDb CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDb>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database name to avoid conflicts
            .Options;
        return new LibraryDb(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAuthors()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        context.AuthorEntities.AddRange(
            new AuthorEntity { Id = 1, Name = "Author 1", Country = "Country 1" },
            new AuthorEntity { Id = 2, Name = "Author 2", Country = "Country 2" }
        );
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);

        // Act
        var result = await repository.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, a => a.Name == "Author 1");
        Assert.Contains(result, a => a.Name == "Author 2");
    }

    [Fact]
    public async Task GetAuthorByIdAsync_ReturnsCorrectAuthor()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        context.AuthorEntities.Add(new AuthorEntity { Id = 1, Name = "Author 1", Country = "Country 1" });
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);

        // Act
        var result = await repository.GetAuthorByIdAsync(1, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Author 1", result?.Name);
    }

    [Fact]
    public async Task CreateAuthorAsync_AddsAuthorToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        var repository = new AuthorRepository(context);

        var newAuthor = new AuthorEntity { Id = 1, Name = "New Author", Country = "New Country" };

        // Act
        var result = await repository.CreateAuthorAsync(newAuthor, CancellationToken.None);

        // Assert
        Assert.Equal("New Author", result.Name);
        Assert.Single(context.AuthorEntities);
    }

    [Fact]
    public async Task UpdateAuthorAsync_UpdatesExistingAuthor()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        context.AuthorEntities.Add(new AuthorEntity { Id = 1, Name = "Original Name", Country = "Original Country" });
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);

        var updatedAuthor = new AuthorEntity { Name = "Updated Name", Country = "Updated Country" };

        // Act
        var result = await repository.UpdateAuthorAsync(1, updatedAuthor, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Name", result?.Name);
        Assert.Equal("Updated Country", result?.Country);
    }

    [Fact]
    public async Task DeleteAuthorAsync_RemovesAuthorFromDatabase()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        context.AuthorEntities.Add(new AuthorEntity { Id = 1, Name = "Author to Delete", Country = "Country" });
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);

        // Act
        var result = await repository.DeleteAuthorAsync(1, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Empty(context.AuthorEntities);
    }

    [Fact]
    public async Task GetCountByFiltersAsync_ReturnsFilteredCount()
    {
        // Arrange
        using var context = CreateInMemoryDb();
        context.AuthorEntities.AddRange(
            new AuthorEntity { Id = 1, Name = "Author A", Country = "Country 1" },
            new AuthorEntity { Id = 2, Name = "Author B", Country = "Country 2" }
        );
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);
        var filters = new AuthorFiltersModel { Country = "Country 1" };

        // Act
        var result = await repository.GetCountByFiltersAsync(filters, CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
    }
}
