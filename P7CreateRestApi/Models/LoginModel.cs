using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Le champs Username est requis")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le champs Password est requis")]
        public string Password { get; set; }
    }
}
