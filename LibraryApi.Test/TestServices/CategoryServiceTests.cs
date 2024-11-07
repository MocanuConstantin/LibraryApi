using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Core.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LibraryApi.Test.TestServices;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepository;
    private readonly Mock<ILogger<CategoryService>> _mockLogger;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _mockRepository = new Mock<ICategoryRepository>();
        _mockLogger = new Mock<ILogger<CategoryService>>();
        _categoryService = new CategoryService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfCategories()
    {
        //Arrange
        var categoryList = new List<CategoryEntity> { new CategoryEntity { Name = "Category 1" } };
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CategoryFiltersModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoryList);

        //Act
        var result = await _categoryService.GetAllAsync(new CategoryFiltersModel(), CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Category 1", result[0].Name);
    }

    [Fact]
    public async Task GetCountByFiltersAsync_ReturnsCategoryCount()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetCountByFiltersAsync(It.IsAny<CategoryFiltersModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        //Act
        var result = await _categoryService.GetCountByFiltersAsync(new CategoryFiltersModel(), CancellationToken.None);

        //Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsCategory()
    {
        //Arrange
        var newCategory = new CategoryEntity { Name = "New Category" };
        _mockRepository.Setup(repo => repo.CreateCategoryAsync(newCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(newCategory);

        //Act
        var result = await _categoryService.CreateAsync(newCategory, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("New Category", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesAndReturnsCategory()
    {
        //Arrange
        var updatedCategory = new CategoryEntity { Name = "Updated Category" };
        _mockRepository.Setup(repo => repo.UpdateCategoryAsync(1, updatedCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedCategory);

        //Act
        var result = await _categoryService.UpdateAsync(1, updatedCategory, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Category", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_DeletesCategoryAndReturnsTrue()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.DeleteCategoryAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //Act
        var result = await _categoryService.DeleteAsync(1, CancellationToken.None);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCategoryById()
    {
        //Arrange
        var category = new CategoryEntity { Id = 1, Name = "Category" };
        _mockRepository.Setup(repo => repo.GetCategoryByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        //Act
        var result = await _categoryService.GetByIdAsync(1, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}