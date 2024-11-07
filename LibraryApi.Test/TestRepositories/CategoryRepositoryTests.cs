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

public class CategoryRepositoryTests
{
    private LibraryDb CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDb>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
        return new LibraryDb(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCategories()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        context.CategoryEntities.AddRange(new CategoryEntity { Name = "Category 1" }, new CategoryEntity { Name = "Category 2" });
        await context.SaveChangesAsync();

        var repository = new CategoryRepository(context);

        //Act
        var result = await repository.GetAllAsync(CancellationToken.None);

        //Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateCategoryAsync_SavesCategoryToDatabase()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new CategoryRepository(context);
        var newCategory = new CategoryEntity { Name = "New Category" };

        //Act
        var result = await repository.CreateCategoryAsync(newCategory, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("New Category", result.Name);
        Assert.Equal(1, await context.CategoryEntities.CountAsync());
    }

    [Fact]
    public async Task UpdateCategoryAsync_UpdatesCategoryInDatabase()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new CategoryRepository(context);
        var category = new CategoryEntity { Name = "Old Category" };
        context.CategoryEntities.Add(category);
        await context.SaveChangesAsync();

        category.Name = "Updated Category";

        //Act
        var result = await repository.UpdateCategoryAsync(category.Id, category, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Category", result.Name);
    }

    [Fact]
    public async Task DeleteCategoryAsync_RemovesCategoryFromDatabase()
    {
        //Arrange
        using var context = CreateInMemoryDb();
        var repository = new CategoryRepository(context);
        var category = new CategoryEntity { Name = "Category to Delete" };
        context.CategoryEntities.Add(category);
        await context.SaveChangesAsync();

        //Act
        var result = await repository.DeleteCategoryAsync(category.Id, CancellationToken.None);

        //Assert
        Assert.True(result);
        Assert.Null(await context.CategoryEntities.FindAsync(category.Id));
    }
}