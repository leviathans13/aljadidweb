using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}