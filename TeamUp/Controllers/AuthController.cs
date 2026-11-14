using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamUpServices.Auth.ABSTRACTION;
using TeamUpServices.Auth.DTOS;

namespace TeamUpAPI.Controllers
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

        [HttpPost("register")]
        [AllowAnonymous]  
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var isRegistered = await _authService.RegisterAsync(request);

            if (!isRegistered)
            {
                 return BadRequest(new { message = "هذا الإيميل مسجل مسبقاً، يرجى استخدام إيميل آخر أو تسجيل الدخول." });
            }

            return Ok(new { message = "تم إنشاء الحساب بنجاح! يمكنك الآن تسجيل الدخول." });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized(new { message = "الإيميل أو كلمة المرور غير صحيحة" });
            }

            return Ok(response);
        }
    }
}