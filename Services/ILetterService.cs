using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Services
{
    public interface ILetterService
    {
        Task<LetterDto> CreateLetterAsync(CreateLetterDto createLetterDto, string senderId);
        Task<LetterDto> GetLetterByIdAsync(int id, string userId);
        Task<PaginatedResult<LetterSummaryDto>> GetLettersAsync(LetterQueryDto query, string userId);
        Task<LetterDto> UpdateLetterAsync(int id, UpdateLetterDto updateLetterDto, string userId);
        Task<bool> DeleteLetterAsync(int id, string userId);
        Task<bool> SendLetterAsync(int id, string userId);
        Task<bool> MarkAsReadAsync(int id, string userId);
        Task<bool> ArchiveLetterAsync(int id, string userId);
        Task<string> GenerateLetterNumberAsync(LetterType type);
        Task<List<UserSummaryDto>> GetUsersAsync();
    }
}