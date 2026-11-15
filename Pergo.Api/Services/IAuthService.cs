
using Pergo.Api.DTOs;
using Pergo.Api.Models;

namespace Pergo.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        string GenerateJwtToken(ApplicationUser user);
    }
}