using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamUpCore.Model;
using TeamUpData;
using TeamUpServices.Auth.ABSTRACTION;
using TeamUpServices.Auth.DTOS;
//Authenuiction (JWT)

namespace TeamUpServices.Auth.IMPLEMENTATION
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

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
               .Include(u => u.Role)
               .FirstOrDefaultAsync(u => u.Email == request.Email && !u.ISDELETED);


            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash); ;
            

            if (!isPasswordValid)
            {
                return null;
            }

            string token = GenerateJwtToken(user.Email, user.Name, user.Role.Name, user.Id);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.Name
            };
        }

        private string GenerateJwtToken(string email, string name, string role, int userId)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                //new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
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

             var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                 Phone = request.Phone,
                 PasswordHash = hashedPassword,  
                RoleId = 2// عشان يكون طالب اي شخص بسجل بهاي الطريقه هس الفكره انه الادمن بقدر يعطي رول ثانيات 
            };
            
            await _context.Users.AddAsync(newUser);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}