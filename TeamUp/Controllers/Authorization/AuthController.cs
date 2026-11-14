using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamUpServices.Authorization.ABSTRACTION;
using TeamUpServices.Authorization.DTOS;

namespace TeamUpAPI.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
                return Unauthorized("Invalid email or password.");

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var isRegistered = await _authService.RegisterAsync(request);

            if (!isRegistered)
                return BadRequest("Email already exists or registration failed.");

            return Ok("User registered successfully.");
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto refreshDto)
        {
            var result = await _authService.RefreshTokenAsync(refreshDto);

            if (result == null)
                return Unauthorized("Invalid or expired refresh token. Please login again.");

            return Ok(result);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto logoutDto)
        {
            await _authService.LogoutAsync(logoutDto);

            //  ملاحظة أمنية: في الـ Logout دائماً نعيد Ok حتى لو كان التوكن خطأ 
            // لكي لا نكشف للهاكر ما إذا كان الإيميل موجوداً في نظامنا أم لا
            return Ok("Logged out successfully.");
        }
    }
}
