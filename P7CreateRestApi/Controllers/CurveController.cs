using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

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
        /// <response code="200">Returns the list of CurvePointDTOs</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CurvePointDTO>), 200)]
        [ProducesResponseType(500)]
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
        /// <param name="CurvePointDTO">The CurvePoint model to create</param>
        /// <returns>The created CurvePointDTO</returns>
        /// <response code="201">Returns the newly created CurvePoint</response>
        /// <response code="400">If the model is invalid</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(CurvePointDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePointDTO CurvePointDTO)
        {
            try
            {
                _logger.LogInformation("Adding a new CurvePoint.");
                var createdCurvePoint = await _curvePointService.CreateAsync(CurvePointDTO);
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
        /// <response code="200">Returns the CurvePointDTO</response>
        /// <response code="404">If the CurvePoint with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "User")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CurvePointDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        /// <param name="CurvePointDTO">The CurvePoint model with updated values</param>
        /// <returns>The updated CurvePointDTO</returns>
        /// <response code="200">Returns the updated CurvePointDTO</response>
        /// <response code="404">If the CurvePoint with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CurvePointDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePointDTO CurvePointDTO)
        {
            try
            {
                _logger.LogInformation("Updating CurvePoint with ID {Id}.", id);
                var updatedCurvePoint = await _curvePointService.UpdateByIdAsync(id, CurvePointDTO);
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
        /// <response code="204">If the CurvePoint is successfully deleted</response>
        /// <response code="404">If the CurvePoint with the specified ID is not found</response>
        /// <response code="500">If an internal error occurs</response>
        [Authorize(policy: "Admin")]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
