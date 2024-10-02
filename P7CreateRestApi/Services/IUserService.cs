using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    public interface IUserService
    {
        Task<UserDTO?> CreateAsync(UserModel model);
        Task<UserDTO?> DeleteByIdAsync(int id);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<List<UserDTO>> ListAsync();
        Task<UserDTO?> UpdateByIdAsync(int id, UserModel model);
    }
}
