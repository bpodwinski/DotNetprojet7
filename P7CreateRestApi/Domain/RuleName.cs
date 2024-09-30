using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Controllers
{
    public class RuleName
    {
        [Required(ErrorMessage = "Le champs Name est requis")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le champs Description est requis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le champs Json est requis")]
        public string Json { get; set; }

        [Required(ErrorMessage = "Le champs Template est requis")]
        public string Template { get; set; }

        [Required(ErrorMessage = "Le champs SqlStr est requis")]
        public string SqlStr { get; set; }

        [Required(ErrorMessage = "Le champs SqlPart est requis")]
        public string SqlPart { get; set; }
    }
}
