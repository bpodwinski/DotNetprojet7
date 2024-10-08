using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing Trade entities in the database.
    /// </summary>
    public interface ITradeRepository
    {
        /// <summary>
        /// Asynchronously creates a new Trade entity and saves it to the database.
        /// </summary>
        /// <param name="trade">The Trade entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Create(Trade trade);

        /// <summary>
        /// Asynchronously deletes a Trade entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade entity to delete.</param>
        /// <returns>The deleted Trade entity, or null if not found.</returns>
        Task<Trade?> DeleteById(int id);

        /// <summary>
        /// Asynchronously retrieves a Trade entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade entity to retrieve.</param>
        /// <returns>The Trade entity, or null if not found.</returns>
        Task<Trade?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves all Trade entities from the database.
        /// </summary>
        /// <returns>A list of Trade entities.</returns>
        Task<List<Trade>> GetAll();

        /// <summary>
        /// Asynchronously updates an existing Trade entity and saves changes to the database.
        /// </summary>
        /// <param name="trade">The Trade entity with updated values.</param>
        /// <returns>The updated Trade entity, or null if not found.</returns>
        Task<Trade?> UpdateAsync(Trade trade);
    }
}
