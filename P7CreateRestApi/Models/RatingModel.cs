using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RatingModel
    {
        [Required(ErrorMessage = "Le champs MoodysRating est requis")]
        public string MoodysRating { get; set; }

        [Required(ErrorMessage = "Le champs SandPRating est requis")]
        public string SandPRating { get; set; }

        [Required(ErrorMessage = "Le champs FitchRating est requis")]
        public string FitchRating { get; set; }

        [Required(ErrorMessage = "Le champs OrderNumber est requis")]
        public byte? OrderNumber { get; set; }
    }
}
