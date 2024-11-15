using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    public class UserControllerGetAllTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerGetAllTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockUserService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithUserList()
        {
            // Arrange
            var users = new List<UserDTO>
            {
                new UserDTO { Id = 1, Username = "User1" },
                new UserDTO { Id = 2, Username = "User2" }
            };
            _mockUserService.Setup(service => service.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<UserDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoUsersFound()
        {
            // Arrange
            var users = new List<UserDTO>(); // Empty list
            _mockUserService.Setup(service => service.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<UserDTO>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetAll_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An internal error occurred.", statusCodeResult.Value);
        }
    }
}
