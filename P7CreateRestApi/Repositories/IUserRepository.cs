using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for the User repository.
    /// </summary>
    public interface IUserRepository
    {
        Task<List<User>> FindAllAsync();
        Task AddAsync(User user);
        Task<User?> FindByIdAsync(int id);
        Task<User?> FindByUserNameAsync(string username);
    }
}
