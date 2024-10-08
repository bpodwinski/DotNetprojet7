using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("curve")]
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;
        private readonly ILogger<CurveController> _logger;

        public CurveController(ICurvePointService curvePointService, ILogger<CurveController> logger)
        {
            _curvePointService = curvePointService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all CurvePoints.
        /// </summary>
        /// <returns>A list of CurvePointDTOs</returns>
        [Authorize(policy: "User")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.LogInformation("Fetching all CurvePoints.");
                var curvePoints = await _curvePointService.ListAsync();
                return Ok(curvePoints);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching CurvePoints.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Adds a new CurvePoint.
        /// </summary>
        /// <param name="curvePointModel">The CurvePoint model to create</param>
        /// <returns>The created CurvePointDTO</returns>
        [Authorize(policy: "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePointModel curvePointModel)
        {
            try
            {
                _logger.LogInformation("Adding a new CurvePoint.");
                var createdCurvePoint = await _curvePointService.CreateAsync(curvePointModel);
                return CreatedAtAction(nameof(GetById), new { id = createdCurvePoint.Id }, createdCurvePoint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a CurvePoint.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO</returns>
        [Authorize(policy: "User")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching CurvePoint with ID {Id}.", id);
                var curvePoint = await _curvePointService.GetByIdAsync(id);
                if (curvePoint is not null)
                {
                    return Ok(curvePoint);
                }
                _logger.LogWarning("CurvePoint with ID {Id} not found.", id);
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching CurvePoint with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates an existing CurvePoint.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="curvePointModel">The CurvePoint model with updated values</param>
        /// <returns>The updated CurvePointDTO</returns>
        [Authorize(policy: "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePointModel curvePointModel)
        {
            try
            {
                _logger.LogInformation("Updating CurvePoint with ID {Id}.", id);
                var updatedCurvePoint = await _curvePointService.UpdateByIdAsync(id, curvePointModel);
                if (updatedCurvePoint is not null)
                {
                    return Ok(updatedCurvePoint);
                }
                _logger.LogWarning("CurvePoint with ID {Id} not found.", id);
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating CurvePoint with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>No content if successful</returns>
        [Authorize(policy: "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            try
            {
                _logger.LogInformation("Deleting CurvePoint with ID {Id}.", id);
                var deletedCurvePoint = await _curvePointService.DeleteByIdAsync(id);
                if (deletedCurvePoint is not null)
                {
                    return NoContent();
                }
                _logger.LogWarning("CurvePoint with ID {Id} not found for deletion.", id);
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting CurvePoint with ID {Id}.", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
