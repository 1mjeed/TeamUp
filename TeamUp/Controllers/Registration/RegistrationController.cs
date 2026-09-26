using Microsoft.AspNetCore.Mvc;
using TeamUpServices.REGISTRATIONS.ABSTRACTION;
using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpAPI.Controllers.Registration
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationCommand _command;

        public RegistrationController(IRegistrationCommand command)
        {
            _command = command;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegistrationAsync(RegistrationCreateDto registrationCreateDto)
        {
            if (registrationCreateDto == null)
                return BadRequest();

            var newR = await _command.CreateRegistrationAsync(registrationCreateDto);
            return Ok(newR);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegistrationStatusAsync(RegistrationUpdateStatusDto registrationUpdateStatusDto)
        {
            if (registrationUpdateStatusDto == null) return BadRequest();

            var updateR = await _command.UpdateRegistrationStatusAsync(registrationUpdateStatusDto);

            if (!updateR)
                return NotFound();

            return Ok(updateR);

        }
    }
}
