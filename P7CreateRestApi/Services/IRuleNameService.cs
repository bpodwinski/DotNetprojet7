using P7CreateRestApi.DTOs;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for managing operations on RuleName entities.
    /// </summary>
    public interface IRuleNameService
    {
        /// <summary>
        /// Asynchronously creates a new RuleName.
        /// </summary>
        /// <param name="dto">The RuleNameDTO containing the details of the RuleName to create.</param>
        /// <returns>The created RuleNameDTO, or null if the creation failed.</returns>
        Task<RuleNameDTO?> Create(RuleNameDTO dto);

        /// <summary>
        /// Asynchronously deletes a RuleName by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to delete.</param>
        /// <returns>The deleted RuleNameDTO, or null if the RuleName was not found.</returns>
        Task<RuleNameDTO?> DeleteById(int id);

        /// <summary>
        /// Asynchronously retrieves a RuleName by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to retrieve.</param>
        /// <returns>The RuleNameDTO, or null if the RuleName was not found.</returns>
        Task<RuleNameDTO?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves all RuleName entities.
        /// </summary>
        /// <returns>A list of RuleNameDTOs.</returns>
        Task<List<RuleNameDTO>> GetAll();

        /// <summary>
        /// Asynchronously updates an existing RuleName by its ID.
        /// </summary>
        /// <param name="id">The ID of the RuleName to update.</param>
        /// <param name="dto">The RuleNameDTO containing the updated details of the RuleName.</param>
        /// <returns>The updated RuleNameDTO, or null if the RuleName was not found.</returns>
        Task<RuleNameDTO?> Update(int id, RuleNameDTO dto);
    }
}
