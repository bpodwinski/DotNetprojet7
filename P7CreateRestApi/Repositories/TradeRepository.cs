using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing Trade entities in the database.
    /// </summary>
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<TradeRepository> _logger;

        public TradeRepository(LocalDbContext dbContext, ILogger<TradeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously creates a new Trade entity and saves it to the database.
        /// </summary>
        /// <param name="trade">The Trade entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Trade trade)
        {
            try
            {
                await _dbContext.Trades.AddAsync(trade);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new Trade.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a Trade entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade entity to delete.</param>
        /// <returns>The deleted Trade entity, or null if not found.</returns>
        public async Task<Trade?> DeleteByIdAsync(int id)
        {
            try
            {
                var trade = new Trade { TradeId = id };

                _dbContext.Trades.Remove(trade);
                await _dbContext.SaveChangesAsync();

                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Trade with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a Trade entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade entity to retrieve.</param>
        /// <returns>The Trade entity, or null if not found.</returns>
        public async Task<Trade?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Trades.FirstOrDefaultAsync(t => t.TradeId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the Trade with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all Trade entities from the database.
        /// </summary>
        /// <returns>A list of Trade entities.</returns>
        public async Task<List<Trade>> ListAsync()
        {
            try
            {
                return await _dbContext.Trades.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Trades.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing Trade entity and saves changes to the database.
        /// </summary>
        /// <param name="trade">The Trade entity with updated values.</param>
        /// <returns>The updated Trade entity, or null if not found.</returns>
        public async Task<Trade?> UpdateAsync(Trade trade)
        {
            try
            {
                _dbContext.Trades.Update(trade);
                await _dbContext.SaveChangesAsync();

                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Trade with ID {trade.TradeId}.");
                throw;
            }
        }
    }
}
