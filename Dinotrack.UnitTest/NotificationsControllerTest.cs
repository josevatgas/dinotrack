using Azure;
using Dinotrack.Backend.Controllers;
using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly NotificationsController _controller = null!;
        private const string _string64base = "U29tZVZhbGlkQmFzZTY0U3RyaW5n";
        public NotificationsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Notification>>();
            _userHelperMock = new Mock<IUserHelper>();
            _mailHelperMock = new Mock<IMailHelper>();
            var context = new DataContext(_options);
            _controller = new NotificationsController(_userHelperMock.Object, _unitOfWorkMock.Object, context, _mailHelperMock.Object);
        }

        private void SetupUser(string username)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, username)
            }, "mock"));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequest()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                Password = "password123",
                Photo = _string64base,
                Address = "Some",
                CityId = 1,
                Document = "Any",
                Email = "Some",
                FirstName = "Test",
                Id = "123",
                LastName = "Test",
                PasswordConfirm = "password123",
                PhoneNumber = "Any",
                UserName = "Test",
                UserType = UserType.User
            };
            var user = new User();  
            SetupUser("Some");
            var notification = new Notification();
            var paginationDto = new PaginationDTO();
            _userHelperMock.Setup(x => x.AddUserAsync(It.IsAny<User>(), userDTO.Password))
                .ReturnsAsync(IdentityResult.Success);
            _userHelperMock.Setup(x => x.AddUserToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            _userHelperMock.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("token");
          

            // Act
            var result = await _controller.GetAsync(paginationDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

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

    
    }
}
