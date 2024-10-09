using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("trade")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly ILogger<TradeController> _logger;

        public TradeController(ITradeService tradeService, ILogger<TradeController> logger)
        {
            _tradeService = tradeService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all Trade items.
        /// </summary>
        /// <returns>A list of TradeDTOs</returns>
        /// <response code="200">Returns the list of TradeDTOs</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [ProducesResponseType(typeof(List<TradeDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all trades.");
                var trades = await _tradeService.GetAll();
                return Ok(trades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching trades.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a specific Trade by ID.
        /// </summary>
        /// <param name="id">The ID of the Trade to retrieve</param>
        /// <returns>The TradeDTO</returns>
        /// <response code="200">Returns the TradeDTO</response>
        /// <response code="404">If the Trade with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TradeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching trade with ID {Id}.", id);
                var trade = await _tradeService.GetById(id);
                if (trade is null)
                {
                    _logger.LogWarning("Trade with ID {Id} not found.", id);
                    return NotFound($"Trade with ID {id} not found.");
                }
                return Ok(trade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching trade with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Creates a new Trade.
        /// </summary>
        /// <param name="dto">The Trade dto to create</param>
        /// <returns>The newly created TradeDTO</returns>
        /// <response code="201">Returns the newly created TradeDTO</response>
        /// <response code="400">If the dto is invalid</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(TradeDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] TradeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating a new trade.");
                var createdTrade = await _tradeService.Create(dto);
                if (createdTrade is null)
                {
                    _logger.LogWarning("Trade could not be created.");
                    return BadRequest("Trade could not be created.");
                }

                _logger.LogInformation("Trade created with ID {Id}.", createdTrade.TradeId);
                return CreatedAtAction(nameof(GetById), new { id = createdTrade.TradeId }, createdTrade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new trade.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates an existing Trade.
        /// </summary>
        /// <param name="id">The ID of the Trade to update</param>
        /// <param name="dto">The Trade dto with updated values</param>
        /// <returns>The updated TradeDTO</returns>
        /// <response code="200">Returns the updated TradeDTO</response>
        /// <response code="404">If the Trade with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TradeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] TradeDTO dto)
        {
            try
            {
                _logger.LogInformation("Updating trade with ID {Id}.", id);
                var updatedTrade = await _tradeService.Update(id, dto);
                if (updatedTrade is null)
                {
                    _logger.LogWarning("Trade with ID {Id} not found.", id);
                    return NotFound($"Trade with ID {id} not found.");
                }
                return Ok(updatedTrade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating trade with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a specific Trade by ID.
        /// </summary>
        /// <param name="id">The ID of the Trade to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the Trade is successfully deleted</response>
        /// <response code="404">If the Trade with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting trade with ID {Id}.", id);
                var deletedTrade = await _tradeService.Delete(id);
                if (deletedTrade is null)
                {
                    _logger.LogWarning("Trade with ID {Id} not found.", id);
                    return NotFound($"Trade with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting trade with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
