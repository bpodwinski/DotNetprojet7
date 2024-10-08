using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("rating")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly ILogger<RatingController> _logger;

        public RatingController(IRatingService ratingService, ILogger<RatingController> logger)
        {
            _ratingService = ratingService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all Rating items.
        /// </summary>
        /// <returns>A list of RatingDTOs</returns>
        [Authorize(policy: "User")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.LogInformation("Fetching all Ratings.");
                var ratings = await _ratingService.ListAsync();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Ratings.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Adds a new Rating.
        /// </summary>
        /// <param name="model">The Rating model to create</param>
        /// <returns>The created RatingDTO</returns>
        [Authorize(policy: "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingModel model)
        {
            try
            {
                _logger.LogInformation("Adding a new Rating.");
                var createdRating = await _ratingService.CreateAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = createdRating.Id }, createdRating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a Rating.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to retrieve</param>
        /// <returns>The RatingDTO</returns>
        [Authorize(policy: "User")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching Rating with ID {Id}.", id);
                var rating = await _ratingService.GetByIdAsync(id);
                if (rating is not null)
                {
                    return Ok(rating);
                }
                _logger.LogWarning("Rating with ID {Id} not found.", id);
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Rating with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates an existing Rating.
        /// </summary>
        /// <param name="id">The ID of the Rating to update</param>
        /// <param name="model">The Rating model with updated values</param>
        /// <returns>The updated RatingDTO</returns>
        [Authorize(policy: "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingModel model)
        {
            try
            {
                _logger.LogInformation("Updating Rating with ID {Id}.", id);
                var updatedRating = await _ratingService.UpdateByIdAsync(id, model);
                if (updatedRating is not null)
                {
                    return Ok(updatedRating);
                }
                _logger.LogWarning("Rating with ID {Id} not found.", id);
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Rating with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to delete</param>
        /// <returns>No content if successful</returns>
        [Authorize(policy: "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Rating with ID {Id}.", id);
                var deletedRating = await _ratingService.DeleteByIdAsync(id);
                if (deletedRating is not null)
                {
                    return NoContent();
                }
                _logger.LogWarning("Rating with ID {Id} not found for deletion.", id);
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Rating with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
