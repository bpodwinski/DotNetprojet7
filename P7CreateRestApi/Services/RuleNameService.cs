using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNameService(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        /// <summary>
        /// Creates a new RuleName based on the provided model.
        /// </summary>
        /// <param name="inputModel">The RuleNameModel containing the data to create the RuleName</param>
        /// <returns>The created RuleNameDTO</returns>
        public async Task<RuleNameDTO?> CreateAsync(RuleNameModel inputModel)
        {
            var ruleName = new RuleName
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Json = inputModel.Json,
                Template = inputModel.Template,
                SqlStr = inputModel.SqlStr,
                SqlPart = inputModel.SqlPart
            };

            await _ruleNameRepository.CreateAsync(ruleName);
            return ToDTO(ruleName);
        }

        /// <summary>
        /// Deletes a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to delete</param>
        /// <returns>The deleted RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> DeleteByIdAsync(int id)
        {
            var existingRuleName = await _ruleNameRepository.GetByIdAsync(id);
            if (existingRuleName == null)
            {
                return null;
            }

            var deletedRuleName = await _ruleNameRepository.DeleteByIdAsync(id);
            return deletedRuleName != null ? ToDTO(deletedRuleName) : null;
        }

        /// <summary>
        /// Retrieves a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to retrieve</param>
        /// <returns>The RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> GetByIdAsync(int id)
        {
            var ruleName = await _ruleNameRepository.GetByIdAsync(id);
            return ruleName != null ? ToDTO(ruleName) : null;
        }

        /// <summary>
        /// Retrieves all RuleName entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of RuleNameDTOs</returns>
        public async Task<List<RuleNameDTO>> ListAsync()
        {
            var ruleNames = await _ruleNameRepository.ListAsync();
            return ruleNames.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Updates a specific RuleName entity.
        /// </summary>
        /// <param name="id">The ID of the RuleName to update</param>
        /// <param name="inputModel">The RuleNameModel containing the updated values</param>
        /// <returns>The updated RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> UpdateByIdAsync(int id, RuleNameModel inputModel)
        {
            var existingRuleName = await _ruleNameRepository.GetByIdAsync(id);
            if (existingRuleName == null)
            {
                return null;
            }

            existingRuleName.Name = inputModel.Name;
            existingRuleName.Description = inputModel.Description;
            existingRuleName.Json = inputModel.Json;
            existingRuleName.Template = inputModel.Template;
            existingRuleName.SqlStr = inputModel.SqlStr;
            existingRuleName.SqlPart = inputModel.SqlPart;

            var updatedRuleName = await _ruleNameRepository.UpdateAsync(existingRuleName);
            return ToDTO(updatedRuleName);
        }

        /// <summary>
        /// Converts a RuleName entity to a RuleNameDTO.
        /// </summary>
        /// <param name="ruleName">The RuleName entity to convert</param>
        /// <returns>The corresponding RuleNameDTO</returns>
        private RuleNameDTO ToDTO(RuleName ruleName) =>
            new RuleNameDTO
            {
                Id = ruleName.Id,
                Name = ruleName.Name,
                Description = ruleName.Description,
                Json = ruleName.Json,
                Template = ruleName.Template,
                SqlStr = ruleName.SqlStr,
                SqlPart = ruleName.SqlPart
            };
    }
}
