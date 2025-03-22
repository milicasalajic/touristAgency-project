using TouristAgency.Model;

namespace TouristAgency.DTO.Responses
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
       public Role Role { get; set; }
    }
}
