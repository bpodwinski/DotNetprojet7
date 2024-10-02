using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.Extensions.Logging;

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
        public async Task CreateAsync(RuleName ruleName)
        {
            try
            {
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
        /// <returns>The deleted RuleName entity.</returns>
        public async Task<RuleName?> DeleteByIdAsync(int id)
        {
            try
            {
                var ruleName = new RuleName { Id = id };

                _dbContext.RuleNames.Remove(ruleName);
                await _dbContext.SaveChangesAsync();

                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting RuleName with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a RuleName entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName entity to retrieve.</param>
        /// <returns>The RuleName entity, or null if not found.</returns>
        public async Task<RuleName?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.RuleNames.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the RuleName with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all RuleName entities from the database.
        /// </summary>
        /// <returns>A list of RuleName entities.</returns>
        public async Task<List<RuleName>> ListAsync()
        {
            try
            {
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
        /// <returns>The updated RuleName entity.</returns>
        public async Task<RuleName?> UpdateAsync(RuleName ruleName)
        {
            try
            {
                _dbContext.RuleNames.Update(ruleName);
                await _dbContext.SaveChangesAsync();

                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the RuleName with ID {ruleName.Id}.");
                throw;
            }
        }
    }
}
