using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Controller to handle authentication operations, including JWT generation, refresh token management, and token revocation.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userManager">The UserManager for managing users.</param>
        /// <param name="config">The configuration service to access app settings.</param>
        /// <param name="logger">The logger for logging authentication-related events.</param>
        /// <param name="authService">The authentication service to manage tokens.</param>
        public AuthController(UserManager<User> userManager, IConfiguration config, ILogger<AuthController> logger, IAuthService authService)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token and refresh token if the credentials are valid.
        /// </summary>
        /// <param name="model">The login model containing the username and password.</param>
        /// <returns>A JWT token, refresh token, and the user ID if authentication is successful, or a 401/500 error otherwise.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    _logger.LogWarning("Invalid username");
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isPasswordValid)
                {
                    _logger.LogWarning("Invalid password");
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                var userClaims = await _userManager.GetClaimsAsync(user);
                var token = _authService.GenerateToken(user, userClaims);
                var refreshToken = _authService.GenerateRefreshToken();
                await _authService.AddRefreshToken(user, refreshToken);

                return Ok(new { Token = token, RefreshToken = refreshToken, UserId = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occurred during login");
                return StatusCode(500, new { message = "An internal error occurred" });
            }
        }

        /// <summary>
        /// Refreshes the JWT token using the refresh token.
        /// </summary>
        /// <param name="model">The token refresh model containing the expired JWT token and refresh token.</param>
        /// <returns>A new JWT token and refresh token if the refresh is successful.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRefresh model)
        {
            var principal = _authService.GetPrincipalFromExpiredToken(model.Token);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid user" });
            }

            var storedRefreshToken = await _authService.GetRefreshToken(user);
            var refreshTokenExpiry = await _authService.GetRefreshTokenExpiry(user);

            if (storedRefreshToken != model.RefreshToken || refreshTokenExpiry <= DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var newJwtToken = _authService.GenerateToken(user, userClaims);
            var newRefreshToken = _authService.GenerateRefreshToken();

            await _authService.AddRefreshToken(user, newRefreshToken);

            return Ok(new { Token = newJwtToken, RefreshToken = newRefreshToken });
        }

        /// <summary>
        /// Revokes the refresh token for the current user.
        /// </summary>
        /// <returns>A message confirming the refresh token revocation.</returns>
        [HttpPost]
        [Authorize]
        [Route("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest(new { message = "User not found" });

            await _authService.RevokeRefreshToken(user);
            return Ok(new { message = "Refresh token revoked" });
        }
    }
}
