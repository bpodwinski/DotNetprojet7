using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing CurvePoint entities in the database.
    /// </summary>
    public interface ICurvePointRepository
    {
        /// <summary>
        /// Asynchronously retrieves all CurvePoint entities from the database.
        /// </summary>
        /// <returns>A list of CurvePoint entities.</returns>
        Task<List<CurvePoint>> List();

        /// <summary>
        /// Asynchronously creates a new CurvePoint entity and saves it to the database.
        /// </summary>
        /// <param name="curvePoint">The CurvePoint entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Create(CurvePoint curvePoint);

        /// <summary>
        /// Asynchronously retrieves a CurvePoint entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint entity to retrieve.</param>
        /// <returns>The CurvePoint entity, or null if not found.</returns>
        Task<CurvePoint?> GetById(int id);

        /// <summary>
        /// Asynchronously updates an existing CurvePoint entity and saves changes to the database.
        /// </summary>
        /// <param name="curvePoint">The CurvePoint entity with updated values.</param>
        /// <returns>The updated CurvePoint entity, or null if not found.</returns>
        Task<CurvePoint?> Update(CurvePoint curvePoint);

        /// <summary>
        /// Asynchronously deletes a CurvePoint entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint entity to delete.</param>
        /// <returns>The deleted CurvePoint entity, or null if not found.</returns>
        Task<CurvePoint?> DeleteById(int id);
    }
}
