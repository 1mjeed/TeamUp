using TeamUpServices.Auth.DTOS;

namespace TeamUpServices.Auth.ABSTRACTION
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

        Task<bool> RegisterAsync(RegisterRequestDto request);
    }
}
