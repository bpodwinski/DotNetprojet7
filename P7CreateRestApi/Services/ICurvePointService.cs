using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for managing operations on CurvePoint entities.
    /// </summary>
    public interface ICurvePointService
    {
        /// <summary>
        /// Asynchronously retrieves all CurvePoint entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of CurvePointDTOs</returns>
        Task<List<CurvePointDTO>> ListAsync();

        /// <summary>
        /// Asynchronously creates a new CurvePoint entity based on the provided model.
        /// </summary>
        /// <param name="model">The CurvePointModel object containing the data for the new entity</param>
        /// <returns>The created CurvePointDTO</returns>
        Task<CurvePointDTO?> CreateAsync(CurvePointModel model);

        /// <summary>
        /// Asynchronously retrieves a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously updates a specific CurvePoint entity.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="model">The CurvePointModel with updated values</param>
        /// <returns>The updated CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> UpdateByIdAsync(int id, CurvePointModel model);

        /// <summary>
        /// Asynchronously deletes a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>The deleted CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> DeleteByIdAsync(int id);
    }
}
