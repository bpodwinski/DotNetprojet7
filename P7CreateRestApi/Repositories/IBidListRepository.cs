using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing BidList entities.
    /// Defines the methods for CRUD operations on BidList entities.
    /// </summary>
    public interface IBidListRepository
    {
        /// <summary>
        /// Asynchronously creates a new BidList entity and saves it to the database.
        /// </summary>
        /// <param name="bidList">The BidList entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateAsync(BidList bidList);

        /// <summary>
        /// Asynchronously deletes a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList entity to delete.</param>
        /// <returns>The deleted BidList entity, or null if not found.</returns>
        Task<BidList?> DeleteAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList entity to retrieve.</param>
        /// <returns>The BidList entity, or null if not found.</returns>
        Task<BidList?> GetAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all BidList entities from the database.
        /// </summary>
        /// <returns>A list of BidList entities.</returns>
        Task<List<BidList>> ListAsync();

        /// <summary>
        /// Asynchronously updates an existing BidList entity and saves changes to the database.
        /// </summary>
        /// <param name="bidList">The BidList entity with updated values.</param>
        /// <returns>The updated BidList entity, or null if not found.</returns>
        Task<BidList?> UpdateAsync(BidList bidList);
    }
}
