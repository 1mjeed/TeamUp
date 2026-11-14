using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamUpServices.REGISTRATIONS.ABSTRACTION;
using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpAPI.Controllers.Registration
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        #region Dependence Injection
        private readonly IRegistrationCommand _command;

        public RegistrationController(IRegistrationCommand command)
        {
            _command = command;
        }
        #endregion

        #region  Create Registration
        [Authorize(Roles = "Student")]// لازم تتعدل 
        [HttpPost]
        public async Task<IActionResult> CreateRegistrationAsync(RegistrationCreateDto registrationCreateDto)
        {
            if (registrationCreateDto == null)
                return BadRequest();

            var newR = await _command.CreateRegistrationAsync(registrationCreateDto);
            return Ok(newR);
        }
        #endregion

        #region  Update Registration
        [Authorize(Roles = "Student, Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateRegistrationStatusAsync(RegistrationUpdateStatusDto registrationUpdateStatusDto)
        {
            if (registrationUpdateStatusDto == null) return BadRequest();

            var updateR = await _command.UpdateRegistrationStatusAsync(registrationUpdateStatusDto);

            if (!updateR)
                return NotFound();

            return Ok(updateR);

        }
        #endregion
    }
}
//الثغرة الخفية(تنتظر درس الـ Policy): بما أن أي طالب يستطيع الدخول، ما الذي يمنع الطالب "علي" من إرسال DTO يحتوي على StudentId الخاص بالطالب "عمر"، ويورطه في الانضمام لمشروع لا يريده؟!

//الحل القادم: سيعلمك الدكتور كيف تقرأ الـ ID من الـ Token مباشرة وتقارنه بالـ DTO (هل Token.Id == DTO.StudentId؟).