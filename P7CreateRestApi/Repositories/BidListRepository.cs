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
                var bidList = new BidList { BidListId = id };

                _dbContext.Bids.Remove(bidList);
                await _dbContext.SaveChangesAsync();

                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting BidList with ID {id}");
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
            return await _dbContext.Bids.FirstOrDefaultAsync(b => b.BidListId == id);
        }

        /// <summary>
        /// Asynchronously retrieves all BidList entities from the database.
        /// </summary>
        /// <returns>A list of BidList entities.</returns>
        public IQueryable<BidList> GetAll()
        {
            return _dbContext.Bids.AsQueryable();
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
                _dbContext.Bids.Update(bidList);

                await _dbContext.SaveChangesAsync();

                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating BidList with ID {bidList.BidListId}.");
                throw;
            }
        }

    }
}
