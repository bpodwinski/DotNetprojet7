using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("bidlist")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;
        private readonly ILogger<BidListController> _logger;

        public BidListController(IBidListService bidListService, ILogger<BidListController> logger)
        {
            _bidListService = bidListService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all BidList items.
        /// </summary>
        /// <remarks>
        /// This method returns all BidList entities in the system.
        /// </remarks>
        /// <returns>A list of BidListDTOs</returns>
        /// <response code="200">Returns the list of BidListDTOs</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpGet]
        [Authorize(policy: "User")]
        [ProducesResponseType(typeof(List<BidListDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all BidList items.");
                var bidLists = await _bidListService.GetAll();
                return Ok(bidLists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching BidList items.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a specific BidList by ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to retrieve</param>
        /// <returns>A BidListDTO corresponding to the ID</returns>
        /// <response code="200">Returns the BidListDTO</response>
        /// <response code="404">If the BidList with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpGet]
        [Route("{id}")]
        [Authorize(policy: "User")]
        [ProducesResponseType(typeof(BidListDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Fetching BidList item with ID {Id}.", id);
                var bidList = await _bidListService.GetById(id);

                if (bidList is not null)
                {
                    return Ok(bidList);
                }

                _logger.LogWarning("BidList item with ID {Id} not found.", id);
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching BidList item with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Creates a new BidList.
        /// </summary>
        /// <param name="dto">The BidListDTO object to create</param>
        /// <returns>The newly created BidListDTO</returns>
        /// <response code="201">Returns the newly created BidListDTO</response>
        /// <response code="400">If the model is invalid</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpPost]
        [Authorize(policy: "Admin")]
        [ProducesResponseType(typeof(BidListDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] BidListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a BidList.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating a new BidList item.");
                var createdBidList = await _bidListService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdBidList.BidListId }, createdBidList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a BidList item.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates a specific BidList.
        /// </summary>
        /// <param name="id">The ID of the BidList to update</param>
        /// <param name="dto">The BidListDTO object with updated values</param>
        /// <returns>The updated BidListDTO</returns>
        /// <response code="200">Returns the updated BidListDTO</response>
        /// <response code="404">If the BidList with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(policy: "Admin")]
        [ProducesResponseType(typeof(BidListDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BidListDTO dto)
        {
            try
            {
                _logger.LogInformation("Updating BidList item with ID {Id}.", id);
                var updatedBidList = await _bidListService.Update(id, dto);
                if (updatedBidList is not null)
                {
                    return Ok(updatedBidList);
                }
                _logger.LogWarning("BidList item with ID {Id} not found for update.", id);
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating BidList item with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a specific BidList by ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to delete</param>
        /// <response code="204">If the BidList is successfully deleted</response>
        /// <response code="404">If the BidList with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(policy: "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Deleting BidList item with ID {Id}.", id);
                var deletedBidList = await _bidListService.DeleteById(id);
                if (deletedBidList is not null)
                {
                    return NoContent();
                }
                _logger.LogWarning("BidList item with ID {Id} not found for deletion.", id);
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting BidList item with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
