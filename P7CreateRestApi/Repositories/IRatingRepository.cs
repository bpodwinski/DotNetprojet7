using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing Rating entities in the database.
    /// </summary>
    public interface IRatingRepository
    {
        /// <summary>
        /// Asynchronously retrieves all Rating entities from the database.
        /// </summary>
        /// <returns>A list of Rating entities.</returns>
        Task<List<Rating>> GetAll();

        /// <summary>
        /// Asynchronously creates a new Rating entity and saves it to the database.
        /// </summary>
        /// <param name="rating">The Rating entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Create(Rating rating);

        /// <summary>
        /// Asynchronously retrieves a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Rating entity to retrieve.</param>
        /// <returns>The Rating entity, or null if not found.</returns>
        Task<Rating?> GetById(int id);

        /// <summary>
        /// Asynchronously updates an existing Rating entity and saves changes to the database.
        /// </summary>
        /// <param name="rating">The Rating entity with updated values.</param>
        /// <returns>The updated Rating entity.</returns>
        Task<Rating?> Update(Rating rating);

        /// <summary>
        /// Asynchronously deletes a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Rating entity to delete.</param>
        /// <returns>The deleted Rating entity, or null if not found.</returns>
        Task<Rating?> DeleteById(int id);
    }
}
