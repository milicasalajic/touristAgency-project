using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtService;
        public UserService(IUserRepository userRepository, JwtTokenService jwtService)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }
        public async Task<string> Login(string username, string password)
        {
            //Da bi uzela sve promenljive istog imena pritisnes na ime promenljive i onda ctrl + r + r
            User userByUsername = await _userRepository.FindByUsernameAsync(username);
            if (userByUsername == null)
            {
                throw new DataRetrievalException<User>();
            }
            if (password != userByUsername.Password)
            {
                throw new DataRetrievalException<User>();
            }
            var token = _jwtService.GenerateJWTToken(userByUsername);
            return token;
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException<User>();
            }
            var userDTO = new UserResponseDTO()
            {
                Name = user.Name,
                UserName = user.UserName,
                Role = user.Role,
            };
            return userDTO;
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserUpdateRequestDTO userUpdateDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new EntityNotFoundException<User>();
            }

            user.Name = userUpdateDTO.Name ?? user.Name;
            user.LastName = userUpdateDTO.LastName ?? user.LastName;
            user.UserName = userUpdateDTO.UserName ?? user.UserName;
            user.Email = userUpdateDTO.Email ?? user.Email;
            user.PhoneNumber = userUpdateDTO.PhoneNumber ?? user.PhoneNumber;
            user.UserPhoto = userUpdateDTO.UserPhoto ?? user.UserPhoto;

            if (!string.IsNullOrEmpty(userUpdateDTO.OldPassword) && !string.IsNullOrEmpty(userUpdateDTO.NewPassword))
            {
                if (userUpdateDTO.OldPassword != user.Password)
                {
                    throw new IncorrectPasswordException();
                }

                user.Password = userUpdateDTO.NewPassword;
            }

            return await _userRepository.UpdateUserAsync(user);
        }
        public async Task<bool> RegisterUserAsync(RegistrationRequestDTO request)
        {
            var userByEmail = await _userRepository.FindByEmailAsync(request.Email);
            if (userByEmail != null)
            {
                throw new EntityAlreadyExistsException<User>("email");
            }
            var userByUsername = await _userRepository.FindByUserNameAsync(request.UserName);
            if (userByUsername != null)
            {
                throw new EntityAlreadyExistsException<User>("username");
            }
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                Role = request.Role,
                Password = request.Password,
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserPhoto = string.Empty,
            };

            var result = await _userRepository.InsertAsync(user);
            if (!result)
                return false;

            return true;
        }
    }
}
