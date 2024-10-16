using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a User entity.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the User.
        /// </summary>
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the User.
        /// </summary>
        [Required(ErrorMessage = "The Username field is required.")]
        [MinLength(3, ErrorMessage = "The Username must be at least 3 characters long.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the User.
        /// </summary>
        [Required(ErrorMessage = "The Password field is required.")]
        [MinLength(8, ErrorMessage = "The Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).*$", ErrorMessage = "The Password must contain at least one uppercase letter, one number, and one special character.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full name of the User.
        /// </summary>
        [Required(ErrorMessage = "The FullName field is required.")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role assigned to the User.
        /// </summary>
        [Required(ErrorMessage = "The Role field is required.")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "The Role must be 'Admin' or 'User'.")]
        public string Role { get; set; } = string.Empty;
    }
}
