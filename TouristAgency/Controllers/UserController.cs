using Microsoft.AspNetCore.Mvc;
using TouristAgency.RequestModel;
using TouristAgency.ServiceInterfaces;

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
    }
}
