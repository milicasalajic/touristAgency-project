using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserByIdAsync(Guid id);
        Task<string> Login(string email, string password);
        Task<bool> RegisterUserAsync(RegistrationRequestDTO requestDTO);
        Task<bool> UpdateUserAsync(Guid userId, UserUpdateRequestDTO userUpdateDTO);
    }
}
