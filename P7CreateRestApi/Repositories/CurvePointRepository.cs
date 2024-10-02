using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing CurvePoint entities in the database.
    /// </summary>
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<CurvePointRepository> _logger;

        public CurvePointRepository(LocalDbContext dbContext, ILogger<CurvePointRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously retrieves all CurvePoint entities from the database.
        /// </summary>
        /// <returns>A list of CurvePoint entities.</returns>
        public async Task<List<CurvePoint>> List()
        {
            try
            {
                return await _dbContext.CurvePoints.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving CurvePoints.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously creates a new CurvePoint entity and saves it to the database.
        /// </summary>
        /// <param name="curvePoint">The CurvePoint entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Create(CurvePoint curvePoint)
        {
            try
            {
                await _dbContext.CurvePoints.AddAsync(curvePoint);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new CurvePoint.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a CurvePoint entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint entity to retrieve.</param>
        /// <returns>The CurvePoint entity, or null if not found.</returns>
        public async Task<CurvePoint?> GetById(int id)
        {
            try
            {
                return await _dbContext.CurvePoints.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the CurvePoint with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing CurvePoint entity and saves changes to the database.
        /// </summary>
        /// <param name="curvePoint">The CurvePoint entity with updated values.</param>
        /// <returns>The updated CurvePoint entity.</returns>
        public async Task<CurvePoint> Update(CurvePoint curvePoint)
        {
            try
            {
                _dbContext.CurvePoints.Update(curvePoint);
                await _dbContext.SaveChangesAsync();
                return curvePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the CurvePoint with ID {curvePoint.Id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a CurvePoint entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint entity to delete.</param>
        /// <returns>The deleted CurvePoint entity.</returns>
        public async Task<CurvePoint> DeleteById(int id)
        {
            try
            {
                var curvePoint = new CurvePoint { Id = id };
                _dbContext.CurvePoints.Remove(curvePoint);
                await _dbContext.SaveChangesAsync();
                return curvePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the CurvePoint with ID {id}.");
                throw;
            }
        }
    }
}
