using SuratMenyuratAPI.DTOs;

namespace SuratMenyuratAPI.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto loginDto);
        Task<UserResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<UserResponseDto?> GetUserByIdAsync(int userId);
    }
}