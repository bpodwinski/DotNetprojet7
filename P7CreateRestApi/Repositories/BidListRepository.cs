using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing BidList entities in the database.
    /// </summary>
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<BidListRepository> _logger;

        public BidListRepository(LocalDbContext dbContext, ILogger<BidListRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously creates a new BidList entity and saves it to the database.
        /// </summary>
        /// <param name="bidList">The BidList entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Create(BidList bidList)
        {
            try
            {
                await _dbContext.Bids.AddAsync(bidList);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Successfully created BidList with ID {BidListId}.", bidList.BidListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new BidList.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList entity to delete.</param>
        /// <returns>The deleted BidList entity, or null if not found.</returns>
        public async Task<BidList?> DeleteById(int id)
        {
            try
            {
                var bidList = await _dbContext.Bids.FindAsync(id);

                if (bidList == null)
                {
                    _logger.LogWarning("Attempted to delete BidList with ID {Id}, but it was not found.", id);
                    return null;
                }

                _dbContext.Bids.Remove(bidList);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Successfully deleted BidList with ID {Id}.", id);

                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting BidList with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList entity to retrieve.</param>
        /// <returns>The BidList entity, or null if not found.</returns>
        public async Task<BidList?> GetById(int id)
        {
            try
            {
                var bidList = await _dbContext.Bids.FirstOrDefaultAsync(b => b.BidListId == id);
                if (bidList == null)
                {
                    _logger.LogWarning("BidList with ID {Id} was not found.", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved BidList with ID {Id}.", id);
                }

                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving BidList with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all BidList entities from the database.
        /// </summary>
        /// <returns>A list of BidList entities.</returns>
        public IQueryable<BidList> GetAll()
        {
            try
            {
                _logger.LogInformation("Retrieving all BidList entities.");
                return _dbContext.Bids.AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all BidList entities.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing BidList entity and saves changes to the database.
        /// </summary>
        /// <param name="bidList">The BidList entity with updated values.</param>
        /// <returns>The updated BidList entity.</returns>
        public async Task<BidList?> Update(BidList bidList)
        {
            try
            {
                _logger.LogInformation("Attempting to update BidList with ID {BidListId}.", bidList.BidListId);

                var existingBidList = await _dbContext.Bids.FindAsync(bidList.BidListId);

                if (existingBidList == null)
                {
                    _logger.LogWarning("BidList with ID {BidListId} not found for update.", bidList.BidListId);
                    return null;
                }

                _dbContext.Entry(existingBidList).CurrentValues.SetValues(bidList);

                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Successfully updated BidList with ID {BidListId}.", bidList.BidListId);

                return existingBidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the BidList with ID {BidListId}.", bidList.BidListId);
                throw;
            }
        }

    }
}
