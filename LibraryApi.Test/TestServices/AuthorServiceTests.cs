using Library.Core.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LibraryApi.Test.TestServices;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _mockRepository;
    private readonly Mock<ILogger<AuthorService>> _mockLogger;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _mockRepository = new Mock<IAuthorRepository>();
        _mockLogger = new Mock<ILogger<AuthorService>>();
        _authorService = new AuthorService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfAuthors()
    {
        //Arrange
        var authorList = new List<AuthorEntity> { new AuthorEntity { Id = 1, Name = "Eminescu" } };

        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<AuthorFiltersModel>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(authorList);

        //Act
        var result = await _authorService.GetAllAsync(new AuthorFiltersModel(), CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Eminescu", result[0].Name);
    }

    [Fact]
    public async Task CreateAsync_SavesAndReturnsAuthor()
    {
        //Arrange
        var newAuthor = new AuthorEntity { Id = 1, Name = "Grigore Vieru" };
        _mockRepository.Setup(repo => repo.CreateAuthorAsync(newAuthor, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(newAuthor);

        //Act
        var result = await _authorService.CreateAsync(newAuthor, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Grigore Vieru", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedAuthor()
    {
        //Arrange
        var updatedAuthor = new AuthorEntity { Id = 1, Name = "Ion Druta" };
        _mockRepository.Setup(repo => repo.UpdateAuthorAsync(1, updatedAuthor, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(updatedAuthor);

        //Act
        var result = await _authorService.UpdateAsync(1, updatedAuthor, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Ion Druta", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrueIfDeleted()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.DeleteAuthorAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        //Act
        var result = await _authorService.DeleteAsync(1, CancellationToken.None);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsAuthorIfExists()
    {
        //Arrange
        var author = new AuthorEntity { Id = 1, Name = "Mihail Kogalniceanu" };
        _mockRepository.Setup(repo => repo.GetAuthorByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(author);

        //Act
        var result = await _authorService.GetByIdAsync(1, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Mihail Kogalniceanu", result?.Name);
    }
}