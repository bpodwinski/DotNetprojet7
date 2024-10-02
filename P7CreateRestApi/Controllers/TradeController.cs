using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("trade")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        /// <summary>
        /// Retrieves all Trade items.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var trades = await _tradeService.ListAsync();
                return Ok(trades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var trade = await _tradeService.GetByIdAsync(id);
                if (trade is null)
                {
                    return NotFound($"Trade with ID {id} not found.");
                }
                return Ok(trade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var createdTrade = await _tradeService.CreateAsync(model);
                if (createdTrade is null)
                {
                    return BadRequest("Trade could not be created.");
                }

                return CreatedAtAction(nameof(GetById), new { id = createdTrade.TradeId }, createdTrade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var updatedTrade = await _tradeService.UpdateByIdAsync(id, model);
                if (updatedTrade is null)
                {
                    return NotFound($"Trade with ID {id} not found.");
                }
                return Ok(updatedTrade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var deletedTrade = await _tradeService.DeleteByIdAsync(id);
                if (deletedTrade is null)
                {
                    return NotFound($"Trade with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
