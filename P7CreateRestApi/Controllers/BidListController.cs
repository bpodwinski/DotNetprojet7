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

        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
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
        public async Task<IActionResult> List()
        {
            try
            {
                var bidLists = await _bidListService.GetAll();
                return Ok(bidLists);
            }
            catch (Exception)
            {
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
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var bidList = await _bidListService.GetById(id);
                if (bidList is not null)
                {
                    return Ok(bidList);
                }
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new BidList.
        /// </summary>
        /// <param name="dto">The BidListDTO object to create</param>
        /// <returns>The newly created BidListDTO</returns>
        /// <response code="201">Returns the newly created BidListDTO</response>
        /// <response code="500">If an internal error occurs</response>
        [HttpPost]
        [Authorize(policy: "User")]
        [ProducesResponseType(typeof(BidListDTO), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddBidList([FromBody] BidListDTO dto)
        {
            try
            {
                var createdBidList = await _bidListService.Create(dto);
                return CreatedAtAction(nameof(Get), new { id = createdBidList.BidListId }, createdBidList);
            }
            catch (Exception)
            {
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
        [Authorize(policy: "User")]
        [ProducesResponseType(typeof(BidListDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] BidListDTO dto)
        {
            try
            {
                var updatedBidList = await _bidListService.UpdateById(id, dto);
                if (updatedBidList is not null)
                {
                    return Ok(updatedBidList);
                }
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
        [Authorize(policy: "User")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            try
            {
                var deletedBidList = await _bidListService.DeleteById(id);
                if (deletedBidList is not null)
                {
                    return NoContent();
                }
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
