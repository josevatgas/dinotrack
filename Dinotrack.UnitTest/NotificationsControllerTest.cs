using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinotrack.UnitTest
{
    [TestClass]
    public class NotificationsControllerTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Notification>> _unitOfWorkMock;
        private readonly Mock<IUserHelper> _userHelperMock;
        private readonly Mock<IMailHelper> _mailHelperMock;

        public NotificationsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Notification>>();
            _userHelperMock = new Mock<IUserHelper>();
            _mailHelperMock = new Mock<IMailHelper>();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            var user = new User();
            var message = "Invalid token";
            var token = "token";
            var identityErrors = new List<IdentityError> { new IdentityError { Description = message } };

            _mockUserHelper.Setup(x => x.GetUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);
            using var context = new DataContext(_options);
            var controller = new NotificationsController(_userHelperMock.Object, _unitOfWorkMock.Object, context, _mailHelperMock.Object);
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
            var controller = new NotificationsController(_userHelperMock.Object, _unitOfWorkMock.Object, context, _mailHelperMock.Object);
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
            var controller = new NotificationsController(_userHelperMock.Object, _unitOfWorkMock.Object, context, _mailHelperMock.Object);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetCountAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new NotificationsController(_userHelperMock.Object, _unitOfWorkMock.Object, context, _mailHelperMock.Object);

            // Act
            var result = await controller.GetCountAsync() as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
    }
}
