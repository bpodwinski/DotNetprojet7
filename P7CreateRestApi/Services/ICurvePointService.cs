using P7CreateRestApi.DTOs;

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
        Task<List<CurvePointDTO>> GetAll();

        /// <summary>
        /// Asynchronously creates a new CurvePoint entity based on the provided model.
        /// </summary>
        /// <param name="dto">The CurvePointModel object containing the data for the new entity</param>
        /// <returns>The created CurvePointDTO</returns>
        Task<CurvePointDTO?> Create(CurvePointDTO dto);

        /// <summary>
        /// Asynchronously retrieves a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> GetById(int id);

        /// <summary>
        /// Asynchronously updates a specific CurvePoint entity.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="dto">The CurvePointModel with updated values</param>
        /// <returns>The updated CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> Update(int id, CurvePointDTO dto);

        /// <summary>
        /// Asynchronously deletes a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>The deleted CurvePointDTO or null if not found</returns>
        Task<CurvePointDTO?> DeleteById(int id);
    }
}
