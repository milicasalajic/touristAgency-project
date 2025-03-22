using System.ComponentModel.DataAnnotations;

namespace TouristAgency.RequestModel
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
