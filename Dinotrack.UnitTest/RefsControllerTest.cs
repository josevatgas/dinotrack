using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Dinotrack.UnitTest
{
    [TestClass]
    public class RefsControllerTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Ref>> _unitOfWorkMock;
        private readonly Mock<IFileStorage> _fileStorage;

        public RefsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Ref>>();
            _fileStorage = new Mock<IFileStorage>();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new RefsController(_unitOfWorkMock.Object, context, _fileStorage.Object);
            var stateId = 1;

            // Act
            var result = await controller.PutFullAsync(stateId) as OkObjectResult;

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
            var controller = new CitiesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Id = 1, Filter = "Some" };

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
            var controller = new CitiesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Id = 1, Filter = "Some" };

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenStateNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CitiesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
    }
}