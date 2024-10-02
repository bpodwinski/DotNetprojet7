using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    /// <summary>
    /// Interface for managing RuleName entities in the database.
    /// </summary>
    public interface IRuleNameRepository
    {
        Task CreateAsync(RuleName ruleName);
        Task<RuleName?> DeleteByIdAsync(int id);
        Task<RuleName?> GetByIdAsync(int id);
        Task<List<RuleName>> ListAsync();
        Task<RuleName?> UpdateAsync(RuleName ruleName);
    }
}
