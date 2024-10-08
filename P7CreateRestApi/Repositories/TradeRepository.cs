using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

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
        public async Task Create(Trade trade)
        {
            try
            {
                _logger.LogInformation("Creating a new Trade.");
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
        public async Task<Trade?> DeleteById(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete Trade with ID {Id}.", id);

                // Fetch the trade first to ensure it's tracked properly
                var trade = await _dbContext.Trades.FindAsync(id);

                if (trade == null)
                {
                    _logger.LogWarning("Trade with ID {Id} not found.", id);
                    return null;
                }

                _dbContext.Trades.Remove(trade);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Trade with ID {Id}.", id);
                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Trade with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a Trade entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade entity to retrieve.</param>
        /// <returns>The Trade entity, or null if not found.</returns>
        public async Task<Trade?> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching Trade with ID {Id}.", id);
                return await _dbContext.Trades.FirstOrDefaultAsync(t => t.TradeId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Trade with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all Trade entities from the database.
        /// </summary>
        /// <returns>A list of Trade entities.</returns>
        public async Task<List<Trade>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Trades.");
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
                _logger.LogInformation("Updating Trade with ID {Id}.", trade.TradeId);

                var existingTrade = await _dbContext.Trades.FindAsync(trade.TradeId);

                if (existingTrade == null)
                {
                    _logger.LogWarning("Trade with ID {Id} not found for update.", trade.TradeId);
                    return null;
                }

                _dbContext.Entry(existingTrade).CurrentValues.SetValues(trade);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Trade with ID {Id}.", trade.TradeId);
                return existingTrade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Trade with ID {Id}.", trade.TradeId);
                throw;
            }
        }

    }
}
