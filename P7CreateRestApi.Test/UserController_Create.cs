using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    public class UserControllerCreateTest
    {
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerCreateTest()
        {
            _mockService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Username = "NewUserTest" };
            _mockService.Setup(service => service.Create(It.IsAny<UserDTO>())).ReturnsAsync(userDto);

            // Act
            var result = await _controller.Create(userDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<UserDTO>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        /// <summary>
        /// Tests if Create returns BadRequestResult when the model state is invalid.
        /// </summary>
        [Fact]
        public async Task Create_ModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "The Username field is required.");

            // Act
            var result = await _controller.Create(new UserDTO { Id = 1, Username = "NewUserTest" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_WhenServicesNull()
        {
            // Arrange
            var newUserDto = new UserDTO { Id = 1, Username = "NewUserTest" };
            _mockService.Setup(service => service.Create(It.IsAny<UserDTO>())).ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.Create(newUserDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Unable to create user.", badRequestResult.Value);
        }

        /// <summary>
        /// Tests if Create returns 500 Internal Server Error when an exception is thrown in the service.
        /// </summary>
        [Fact]
        public async Task Create_InternalServerError()
        {
            // Arrange
            var newUserDto = new UserDTO { Id = 1, Username = "NewUserTest" };
            _mockService.Setup(service => service.Create(It.IsAny<UserDTO>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Create(newUserDto);

            // Assert
            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverErrorResult.StatusCode);
        }
    }
}
