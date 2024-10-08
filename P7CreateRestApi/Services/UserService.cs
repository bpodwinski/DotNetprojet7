using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
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
        /// Creates a new user based on the provided dto.
        /// </summary>
        public async Task<UserDTO?> Create(UserDTO dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                FullName = dto.FullName,
                Role = dto.Role,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
                return ToOutputdto(user);
            }
            return null;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        public async Task<UserDTO?> DeleteById(int id)
        {
            var user = await _userRepository.FindById(id);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return ToOutputdto(user);
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        public async Task<UserDTO?> GetById(int id)
        {
            var user = await _userRepository.FindById(id);
            return user is not null ? ToOutputdto(user) : null;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _userRepository.FindAll();
            return users.Select(ToOutputdto).ToList();
        }

        /// <summary>
        /// Updates a user's information and resets password if necessary.
        /// </summary>
        public async Task<UserDTO?> Update(int id, UserDTO dto)
        {
            var user = await _userRepository.FindById(id);
            if (user is null) return null;

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, dto.Password);
            }

            user.UserName = dto.Username;
            user.FullName = dto.FullName;
            user.Role = dto.Role;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? ToOutputdto(user) : null;
        }

        /// <summary>
        /// Converts a User entity to a UserDTO.
        /// </summary>
        private static UserDTO ToOutputdto(User user) => new()
        {
            Id = user.Id,
            Username = user.UserName,
            FullName = user.FullName,
            Role = user.Role
        };
    }
}
