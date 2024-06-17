using System.ComponentModel.DataAnnotations;

namespace PresentationSiteWeb.Models
{
    public class CreateContactForm
    {
        [Required(ErrorMessage = "Le nom est requis")]
        [MaxLength(100)]
        [MinLength(1)]
        public required string Nom { get; set; }

        [Required(ErrorMessage = "Le prenom est requis")]
        [MaxLength(100)]
        [MinLength(1)]
        public required string Prenom { get; set; }


        [Required(ErrorMessage = "L'email est requis")]
        [MaxLength(356)]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
