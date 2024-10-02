using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("curve")]
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurveController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        /// <summary>
        /// Retrieves all CurvePoints.
        /// </summary>
        /// <returns>A list of CurvePointDTOs</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var curvePoints = await _curvePointService.ListAsync();
                return Ok(curvePoints);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new CurvePoint.
        /// </summary>
        /// <param name="curvePointModel">The CurvePoint model to create</param>
        /// <returns>The created CurvePointDTO</returns>
        [HttpPost]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePointModel curvePointModel)
        {
            try
            {
                var createdCurvePoint = await _curvePointService.CreateAsync(curvePointModel);
                return CreatedAtAction(nameof(GetById), new { id = createdCurvePoint.Id }, createdCurvePoint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var curvePoint = await _curvePointService.GetByIdAsync(id);
                if (curvePoint is not null)
                {
                    return Ok(curvePoint);
                }
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing CurvePoint.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="curvePointModel">The CurvePoint model with updated values</param>
        /// <returns>The updated CurvePointDTO</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePointModel curvePointModel)
        {
            try
            {
                var updatedCurvePoint = await _curvePointService.UpdateByIdAsync(id, curvePointModel);
                if (updatedCurvePoint is not null)
                {
                    return Ok(updatedCurvePoint);
                }
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            try
            {
                var deletedCurvePoint = await _curvePointService.DeleteByIdAsync(id);
                if (deletedCurvePoint is not null)
                {
                    return NoContent();
                }
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
