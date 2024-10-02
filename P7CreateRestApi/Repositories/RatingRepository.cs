using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing Rating entities in the database.
    /// </summary>
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(LocalDbContext dbContext, ILogger<RatingRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously retrieves all Rating entities from the database.
        /// </summary>
        /// <returns>A list of Rating entities.</returns>
        public async Task<List<Rating>> ListAsync()
        {
            try
            {
                return await _dbContext.Ratings.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Ratings.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously creates a new Rating entity and saves it to the database.
        /// </summary>
        /// <param name="rating">The Rating entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Rating rating)
        {
            try
            {
                await _dbContext.Ratings.AddAsync(rating);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new Rating.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Rating entity to retrieve.</param>
        /// <returns>The Rating entity, or null if not found.</returns>
        public async Task<Rating?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Ratings.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the Rating with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing Rating entity and saves changes to the database.
        /// </summary>
        /// <param name="rating">The Rating entity with updated values.</param>
        /// <returns>The updated Rating entity.</returns>
        public async Task<Rating?> UpdateAsync(Rating rating)
        {
            try
            {
                _dbContext.Ratings.Update(rating);
                await _dbContext.SaveChangesAsync();
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Rating with ID {rating.Id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Rating entity to delete.</param>
        /// <returns>The deleted Rating entity.</returns>
        public async Task<Rating?> DeleteByIdAsync(int id)
        {
            try
            {
                var rating = new Rating { Id = id };
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the Rating with ID {id}.");
                throw;
            }
        }
    }
}
