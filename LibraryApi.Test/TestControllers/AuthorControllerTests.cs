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

public class AuthorControllerTests
{
    private readonly Mock<IAuthorService> _mockAuthorService;
    private readonly AuthorController _authorController;

    public AuthorControllerTests()
    {
        _mockAuthorService = new Mock<IAuthorService>();
        _authorController = new AuthorController(_mockAuthorService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfAuthors()
    {
        // Arrange
        var mockAuthors = new List<AuthorEntity> { new AuthorEntity { Id = 1, Name = "Author 1" } };
        _mockAuthorService.Setup(service => service.GetAllAsync(It.IsAny<AuthorFiltersModel>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(mockAuthors);

        // Act
        var result = await _authorController.GetAll(new AuthorFiltersModel(), CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAuthors = Assert.IsType<List<AuthorEntity>>(okResult.Value);
        Assert.Single(returnedAuthors);
    }

    [Fact]
    public async Task GetCountByFilters_ReturnsOkResult_WithCount()
    {
        // Arrange
        _mockAuthorService.Setup(service => service.GetCountByFiltersAsync(It.IsAny<AuthorFiltersModel>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(5);

        // Act
        var result = await _authorController.GetCountByFilters(new AuthorFiltersModel(), CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var count = Assert.IsType<int>(okResult.Value);
        Assert.Equal(5, count);
    }

    [Fact]
    public async Task GetAuthorById_ReturnsOkResult_WithAuthor()
    {
        // Arrange
        var author = new AuthorEntity { Id = 1, Name = "Author 1" };
        _mockAuthorService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(author);

        // Act
        var result = await _authorController.GetAuthorById(1, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAuthor = Assert.IsType<AuthorEntity>(okResult.Value);
        Assert.Equal(1, returnedAuthor.Id);
    }

    [Fact]
    public async Task GetAuthorById_ReturnsNotFound_WhenAuthorNotFound()
    {
        // Arrange
        _mockAuthorService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((AuthorEntity)null);

        // Act
        var result = await _authorController.GetAuthorById(1, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateAuthor_ReturnsCreatedAtAction_WithCreatedAuthor()
    {
        // Arrange
        var newAuthor = new AuthorEntity { Id = 1, Name = "New Author" };
        _mockAuthorService.Setup(service => service.CreateAsync(newAuthor, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(newAuthor);

        // Act
        var result = await _authorController.CreateAuthor(newAuthor, CancellationToken.None);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdAuthor = Assert.IsType<AuthorEntity>(createdAtActionResult.Value);
        Assert.Equal("New Author", createdAuthor.Name);
    }

    [Fact]
    public async Task UpdateAuthor_ReturnsOkResult_WithUpdatedAuthor()
    {
        // Arrange
        var updatedAuthor = new AuthorEntity { Id = 1, Name = "Updated Author" };
        _mockAuthorService.Setup(service => service.UpdateAsync(1, updatedAuthor, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(updatedAuthor);

        // Act
        var result = await _authorController.UpdateAuthor(1, updatedAuthor, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedAuthor = Assert.IsType<AuthorEntity>(okResult.Value);
        Assert.Equal("Updated Author", returnedAuthor.Name);
    }

    [Fact]
    public async Task UpdateAuthor_ReturnsNotFound_WhenAuthorNotFound()
    {
        // Arrange
        _mockAuthorService.Setup(service => service.UpdateAsync(1, It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync((AuthorEntity)null);

        // Act
        var result = await _authorController.UpdateAuthor(1, new AuthorEntity { Id = 1, Name = "Updated Author" }, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAuthor_ReturnsNoContent_WhenAuthorDeleted()
    {
        // Arrange
        _mockAuthorService.Setup(service => service.DeleteAsync(1, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

        // Act
        var result = await _authorController.DeleteAuthor(1, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAuthor_ReturnsNotFound_WhenAuthorNotFound()
    {
        // Arrange
        _mockAuthorService.Setup(service => service.DeleteAsync(1, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

        // Act
        var result = await _authorController.DeleteAuthor(1, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
