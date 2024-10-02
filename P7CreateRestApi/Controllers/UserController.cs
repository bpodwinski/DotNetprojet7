using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> List()
        {
            try
            {
                var users = await _userService.ListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        [HttpPost]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.CreateAsync(inputModel);
                if (user is not null)
                {
                    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
                }
                return BadRequest("Unable to create user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var user = _userService.GetByIdAsync(id);
                if (user is not null)
                {
                    return Ok(user);
                }
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a user's details by ID.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.UpdateByIdAsync(id, inputModel);
                if (user is not null)
                {
                    return Ok(user);
                }
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.DeleteByIdAsync(id);
                if (user is not null)
                {
                    return NoContent();
                }
                return NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
