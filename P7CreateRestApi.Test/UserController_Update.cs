using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using System.Security.Claims;
using Xunit;

namespace P7CreateRestApi.Test
{
    public class UserControllerUpdateTest
    {
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerUpdateTest()
        {
            _mockService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Update_Ok()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Username = "UpdatedUser" };
            _mockService.Setup(service => service.Update(1, It.IsAny<UserDTO>(), It.IsAny<ClaimsPrincipal>())).ReturnsAsync(userDto);

            // Act
            var result = await _controller.Update(1, userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Update_ModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "The Username field is required.");

            // Act
            var result = await _controller.Update(1, new UserDTO { Id = 1 });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_NotFound()
        {
            // Arrange
            var userDto = new UserDTO { Id = 999, Username = "NonExistentUser" };
            _mockService.Setup(service => service.Update(999, It.IsAny<UserDTO>(), It.IsAny<ClaimsPrincipal>())).ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.Update(999, userDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateForbidden_UnauthorizedAccessException()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Username = "UnauthorizedUser" };
            _mockService.Setup(service => service.Update(1, It.IsAny<UserDTO>(), It.IsAny<ClaimsPrincipal>()))
                .ThrowsAsync(new UnauthorizedAccessException("You do not have permission to perform this action."));

            // Act
            var result = await _controller.Update(1, userDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, statusCodeResult.StatusCode);
            Assert.Equal("You do not have permission to perform this action.", statusCodeResult.Value);
        }

        [Fact]
        public async Task Update_ArgumentException()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Username = "InvalidUser" };
            _mockService.Setup(service => service.Update(1, It.IsAny<UserDTO>(), It.IsAny<ClaimsPrincipal>()))
                .ThrowsAsync(new ArgumentException("Invalid user ID or data."));

            // Act
            var result = await _controller.Update(1, userDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid user ID or data.", badRequestResult.Value);
        }

        [Fact]
        public async Task Update_InternalServerError()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Username = "UserWithError" };
            _mockService.Setup(service => service.Update(1, It.IsAny<UserDTO>(), It.IsAny<ClaimsPrincipal>()))
                .ThrowsAsync(new Exception("An internal error occurred."));

            // Act
            var result = await _controller.Update(1, userDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
