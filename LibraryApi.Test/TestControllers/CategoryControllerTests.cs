using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Controllers;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryApi.Test.TestControllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockService.Object);
        }

        //[Fact]
        //public async Task GetAll_ReturnsListOfCategories()
        //{
        //    var categories = new List<CategoryEntity> { new CategoryEntity { Name = "Category 1" } };
        //    _mockService.Setup(service => service.GetAllAsync(It.IsAny<CategoryFiltersModel>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(categories);

        //    var result = await _controller.GetAll(new CategoryFiltersModel(), CancellationToken.None) as ActionResult<List<CategoryEntity>>;

        //    Assert.IsType<OkObjectResult>(result.Result);
        //    var okResult = result.Result as OkObjectResult;
        //    Assert.IsType<List<CategoryEntity>>(okResult.Value);
        //}

        [Fact]
        public async Task GetCategoryById_ReturnsCategory()
        {
            var category = new CategoryEntity { Id = 1, Name = "Category" };
            _mockService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            var result = await _controller.GetCategoryById(1, CancellationToken.None) as ActionResult<CategoryEntity>;

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(category, okResult.Value);
        }


        [Fact]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            var category = new CategoryEntity { Name = "New Category" };
            _mockService.Setup(service => service.CreateAsync(category, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            var result = await _controller.CreateCategory(category, CancellationToken.None) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(category, result.Value);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsUpdatedCategory()
        {
            var category = new CategoryEntity { Id = 1, Name = "Updated Category" };
            _mockService.Setup(service => service.UpdateAsync(1, category, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            var result = await _controller.UpdateCategory(1, category, CancellationToken.None) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(category, result.Value);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNoContent_WhenSuccessful()
        {
            _mockService.Setup(service => service.DeleteAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _controller.DeleteCategory(1, CancellationToken.None);

            Assert.IsType<NoContentResult>(result);
        }
    }
}