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
    /// <summary>
    /// Constructor for the authentication controller.
    /// </summary>
    /// <param name="userManager">User manager to handle user authentication and validation.</param>
    /// <param name="config">Application configuration (used for accessing JWT settings).</param>
    /// <param name="logger">Logging service to record events and errors.</param>
    [ApiController]
    [Route("auth")]
    public class AuthController(UserManager<User> userManager, IConfiguration config, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _config = config;
        private readonly ILogger<AuthController> _logger = logger;

        /// <summary>
        /// Authenticates a user based on provided credentials and generates a JWT token if valid.
        /// </summary>
        /// <param name="model">The model containing the username and password.</param>
        /// <returns>Returns a JWT token if authentication is successful, or a 401/500 error otherwise.</returns>
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
                    return Unauthorized(new { message = "Invalid username" });
                }

                // Verify the password
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (result)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                        [
                            new (ClaimTypes.Name, user.UserName),
                            new (ClaimTypes.Role, user.Role)
                        ]),
                        Expires = DateTime.UtcNow.AddHours(1),
                        Audience = _config["Jwt:Audience"],
                        Issuer = _config["Jwt:Issuer"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                else
                {
                    _logger.LogWarning("Invalid password");
                    return Unauthorized(new { message = "Invalid username or password" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occurred");
                return StatusCode(500, "An internal error occurred");
            }
        }
    }
}
