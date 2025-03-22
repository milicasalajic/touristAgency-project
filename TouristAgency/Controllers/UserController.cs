using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Requests;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RequestModel;
using TouristAgency.ServiceInterfaces;
using LoginRequest = TouristAgency.RequestModel.LoginRequest;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginData)
        {
            var token = await _userService.Login(loginData.Username, loginData.Password);
            return Ok(token);
        }

        [HttpPut("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
        {
            // Dohvati korisnikov ID iz JWT tokena
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
            {
                throw new EntityNotFoundException<User>();
            }

            // Provera da li su unete nova lozinka i potvrda nove lozinke
            if (!string.IsNullOrWhiteSpace(userUpdateRequest.NewPassword) || !string.IsNullOrWhiteSpace(userUpdateRequest.ConfirmNewPassword))
            {
                if (userUpdateRequest.NewPassword != userUpdateRequest.ConfirmNewPassword)
                {
                    throw new PasswordsNotMatchingException();
                }
            }

            // Mapiraj UserUpdateRequest na UserUpdateRequestDTO
            var userUpdateDTO = new UserUpdateRequestDTO
            {
                Name = userUpdateRequest.Name,
                LastName = userUpdateRequest.LastName,
                UserName = userUpdateRequest.UserName,
                Email = userUpdateRequest.Email,
                PhoneNumber = userUpdateRequest.PhoneNumber,
                UserPhoto = userUpdateRequest.UserPhoto,
                OldPassword = userUpdateRequest.OldPassword,
                NewPassword = userUpdateRequest.NewPassword,
                ConfirmNewPassword = userUpdateRequest.ConfirmNewPassword
            };

            // Pozovi servis za ažuriranje korisnika
            var result = await _userService.UpdateUserAsync(userId, userUpdateDTO);

            if (!result)
            {
                throw new DataRetrievalException<User>();
            }

            return Ok(new { Message = "Uspešno ste izmenili podatke na Vašem profilu." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            RegistrationRequestDTO requestDTO = new RegistrationRequestDTO()
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                Role = request.Role,
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userService.RegisterUserAsync(requestDTO);
            if (!result)
            {
                return BadRequest("An error occurred during registration.");
            }

            return Ok("You have successfully registered.");
        }
    }
}
