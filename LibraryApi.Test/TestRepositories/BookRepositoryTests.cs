using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Data;
using Library.Data.Repositories;
using Library.Domain.Entities;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryApi.Test.TestRepositories;

public class BookRepositoryTests
{
    private LibraryDb CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDb>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;
        return new LibraryDb(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsBooks()
    {
        //Arrange
        using var context = CreateInMemoryDb();

        context.BookEntities.AddRange(
            new BookEntity { Title = "Book 1" },
            new BookEntity { Title = "Book 2" }
        );
        await context.SaveChangesAsync();

        var repository = new BookRepository(context);

        //Act
        var result = await repository.GetAllAsync(CancellationToken.None);

        //Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.Title == "Book 1");
        Assert.Contains(result, b => b.Title == "Book 2");
    }

    [Fact]
    public async Task CreateBookAsync_SavesBookToDatabase()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new BookRepository(context);
        var newBook = new BookEntity { Title = "New Book" };

        //Act
        var result = await repository.CreateBookAsync(newBook, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("New Book", result.Title);
    }

    [Fact]
    public async Task UpdateBookAsync_UpdatesBookInDatabase_WhenBookExists()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new BookRepository(context);
        var existingBook = new BookEntity { Id = 1, Title = "Old Title" };
        context.BookEntities.Add(existingBook);
        await context.SaveChangesAsync();

        var updatedBook = new BookEntity { Title = "New Title" };

        //Act
        var result = await repository.UpdateBookAsync(1, updatedBook, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("New Title", result.Title);
    }

    [Fact]
    public async Task DeleteBookAsync_RemovesBookFromDatabase_WhenBookExists()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new BookRepository(context);

        var book = new BookEntity { Title = "Book to Delete" };
        context.BookEntities.Add(book);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.DeleteBookAsync(book.Id, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Null(await context.BookEntities.FindAsync(book.Id));
    }
}