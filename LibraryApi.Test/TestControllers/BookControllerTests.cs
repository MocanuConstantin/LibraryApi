using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using LibraryApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryApi.Test.TestControllers;

public class BookControllerTests
{
    private readonly Mock<IBookService> _mockService;
    private readonly BookController _bookController;

    public BookControllerTests()
    {
        _mockService = new Mock<IBookService>();
        _bookController = new BookController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithBooks()
    {
        // Arrange
        var books = new List<BookEntity> { new BookEntity { Id = 1, Title = "Book 1" } };
        _mockService.Setup(service => service.GetAllAsync(It.IsAny<BookFiltersModel>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(books);

        // Act
        var result = await _bookController.GetAll(new BookFiltersModel(), CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBooks = Assert.IsType<List<BookEntity>>(okResult.Value);
        Assert.Single(returnedBooks);
    }

    [Fact]
    public async Task GetBookById_ReturnsOkResult_WhenBookExists()
    {
        // Arrange
        var book = new BookEntity { Id = 1, Title = "Book 1" };
        _mockService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(book);

        // Act
        var result = await _bookController.GetBookById(1, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBook = Assert.IsType<BookEntity>(okResult.Value);
        Assert.Equal(1, returnedBook.Id);
    }

    [Fact]
    public async Task CreateBook_ReturnsCreatedAtAction_WithCreatedBook()
    {
        // Arrange
        var newBook = new BookEntity { Id = 1, Title = "New Book" };
        _mockService.Setup(service => service.CreateAsync(newBook, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(newBook);

        // Act
        var result = await _bookController.CreateBook(newBook, CancellationToken.None);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdBook = Assert.IsType<BookEntity>(createdAtActionResult.Value);
        Assert.Equal("New Book", createdBook.Title);
    }

    [Fact]
    public async Task UpdateBook_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var updatedBook = new BookEntity { Id = 1, Title = "Updated Book" };
        _mockService.Setup(service => service.UpdateAsync(1, updatedBook, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(updatedBook);

        // Act
        var result = await _bookController.UpdateBook(1, updatedBook, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBook = Assert.IsType<BookEntity>(okResult.Value);
        Assert.Equal("Updated Book", returnedBook.Title);
    }

    [Fact]
    public async Task DeleteBook_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        // Act
        var result = await _bookController.DeleteBook(1, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        // Act
        var result = await _bookController.DeleteBook(1, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}