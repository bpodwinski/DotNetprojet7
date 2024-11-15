using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit tests for the <see cref="AuthController"/> class.
    /// </summary>
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly Mock<IAuthService> _authServiceMock;

        private readonly AuthController _authController;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthControllerTests"/> class.
        /// Sets up all the dependencies and mock objects for testing.
        /// </summary>
        public AuthControllerTests()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _authServiceMock = new Mock<IAuthService>();

            _authController = new AuthController(
                _userManagerMock.Object,
                _configurationMock.Object,
                _loggerMock.Object,
                _authServiceMock.Object
            );
        }

        /// <summary>
        /// Represents the response model returned by the login operation.
        /// </summary>
        public class LoginResponse
        {
            /// <summary>
            /// Gets or sets the JWT token generated upon successful login.
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// Gets or sets the refresh token generated upon successful login.
            /// </summary>
            public string RefreshToken { get; set; }

            /// <summary>
            /// Gets or sets the ID of the user who successfully logged in.
            /// </summary>
            public int UserId { get; set; }
        }

        /// <summary>
        /// Tests the login functionality with valid credentials.
        /// Ensures that a successful login returns a 200 OK response with the expected data.
        /// </summary>
        [Fact]
        public async Task Login_ValidCredentials()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "testuser", Password = "Test@123" };
            var user = new User { UserName = "testuser", Id = 123 };
            var userClaims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            var generatedToken = "valid-jwt-token";
            var refreshToken = "valid-refresh-token";

            _userManagerMock.Setup(um => um.FindByNameAsync(loginModel.Username))
                .ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginModel.Password))
                .ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetClaimsAsync(user))
                .ReturnsAsync(userClaims);
            _authServiceMock.Setup(auth => auth.GenerateToken(user, userClaims))
                .Returns(generatedToken);
            _authServiceMock.Setup(auth => auth.GenerateRefreshToken())
                .Returns(refreshToken);
            _authServiceMock.Setup(auth => auth.AddRefreshToken(user, refreshToken))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var response = JsonSerializer.Deserialize<LoginResponse>(responseJson);

            Assert.NotNull(response);
            Assert.Equal(generatedToken, response.Token);
            Assert.Equal(refreshToken, response.RefreshToken);
            Assert.Equal(user.Id, response.UserId);
        }

        /// <summary>
        /// Tests the login functionality with invalid credentials.
        /// Ensures that an unsuccessful login returns a 401 Unauthorized response.
        /// </summary>
        [Fact]
        public async Task Login_InvalidCredentials()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "wronguser", Password = "WrongPassword" };

            // Simulate the UserManager returning null because the user does not exist
            _userManagerMock.Setup(um => um.FindByNameAsync(loginModel.Username))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);

            // Use JsonSerializer to verify the structure of the response
            var responseJson = JsonSerializer.Serialize(unauthorizedResult.Value);
            var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

            Assert.NotNull(response);
            Assert.Equal("Invalid username or password", response["message"]);
        }

        [Fact]
        public async Task Login_InvalidPassword()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "testuser", Password = "WrongPassword" };
            var user = new User { UserName = "testuser", Id = 123 };

            // Simulate that a valid user is found
            _userManagerMock.Setup(um => um.FindByNameAsync(loginModel.Username))
                .ReturnsAsync(user);

            // Simulate an incorrect password
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginModel.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(unauthorizedResult.Value);
            var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

            Assert.NotNull(response);
            Assert.Equal("Invalid username or password", response["message"]);
        }

        /// <summary>
        /// Tests the login functionality when an internal exception occurs.
        /// Ensures that an internal error results in a 500 Internal Server Error response.
        /// </summary>
        [Fact]
        public async Task Login_ServerError()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "testuser", Password = "Test@123" };

            // Simulate an exception being thrown during the FindByNameAsync call
            _userManagerMock.Setup(um => um.FindByNameAsync(loginModel.Username))
                .ThrowsAsync(new System.Exception("Database connection error"));

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var serverErrorResult = Assert.IsType<ObjectResult>(result);

            // Verify that the status code is 500
            Assert.Equal(500, serverErrorResult.StatusCode);

            // Use JsonSerializer to verify the structure of the response
            var responseJson = JsonSerializer.Serialize(serverErrorResult.Value);
            var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

            Assert.NotNull(response);
            Assert.Equal("An internal error occurred", response["message"]);
        }
    }
}
