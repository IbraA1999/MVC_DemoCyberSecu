using System.ComponentModel.DataAnnotations;

namespace PresentationSiteWeb.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [MaxLength(100)]
        public required string Nom { get; set; }

        [Required(ErrorMessage = "Le prenom est requis")]
        [MaxLength(100)]
        public required string Prenom { get; set; }


        [Required(ErrorMessage = "L'email est requis")]
        [MaxLength(356)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
