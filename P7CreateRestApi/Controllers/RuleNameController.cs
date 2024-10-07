using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("rulename")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameService _ruleNameService;
        private readonly ILogger<RuleNameController> _logger;

        public RuleNameController(IRuleNameService ruleNameService, ILogger<RuleNameController> logger)
        {
            _ruleNameService = ruleNameService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all RuleName items.
        /// </summary>
        /// <returns>A list of RuleNameDTOs</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.LogInformation("Fetching all RuleNames.");
                var ruleNames = await _ruleNameService.ListAsync();
                return Ok(ruleNames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching RuleNames.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Adds a new RuleName.
        /// </summary>
        /// <param name="model">The RuleNameModel to create</param>
        /// <returns>The created RuleNameDTO</returns>
        [HttpPost]
        public async Task<IActionResult> AddRuleName([FromBody] RuleNameModel model)
        {
            try
            {
                _logger.LogInformation("Adding a new RuleName.");
                var createdRuleName = await _ruleNameService.CreateAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = createdRuleName.Id }, createdRuleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new RuleName.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to retrieve</param>
        /// <returns>The RuleNameDTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching RuleName with ID {Id}.", id);
                var ruleName = await _ruleNameService.GetByIdAsync(id);
                if (ruleName is not null)
                {
                    return Ok(ruleName);
                }
                _logger.LogWarning("RuleName with ID {Id} not found.", id);
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching RuleName with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates an existing RuleName.
        /// </summary>
        /// <param name="id">The ID of the RuleName to update</param>
        /// <param name="model">The RuleName model with updated values</param>
        /// <returns>The updated RuleNameDTO</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleNameModel model)
        {
            try
            {
                _logger.LogInformation("Updating RuleName with ID {Id}.", id);
                var updatedRuleName = await _ruleNameService.UpdateByIdAsync(id, model);
                if (updatedRuleName is not null)
                {
                    return Ok(updatedRuleName);
                }
                _logger.LogWarning("RuleName with ID {Id} not found for update.", id);
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating RuleName with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            try
            {
                _logger.LogInformation("Deleting RuleName with ID {Id}.", id);
                var deletedRuleName = await _ruleNameService.DeleteByIdAsync(id);
                if (deletedRuleName is not null)
                {
                    return NoContent();
                }
                _logger.LogWarning("RuleName with ID {Id} not found for deletion.", id);
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting RuleName with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
