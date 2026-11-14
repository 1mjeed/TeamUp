using TeamUpServices.Authorization.DTOS;

namespace TeamUpServices.Authorization.ABSTRACTION
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto);
        Task<bool> RegisterAsync(RegisterRequestDto request);

        Task<TokenResponseDto?> RefreshTokenAsync(RefreshRequestDto refreshDto);
        Task<bool> LogoutAsync(LogoutRequestDto logoutDto);
    }
}
