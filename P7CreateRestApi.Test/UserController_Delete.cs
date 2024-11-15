using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Test
{
    public class UserControllerDeleteTest
    {
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerDeleteTest()
        {
            _mockService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            var mockUser = new UserDTO { Id = 1, Username = "UserToDelete" };

            _mockService.Setup(service => service.DeleteById(1, It.IsAny<ClaimsPrincipal>())).ReturnsAsync(mockUser);

            // Simulate an admin user with the required claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_SelfDeletionByNonAdminUser()
        {
            // Arrange
            var currentUserId = 1;

            _mockService.Setup(service => service.DeleteById(currentUserId, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new UserDTO { Id = currentUserId, Username = "UserToDelete" });

            // Simulate a regular user deleting their own account
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, currentUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(currentUserId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_Forbidden_UserAttemptsToDeleteAnotherAccount()
        {
            // Arrange
            var userId = 1;
            var targetUserId = 2;

            // Simulate an user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(targetUserId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Delete_AdminDeletesAnotherUser()
        {
            // Arrange
            var adminId = 1;
            var targetUserId = 2;

            _mockService.Setup(service => service.DeleteById(targetUserId, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new UserDTO { Id = targetUserId, Username = "UserToDelete" });

            // Simulate an admin user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, adminId.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(targetUserId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_Forbidden_WhenUnauthorizedAccessExceptionIsThrown()
        {
            // Arrange
            var targetUserId = 2;

            // Simulate the service throwing an UnauthorizedAccessException
            _mockService.Setup(service => service.DeleteById(targetUserId, It.IsAny<ClaimsPrincipal>()))
                .ThrowsAsync(new UnauthorizedAccessException("Unauthorized deletion attempt by user ID"));

            // Simulate an user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(targetUserId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Delete_UserNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var targetUserId = 999;

            // Simulate the service returning null when attempting to delete a non-existent user
            _mockService.Setup(service => service.DeleteById(targetUserId, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((UserDTO)null);

            // Simulate an admin user with claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(targetUserId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Delete_InternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteById(1, It.IsAny<ClaimsPrincipal>()))
                .ThrowsAsync(new Exception("An internal server error occurred."));

            // Simulate an admin user with the required claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
            };

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
