using apiProject.Application.Dtos;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("نام کاربری یا رمز عبور اشتباه است");

            return Ok(result);
        }

        [HttpPost("SelectRole")]
        public async Task<IActionResult> SelectRole([FromBody] SelectRoleDto dto)
        {
            var result = await _authService.SelectRoleAsync(dto.UserId, dto.RoleId);
            if (result == null)
                return BadRequest("نقش انتخاب شده معتبر نیست");

            return Ok(result);
        }
    }
}
