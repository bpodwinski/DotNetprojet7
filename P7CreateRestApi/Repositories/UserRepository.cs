using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Repository class for managing User entities in the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(LocalDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously retrieves all User entities from the database.
        /// </summary>
        public async Task<List<User>> FindAllAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously adds a new User entity to the database.
        /// </summary>
        public async Task AddAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new user.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously finds a User entity by its ID.
        /// </summary>
        public async Task<User?> FindByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the user with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously finds a User entity by its username.
        /// </summary>
        public async Task<User?> FindByUserNameAsync(string username)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the user with username {username}.");
                throw;
            }
        }
    }
}
