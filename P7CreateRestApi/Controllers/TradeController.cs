using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

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
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.LogInformation("Fetching all trades.");
                var trades = await _tradeService.ListAsync();
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching trade with ID {Id}.", id);
                var trade = await _tradeService.GetByIdAsync(id);
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TradeModel model)
        {
            try
            {
                _logger.LogInformation("Creating a new trade.");
                var createdTrade = await _tradeService.CreateAsync(model);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] TradeModel model)
        {
            try
            {
                _logger.LogInformation("Updating trade with ID {Id}.", id);
                var updatedTrade = await _tradeService.UpdateByIdAsync(id, model);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                _logger.LogInformation("Deleting trade with ID {Id}.", id);
                var deletedTrade = await _tradeService.DeleteByIdAsync(id);
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
