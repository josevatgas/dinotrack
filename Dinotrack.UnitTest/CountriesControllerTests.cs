using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Dinotrack.UnitTest.Controllers
{
    [TestClass]
    public class CountriesControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Country>> _unitOfWorkMock;

        public CountriesControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Country>>();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetCombo() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Filter = "some" };

            // Act
            var result = await controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Filter = "some" };

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenCountryNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkWhenCountryFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var country = new Country { Id = 1, Name = "test" };
            _unitOfWorkMock.Setup(x => x.GetCountryAsync(country.Id)).ReturnsAsync(country);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(country.Id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetCountryAsync(country.Id), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
    }
}