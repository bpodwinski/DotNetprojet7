using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing Trade entities in the database.
    /// </summary>
    public interface ITradeRepository
    {
        Task CreateAsync(Trade trade);
        Task<Trade?> DeleteByIdAsync(int id);
        Task<Trade?> GetByIdAsync(int id);
        Task<List<Trade>> ListAsync();
        Task<Trade?> UpdateAsync(Trade trade);
    }
}
