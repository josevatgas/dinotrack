using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Dinotrack.UnitTest
{
    [TestClass]
    public class GenericControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Brand>> _unitOfWorkMock;

        public GenericControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Brand>>();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO();

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
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO();

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenEntityNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsRecord()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Response<Brand> { WasSuccess = true };
            _unitOfWorkMock.Setup(x => x.GetAsync(brand.Id)).ReturnsAsync(brand);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetAsync(brand.Id), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PostAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Response<Brand> { WasSuccess = true };
            _unitOfWorkMock.Setup(x => x.AddAsync(brand)).ReturnsAsync(response);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PostAsync(brand) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.AddAsync(brand), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PostAsync_ReturnsBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Response<Brand> { WasSuccess = false };
            _unitOfWorkMock.Setup(x => x.AddAsync(brand)).ReturnsAsync(response);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PostAsync(brand) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.AddAsync(brand), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
        [TestMethod]
        public async Task PutAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Response<Brand> { WasSuccess = true };
            _unitOfWorkMock.Setup(x => x.UpdateAsync(brand)).ReturnsAsync(response);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PutAsync(brand) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.UpdateAsync(brand), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PutAsync_ReturnsBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Response<Brand> { WasSuccess = false };
            _unitOfWorkMock.Setup(x => x.UpdateAsync(brand)).ReturnsAsync(response);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PutAsync(brand) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.UpdateAsync(brand), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsNoContentWhenEntityDeleted()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            _unitOfWorkMock.Setup(x => x.GetAsync(brand.Id)).ReturnsAsync(brand);
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.DeleteAsync(brand.Id) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetAsync(brand.Id), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsNoContentWhenEntityNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var brand = new Brand { Id = 1, Name = "test" };
            var controller = new GenericController<Brand>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.DeleteAsync(brand.Id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

    }
}