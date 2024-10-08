using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing RuleName entities in the database.
    /// </summary>
    public interface IRuleNameRepository
    {
        /// <summary>
        /// Asynchronously creates a new RuleName entity and saves it to the database.
        /// </summary>
        /// <param name="ruleName">The RuleName entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Create(RuleName ruleName);

        /// <summary>
        /// Asynchronously deletes a RuleName entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName entity to delete.</param>
        /// <returns>The deleted RuleName entity, or null if not found.</returns>
        Task<RuleName?> DeleteById(int id);

        /// <summary>
        /// Asynchronously retrieves a RuleName entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName entity to retrieve.</param>
        /// <returns>The RuleName entity, or null if not found.</returns>
        Task<RuleName?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves all RuleName entities from the database.
        /// </summary>
        /// <returns>A list of RuleName entities.</returns>
        Task<List<RuleName>> GetAll();

        /// <summary>
        /// Asynchronously updates an existing RuleName entity and saves changes to the database.
        /// </summary>
        /// <param name="ruleName">The RuleName entity with updated values.</param>
        /// <returns>The updated RuleName entity, or null if not found.</returns>
        Task<RuleName?> UpdateAsync(RuleName ruleName);
    }
}
