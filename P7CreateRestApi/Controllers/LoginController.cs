using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, IConfiguration config, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="model">The login model containing the username and password.</param>
        /// <returns>Returns a JWT token and the user ID if authentication is successful, or a 401/500 error otherwise.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Find the user by username
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    _logger.LogWarning("Invalid username");
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                // Verify the password
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isPasswordValid)
                {
                    _logger.LogWarning("Invalid password");
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new(ClaimTypes.Name, user.UserName),
                        new(ClaimTypes.Role, user.Role),
                        new(ClaimTypes.NameIdentifier, user.Id.ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Audience = _config["Jwt:Audience"],
                    Issuer = _config["Jwt:Issuer"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return the token and user ID
                return Ok(new { Token = tokenString, UserId = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occurred during login");
                return StatusCode(500, new { message = "An internal error occurred" });
            }
        }
    }
}
