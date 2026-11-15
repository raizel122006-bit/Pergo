
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pergo.Api.Configuration;
using Pergo.Api.DTOs;
using Pergo.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;


namespace Pergo.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value; 
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            //Check if user exists already

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if(existingUser != null)
            {
                throw new Exception("A user with this email already exists");
            }

            // Create a new user
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };

            // Save user to database
            var result = await _userManager.CreateAsync(user, dto.Password);

            // Check whether registration succeeded
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // Generate the jwt token

            var token = GenerateJwtToken(user);

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            return new AuthResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Email = user.Email!,
                ExpiresAt = expiresAt
            };
        }   

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {   
            //Find the user
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null)
            {
                throw new Exception("Invalid email or password.");

            }

            // Verfiy password

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password");

            }

            //Generate jwt token
            var token = GenerateJwtToken(user);

            // Calculate expiration
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            // Return authentication response
            return new AuthResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Email = user.Email!,
                ExpiresAt = expiresAt
            };


        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));


            //Create signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the jwt
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            // Convert token to string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}