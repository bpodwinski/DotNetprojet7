using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("rulename")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameService _ruleNameService;

        public RuleNameController(IRuleNameService ruleNameService)
        {
            _ruleNameService = ruleNameService;
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
                var ruleNames = await _ruleNameService.ListAsync();
                return Ok(ruleNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var createdRuleName = await _ruleNameService.CreateAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = createdRuleName.Id }, createdRuleName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var ruleName = await _ruleNameService.GetByIdAsync(id);
                if (ruleName is not null)
                {
                    return Ok(ruleName);
                }
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var updatedRuleName = await _ruleNameService.UpdateByIdAsync(id, model);
                if (updatedRuleName is not null)
                {
                    return Ok(updatedRuleName);
                }
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
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
                var deletedRuleName = await _ruleNameService.DeleteByIdAsync(id);
                if (deletedRuleName is not null)
                {
                    return NoContent();
                }
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
