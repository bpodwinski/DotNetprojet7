using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing RuleName entities in the database.
    /// </summary>
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RuleNameRepository> _logger;

        public RuleNameRepository(LocalDbContext dbContext, ILogger<RuleNameRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously creates a new RuleName entity and saves it to the database.
        /// </summary>
        /// <param name="ruleName">The RuleName entity to create.</param>
        public async Task Create(RuleName ruleName)
        {
            try
            {
                _logger.LogInformation("Creating a new RuleName.");
                await _dbContext.RuleNames.AddAsync(ruleName);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new RuleName.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a RuleName entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName entity to delete.</param>
        /// <returns>The deleted RuleName entity, or null if not found.</returns>
        public async Task<RuleName?> DeleteById(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete RuleName with ID {Id}.", id);

                // Fetch the RuleName first to ensure it's tracked properly
                var ruleName = await _dbContext.RuleNames.FindAsync(id);

                if (ruleName == null)
                {
                    _logger.LogWarning("RuleName with ID {Id} not found.", id);
                    return null;
                }

                _dbContext.RuleNames.Remove(ruleName);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted RuleName with ID {Id}.", id);
                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting RuleName with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a RuleName entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName entity to retrieve.</param>
        /// <returns>The RuleName entity, or null if not found.</returns>
        public async Task<RuleName?> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching RuleName with ID {Id}.", id);
                return await _dbContext.RuleNames.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the RuleName with ID {Id}.", id);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all RuleName entities from the database.
        /// </summary>
        /// <returns>A list of RuleName entities.</returns>
        public async Task<List<RuleName>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all RuleNames.");
                return await _dbContext.RuleNames.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving RuleNames.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing RuleName entity and saves changes to the database.
        /// </summary>
        /// <param name="ruleName">The RuleName entity with updated values.</param>
        /// <returns>The updated RuleName entity, or null if not found.</returns>
        public async Task<RuleName?> UpdateAsync(RuleName ruleName)
        {
            try
            {
                _logger.LogInformation("Updating RuleName with ID {Id}.", ruleName.Id);

                var existingRuleName = await _dbContext.RuleNames.FindAsync(ruleName.Id);

                if (existingRuleName == null)
                {
                    _logger.LogWarning("RuleName with ID {Id} not found for update.", ruleName.Id);
                    return null;
                }

                _dbContext.Entry(existingRuleName).CurrentValues.SetValues(ruleName);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Successfully updated RuleName with ID {Id}.", ruleName.Id);
                return existingRuleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the RuleName with ID {Id}.", ruleName.Id);
                throw;
            }
        }

    }
}
