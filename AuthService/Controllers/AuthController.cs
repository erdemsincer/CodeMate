using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Email veya şifre hatalı.");

            return Ok(new { token });
        }
        [HttpGet("all-ids")]
        public async Task<IActionResult> GetAllUserIds()
        {
            var ids = await _authService.GetAllUserIdsAsync();
            return Ok(ids);
        }

    }
}
