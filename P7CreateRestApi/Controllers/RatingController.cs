using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
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
        /// <response code="200">Returns the list of RatingDTOs</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [ProducesResponseType(typeof(List<RatingDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Ratings.");
                var ratings = await _ratingService.GetAll();
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
        /// <param name="dto">The Rating dto to create</param>
        /// <returns>The created RatingDTO</returns>
        /// <response code="201">Returns the newly created Rating</response>
        /// <response code="400">If the dto is invalid</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(RatingDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] RatingDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Adding a new Rating.");
                var createdRating = await _ratingService.Create(dto);
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
        /// <response code="200">Returns the RatingDTO</response>
        /// <response code="404">If the Rating with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RatingDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching Rating with ID {Id}.", id);
                var rating = await _ratingService.GetById(id);
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
        /// <param name="dto">The Rating dto with updated values</param>
        /// <returns>The updated RatingDTO</returns>
        /// <response code="200">Returns the updated RatingDTO</response>
        /// <response code="404">If the Rating with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(RatingDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] RatingDTO dto)
        {
            try
            {
                _logger.LogInformation("Updating Rating with ID {Id}.", id);
                var updatedRating = await _ratingService.Update(id, dto);
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
        /// <response code="204">If the Rating is successfully deleted</response>
        /// <response code="404">If the Rating with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Rating with ID {Id}.", id);
                var deletedRating = await _ratingService.Delete(id);
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
