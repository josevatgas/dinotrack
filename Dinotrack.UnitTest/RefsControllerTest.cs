using Azure;
using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly DataContext _context;
        private readonly Mock<IGenericUnitOfWork<Brand>> _brandUnitOfWorkMock;
        private readonly string _container;

        public RefsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Ref>>();
            _fileStorage = new Mock<IFileStorage>();
            _refDTO = new Mock<RefDTO>();
            _context = new DataContext(_options);
            _controller = new RefsController(_unitOfWorkMock.Object, _context, _fileStorage.Object);
            _brandUnitOfWorkMock = new Mock<IGenericUnitOfWork<Brand>>();
            _container = "motorcycles";
        }

        [TestMethod]
        public async Task PostFullAsync_Success_ReturnsOkObjectResult()
        {
            var brand = new Brand { Id = 1, Name = "test" };
            var response = new Shared.Responses.Response<Brand> { WasSuccess = true };

            _fileStorage.Setup(x => x.SaveFileAsync(It.IsAny<byte[]>(), ".jpg", _container))
                .ReturnsAsync("photoUrl");
            _brandUnitOfWorkMock.Setup(x => x.AddAsync(brand)).ReturnsAsync(response);

            var refDTO = new RefDTO
            {
                Name = "TestReference",
                Description = "Test description",
                Model = 2023,
                BrandId = 1,
                RefImages = new List<string> { "base64Image1", "base64Image2" }
            };

            // Act
            var result = await _controller.PostFullAsync(refDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(RefDTO));
            var returnedRefDTO = okResult.Value as RefDTO;
            Assert.AreEqual(refDTO.Name, returnedRefDTO!.Name);
        }

       /* [TestMethod]
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
        }*/

      /*  [TestMethod]
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
        }*/

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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(200, result.StatusCode);

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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(200, result.StatusCode);

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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
    }
}