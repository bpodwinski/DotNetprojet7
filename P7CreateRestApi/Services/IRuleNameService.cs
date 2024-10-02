using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for managing operations on RuleName entities.
    /// </summary>
    public interface IRuleNameService
    {
        Task<RuleNameDTO?> CreateAsync(RuleNameModel inputModel);
        Task<RuleNameDTO?> DeleteByIdAsync(int id);
        Task<RuleNameDTO?> GetByIdAsync(int id);
        Task<List<RuleNameDTO>> ListAsync();
        Task<RuleNameDTO?> UpdateByIdAsync(int id, RuleNameModel inputModel);
    }
}
