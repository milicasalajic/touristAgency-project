using System.ComponentModel.DataAnnotations;

namespace TouristAgency.DTO.Requests
{
    public class UserUpdateRequestDTO
    {
        [StringLength(50, ErrorMessage = "Ime ne može biti duže od 50 karaktera.")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "Prezime ne može biti duže od 50 karaktera.")]
        public string? LastName { get; set; }

        [StringLength(50, ErrorMessage = "Korisničko ime ne može biti duže od 50 karaktera.")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "Nevažeća email adresa.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\+?[0-9]{6,15}$", ErrorMessage = "Nevažeći broj telefona.")]
        public string? PhoneNumber { get; set; }

        public string? UserPhoto { get; set; }

        // Polja za promenu lozinke
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Lozinka mora imati najmanje 6 karaktera.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Nova lozinka i ponovljena lozinka se ne podudaraju.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
