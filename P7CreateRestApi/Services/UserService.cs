using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates a new user based on the provided model.
        /// </summary>
        public async Task<UserDTO?> CreateAsync(UserModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Role = model.Role,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
                return ToOutputModel(user);
            }
            return null;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        public async Task<UserDTO?> DeleteByIdAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return ToOutputModel(user);
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            return user is not null ? ToOutputModel(user) : null;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        public async Task<List<UserDTO>> ListAsync()
        {
            var users = await _userRepository.FindAllAsync();
            return users.Select(ToOutputModel).ToList();
        }

        /// <summary>
        /// Updates a user's information and resets password if necessary.
        /// </summary>
        public async Task<UserDTO?> UpdateByIdAsync(int id, UserModel model)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user is null) return null;

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, model.Password);
            }

            user.UserName = model.UserName;
            user.FullName = model.FullName;
            user.Role = model.Role;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? ToOutputModel(user) : null;
        }

        /// <summary>
        /// Converts a User entity to a UserDTO.
        /// </summary>
        private static UserDTO ToOutputModel(User user) => new UserDTO
        {
            Id = user.Id,
            Username = user.UserName,
            FullName = user.FullName,
            Role = user.Role
        };
    }
}
