
using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpServices.REGISTRATIONS.ABSTRACTION
{
    public interface IRegistrationCommand
    {
        Task<bool> CreateRegistrationAsync(RegistrationCreateDto createDto);
         Task<bool> UpdateRegistrationStatusAsync( RegistrationUpdateStatusDto updateDto);
       // Task<bool> DeleteRegistrationAsync(int registrationId, int requesterId);
    }
}
