using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LibraryApi.Test.TestServices;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockRepository;
    private readonly Mock<ILogger<BookService>> _mockLogger;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _mockRepository = new Mock<IBookRepository>();
        _mockLogger = new Mock<ILogger<BookService>>();
        _bookService = new BookService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfBooks()
    {
        //Arrange
        var bookList = new List<BookEntity> { new BookEntity { Id = 1, Title = "Book 1" } };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<BookFiltersModel>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(bookList);

        //Act
        var result = await _bookService.GetAllAsync(new BookFiltersModel(), CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Book 1", result[0].Title);
    }

    [Fact]
    public async Task GetCountByFiltersAsync_ReturnsCorrectCount()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetCountByFiltersAsync(It.IsAny<BookFiltersModel>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(5);

        //Act
        var count = await _bookService.GetCountByFiltersAsync(new BookFiltersModel(), CancellationToken.None);

        //Assert
        Assert.Equal(5, count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsBook_WhenBookExists()
    {
        //Arrange
        var book = new BookEntity { Id = 1, Title = "Book 1" };
        _mockRepository.Setup(repo => repo.GetBookByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(book);

        //Act
        var result = await _bookService.GetByIdAsync(1, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedBook()
    {
        //Arrange
        var newBook = new BookEntity { Id = 1, Title = "New Book" };
        _mockRepository.Setup(repo => repo.CreateBookAsync(newBook, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(newBook);

        //Act
        var result = await _bookService.CreateAsync(newBook, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("New Book", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedBook_WhenUpdateIsSuccessful()
    {
        //Arrange
        var updatedBook = new BookEntity { Id = 1, Title = "Updated Book" };
        _mockRepository.Setup(repo => repo.UpdateBookAsync(1, updatedBook, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(updatedBook);

        //Act
        var result = await _bookService.UpdateAsync(1, updatedBook, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Book", result.Title);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenDeleteIsSuccessful()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.DeleteBookAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        //Act
        var result = await _bookService.DeleteAsync(1, CancellationToken.None);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenBookDoesNotExist()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.DeleteBookAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        //Act
        var result = await _bookService.DeleteAsync(1, CancellationToken.None);

        //Assert
        Assert.False(result);
    }
}