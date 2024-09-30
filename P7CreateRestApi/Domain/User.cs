using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        [Required(ErrorMessage = "Le champs Username est requis")]
        [MinLength(3, ErrorMessage = "Le champs Username doit avoir au moins 3 caractères")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le champs Password est requis")]
        [MinLength(8, ErrorMessage = "Le champs Password doit avoir au moins 8 caractères")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).*$", ErrorMessage = "Le champs Password doit contenir au moins une lettre majuscule, un chiffre et un symbole")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champs Fullname est requis")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Le champs Role est requis")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "Le rôle doit être 'Admin' ou 'User'")]
        public string Role { get; set; }
    }
}
