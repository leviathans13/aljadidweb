using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        string GenerateRefreshToken();
        Task<bool> ValidateTokenAsync(string token);
    }
}