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

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Retrieves all Rating items.
        /// </summary>
        /// <returns>A list of RatingDTOs</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var ratings = await _ratingService.ListAsync();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new Rating.
        /// </summary>
        /// <param name="model">The Rating model to create</param>
        /// <returns>The created RatingDTO</returns>
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingModel model)
        {
            try
            {
                var createdRating = await _ratingService.CreateAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = createdRating.Id }, createdRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to retrieve</param>
        /// <returns>The RatingDTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rating = await _ratingService.GetByIdAsync(id);
                if (rating is not null)
                {
                    return Ok(rating);
                }
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing Rating.
        /// </summary>
        /// <param name="id">The ID of the Rating to update</param>
        /// <param name="model">The Rating model with updated values</param>
        /// <returns>The updated RatingDTO</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingModel model)
        {
            try
            {
                var updatedRating = await _ratingService.UpdateByIdAsync(id, model);
                if (updatedRating is not null)
                {
                    return Ok(updatedRating);
                }
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                var deletedRating = await _ratingService.DeleteByIdAsync(id);
                if (deletedRating is not null)
                {
                    return NoContent();
                }
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
