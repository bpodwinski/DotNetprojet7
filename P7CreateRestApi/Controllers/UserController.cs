using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using System.Security.Claims;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all users.");
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching users.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        [HttpPost]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Create([FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for adding a user.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Adding a new user.");
                var user = await _userService.Create(dto);
                if (user is not null)
                {
                    _logger.LogInformation("User created with ID {Id}.", user.Id);
                    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
                }
                _logger.LogWarning("Unable to create user.");
                return BadRequest("Unable to create user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(policy: "Admin")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching user with ID {Id}.", id);
                var user = _userService.GetById(id);
                if (user is not null)
                {
                    return Ok(user);
                }
                _logger.LogWarning("User with ID {Id} not found.", id);
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates a user's details by ID.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating user with ID {Id}.", id);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Updating user with ID {Id}.", id);
                var user = await _userService.Update(id, dto);
                if (user is not null)
                {
                    return Ok(user);
                }
                _logger.LogWarning("User with ID {Id} not found for update.", id);
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

                if (id == currentUserId)
                {
                    _logger.LogWarning("User with ID {Id} attempted to delete their own account.", currentUserId);
                    return StatusCode(503, "You cannot delete your own account.");
                }

                _logger.LogInformation("Deleting user with ID {Id}.", id);
                var user = await _userService.DeleteById(id);
                if (user is not null)
                {
                    return NoContent();
                }
                _logger.LogWarning("User with ID {Id} not found for deletion.", id);
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
