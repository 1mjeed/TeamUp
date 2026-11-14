using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamUpServices.Authorization.ABSTRACTION;
using TeamUpServices.Authorization.DTOS;
using Microsoft.AspNetCore.RateLimiting;

namespace TeamUpAPI.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService , ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto )
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                _logger.LogWarning("Failed login attempt. Email={Email}, IP={IP}", loginDto.Email, ip);
                return Unauthorized("Invalid email or password.");
            }

            _logger.LogInformation("Successful login. Email={Email}, IP={IP}", loginDto.Email, ip);

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
        [EnableRateLimiting("AuthLimiter")] 
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto refreshDto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var result = await _authService.RefreshTokenAsync(refreshDto);

            if (result == null)
            {

                _logger.LogInformation("Successful login. Email={Email}, IP={IP}", refreshDto.Email, ip);
                return Unauthorized("Invalid or expired refresh token. Please login again.");
            }

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
