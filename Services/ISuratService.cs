using SuratMenyuratAPI.DTOs;

namespace SuratMenyuratAPI.Services
{
    public interface ISuratService
    {
        Task<IEnumerable<SuratResponseDto>> GetAllSuratsAsync();
        Task<SuratResponseDto?> GetSuratByIdAsync(int id);
        Task<SuratResponseDto> CreateSuratAsync(SuratCreateDto suratDto, int userId);
        Task<SuratResponseDto?> UpdateSuratAsync(int id, SuratUpdateDto suratDto, int userId, string userRole);
        Task<bool> DeleteSuratAsync(int id, int userId, string userRole);
        Task<IEnumerable<SuratResponseDto>> GetSuratsByUserAsync(int userId);
    }
}