using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    public class UserControllerGetByIdTest
    {
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerGetByIdTest()
        {
            _mockService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            // Arrange
            int userId = 99;
            _mockService.Setup(service => service.GetById(userId)).ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetById_UserExists()
        {
            // Arrange
            var mockUser = new UserDTO { Id = 1, Username = "ExistingUser" };
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(mockUser);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(mockUser.Id, returnValue.Id);
            Assert.Equal(mockUser.Username, returnValue.Username);
        }


        /// <summary>
        /// Tests if GetById returns 500 Internal Server Error when an exception occurs.
        /// </summary>
        [Fact]
        public async Task GetById_InternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(1)).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
