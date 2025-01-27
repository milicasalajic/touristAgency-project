using Microsoft.Extensions.Configuration.UserSecrets;
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

    }

}
