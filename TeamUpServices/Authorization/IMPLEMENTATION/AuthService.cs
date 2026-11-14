using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamUpCore.Model;
using TeamUpData;
using TeamUpServices.Authorization.ABSTRACTION;
using TeamUpServices.Authorization.DTOS;
//Authenuiction (JWT)

namespace TeamUpServices.Authorization.IMPLEMENTATION
{
    public class AuthService : IAuthService
    {
        private readonly TeamUpDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(TeamUpDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

             if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            var jwtToken = GenerateJwtToken(user);

            var refreshToken = GenerateRefreshToken();

            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            user.RefreshTokenRevokedAt = null;

            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<bool> LogoutAsync(LogoutRequestDto logoutDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == logoutDto.Email);

            if (user == null) return true;  

            bool isRefreshValid = BCrypt.Net.BCrypt.Verify(logoutDto.RefreshToken, user.RefreshTokenHash);
            if (!isRefreshValid) return true;
            
            user.RefreshTokenRevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
             bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == request.Email);

            if (emailExists)
            {
                return false; 
            }

            // (Hashing with Salt)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            //  انا هون ما كتبته مانيول مابنق لاني حساسه تعقيد زائد في هاي الحاله 
             var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                 Phone = request.Phone,
                 PasswordHash = hashedPassword,  
                RoleId =1 // عشان يكون طالب اي شخص بسجل بهاي الطريقه هس الفكره انه الادمن بقدر يعطي رول ثانيات 
            };
            
            await _context.Users.AddAsync(newUser);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshRequestDto refreshDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == refreshDto.Email);

             if (user == null) return null;

            if (user.RefreshTokenRevokedAt != null) return null; 

            if (user.RefreshTokenExpiresAt == null || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
                return null; 

             
            bool isRefreshValid = BCrypt.Net.BCrypt.Verify(refreshDto.RefreshToken, user.RefreshTokenHash);
            if (!isRefreshValid) return null;

             
            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken);
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            user.RefreshTokenRevokedAt = null;  

            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }

        private static string GenerateRefreshToken()
        {
             var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}