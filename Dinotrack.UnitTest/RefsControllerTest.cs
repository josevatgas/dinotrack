using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace Dinotrack.UnitTest
{
    [TestClass]
    public class RefsControllerTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Ref>> _unitOfWorkMock;
        private readonly Mock<IFileStorage> _fileStorage;
        private readonly Mock<RefDTO> _refDTO;
        private readonly RefsController _controller;

        public RefsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Ref>>();
            _fileStorage = new Mock<IFileStorage>();
            _refDTO = new Mock<RefDTO>();
        }

        [TestMethod]
        public async Task PutFullAsync_Success_ReturnsOkObjectResult()
        {
            // Arrange
            var refe = new Ref();
            refe.Id = 1;
            _unitOfWorkMock.Setup(x => x.UpdateAsync(refe))
                .ReturnsAsync(new Response<Ref> { WasSuccess = true, Result = new Ref() });
            var refDTO = new RefDTO
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
            };

            // Act
            var result = await _controller.PutFullAsync(refDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            _unitOfWorkMock.Verify(x => x.UpdateAsync(refe), Times.Once());
        }

        [TestMethod]
        public async Task PutFullAsync_Failure_ReturnsNotFoundObjectResult()
        {
            // Arrange
            var refe = new Ref();
            refe.Id = 1;
            _unitOfWorkMock.Setup(x => x.UpdateAsync(refe))
                .ReturnsAsync(new Response<Ref> { WasSuccess = true, Result = new Ref() });
            var refDTO = new RefDTO
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
            };

            // Act
            var result = await _controller.PutFullAsync(refDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _unitOfWorkMock.Verify(x => x.UpdateAsync(refe), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new RefsController(_unitOfWorkMock.Object, context, _fileStorage.Object);
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
            var controller = new RefsController(_unitOfWorkMock.Object, context, _fileStorage.Object);
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
            var controller = new RefsController(_unitOfWorkMock.Object, context, _fileStorage.Object);

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