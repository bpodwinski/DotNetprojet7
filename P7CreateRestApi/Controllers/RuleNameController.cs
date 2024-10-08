using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

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
        /// <response code="200">Returns the list of RuleNameDTOs</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [ProducesResponseType(typeof(List<RuleNameDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.LogInformation("Fetching all RuleNames.");
                var ruleNames = await _ruleNameService.GetAll();
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
        /// <param name="dto">The RuleNameDTO to create</param>
        /// <returns>The created RuleNameDTO</returns>
        /// <response code="201">Returns the newly created RuleNameDTO</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(RuleNameDTO), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddRuleName([FromBody] RuleNameDTO dto)
        {
            try
            {
                _logger.LogInformation("Adding a new RuleName.");
                var createdRuleName = await _ruleNameService.Create(dto);
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
        /// <response code="200">Returns the RuleNameDTO</response>
        /// <response code="404">If the RuleName with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RuleNameDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching RuleName with ID {Id}.", id);
                var ruleName = await _ruleNameService.GetById(id);
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
        /// <param name="dto">The RuleName dto with updated values</param>
        /// <returns>The updated RuleNameDTO</returns>
        /// <response code="200">Returns the updated RuleNameDTO</response>
        /// <response code="404">If the RuleName with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(RuleNameDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleNameDTO dto)
        {
            try
            {
                _logger.LogInformation("Updating RuleName with ID {Id}.", id);
                var updatedRuleName = await _ruleNameService.Update(id, dto);
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
        /// <response code="204">If the RuleName is successfully deleted</response>
        /// <response code="404">If the RuleName with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            try
            {
                _logger.LogInformation("Deleting RuleName with ID {Id}.", id);
                var deletedRuleName = await _ruleNameService.DeleteById(id);
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
