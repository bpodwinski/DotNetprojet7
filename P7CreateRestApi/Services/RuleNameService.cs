using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private readonly IRuleNameRepository _ruleNameRepository;
        private readonly LocalDbContext _dbContext;

        public RuleNameService(
            IRuleNameRepository ruleNameRepository,
            LocalDbContext dbContext
        )
        {
            _ruleNameRepository = ruleNameRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new RuleName based on the provided model.
        /// </summary>
        /// <param name="dto">The RuleNameDTO containing the data to create the RuleName</param>
        /// <returns>The created RuleNameDTO</returns>
        public async Task<RuleNameDTO?> Create(RuleNameDTO dto)
        {
            try
            {
                var ruleName = new RuleName
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Json = dto.Json,
                    Template = dto.Template,
                    SqlStr = dto.SqlStr,
                    SqlPart = dto.SqlPart
                };

                await _ruleNameRepository.Create(ruleName);
                return ToDTO(ruleName);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a new RuleName.", ex);
            }
        }

        /// <summary>
        /// Deletes a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to delete</param>
        /// <returns>The deleted RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> DeleteById(int id)
        {
            try
            {
                var existingRuleName = await _ruleNameRepository.GetById(id);
                if (existingRuleName == null)
                {
                    return null;
                }

                _dbContext.Entry(existingRuleName).State = EntityState.Detached;

                var deletedRuleName = await _ruleNameRepository.DeleteById(id);
                return deletedRuleName != null ? ToDTO(deletedRuleName) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the RuleName with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific RuleName by ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to retrieve</param>
        /// <returns>The RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> GetById(int id)
        {
            try
            {
                var ruleName = await _ruleNameRepository.GetById(id);
                return ruleName != null ? ToDTO(ruleName) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the RuleName with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all RuleName entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of RuleNameDTOs</returns>
        public async Task<List<RuleNameDTO>> GetAll()
        {
            try
            {
                var ruleNames = await _ruleNameRepository.GetAll();
                return ruleNames.Select(ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all RuleNames.", ex);
            }
        }

        /// <summary>
        /// Updates a specific RuleName entity.
        /// </summary>
        /// <param name="id">The ID of the RuleName to update</param>
        /// <param name="dto">The RuleNameDTO containing the updated values</param>
        /// <returns>The updated RuleNameDTO, or null if not found</returns>
        public async Task<RuleNameDTO?> Update(int id, RuleNameDTO dto)
        {
            try
            {
                var existingRuleName = await _ruleNameRepository.GetById(id);
                if (existingRuleName == null)
                {
                    return null;
                }

                existingRuleName.Name = dto.Name;
                existingRuleName.Description = dto.Description;
                existingRuleName.Json = dto.Json;
                existingRuleName.Template = dto.Template;
                existingRuleName.SqlStr = dto.SqlStr;
                existingRuleName.SqlPart = dto.SqlPart;

                var updatedRuleName = await _ruleNameRepository.UpdateAsync(existingRuleName);
                return ToDTO(updatedRuleName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the RuleName with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Converts a RuleName entity to a RuleNameDTO.
        /// </summary>
        /// <param name="ruleName">The RuleName entity to convert</param>
        /// <returns>The corresponding RuleNameDTO</returns>
        private RuleNameDTO ToDTO(RuleName ruleName) =>
            new()
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
