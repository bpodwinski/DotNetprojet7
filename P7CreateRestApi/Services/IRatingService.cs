using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for managing operations on Rating entities.
    /// </summary>
    public interface IRatingService
    {
        Task<RatingDTO?> CreateAsync(RatingModel model);
        Task<RatingDTO?> DeleteByIdAsync(int id);
        Task<RatingDTO?> GetByIdAsync(int id);
        Task<List<RatingDTO>> ListAsync();
        Task<RatingDTO?> UpdateByIdAsync(int id, RatingModel model);
    }
}
