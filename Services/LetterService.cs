using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuratMenyuratAPI.Data;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;
using System.Linq.Expressions;

namespace SuratMenyuratAPI.Services
{
    public class LetterService : ILetterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LetterService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LetterDto> CreateLetterAsync(CreateLetterDto createLetterDto, string senderId)
        {
            var letter = _mapper.Map<Letter>(createLetterDto);
            letter.SenderId = senderId;
            letter.LetterNumber = await GenerateLetterNumberAsync(createLetterDto.Type);

            _context.Letters.Add(letter);
            await _context.SaveChangesAsync();

            return await GetLetterByIdAsync(letter.Id, senderId);
        }

        public async Task<LetterDto> GetLetterByIdAsync(int id, string userId)
        {
            var letter = await _context.Letters
                .Include(l => l.Sender)
                .Include(l => l.Recipient)
                .Include(l => l.Attachments)
                .FirstOrDefaultAsync(l => l.Id == id && (l.SenderId == userId || l.RecipientId == userId));

            if (letter == null)
                throw new KeyNotFoundException("Letter not found or access denied");

            return _mapper.Map<LetterDto>(letter);
        }

        public async Task<PaginatedResult<LetterSummaryDto>> GetLettersAsync(LetterQueryDto query, string userId)
        {
            var queryable = _context.Letters
                .Include(l => l.Sender)
                .Include(l => l.Recipient)
                .Where(l => l.SenderId == userId || l.RecipientId == userId);

            // Apply filters
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(l => 
                    l.Subject.Contains(query.Search) || 
                    l.Content.Contains(query.Search) ||
                    l.LetterNumber.Contains(query.Search));
            }

            if (query.Type.HasValue)
                queryable = queryable.Where(l => l.Type == query.Type.Value);

            if (query.Status.HasValue)
                queryable = queryable.Where(l => l.Status == query.Status.Value);

            if (query.Priority.HasValue)
                queryable = queryable.Where(l => l.Priority == query.Priority.Value);

            if (!string.IsNullOrEmpty(query.SenderId))
                queryable = queryable.Where(l => l.SenderId == query.SenderId);

            if (!string.IsNullOrEmpty(query.RecipientId))
                queryable = queryable.Where(l => l.RecipientId == query.RecipientId);

            if (query.DateFrom.HasValue)
                queryable = queryable.Where(l => l.CreatedAt >= query.DateFrom.Value);

            if (query.DateTo.HasValue)
                queryable = queryable.Where(l => l.CreatedAt <= query.DateTo.Value);

            // Apply sorting
            queryable = ApplySorting(queryable, query.SortBy, query.SortDirection);

            var totalCount = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

            var letters = await queryable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            var letterDtos = _mapper.Map<List<LetterSummaryDto>>(letters);

            return new PaginatedResult<LetterSummaryDto>
            {
                Data = letterDtos,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = totalPages,
                HasNextPage = query.Page < totalPages,
                HasPreviousPage = query.Page > 1
            };
        }

        public async Task<LetterDto> UpdateLetterAsync(int id, UpdateLetterDto updateLetterDto, string userId)
        {
            var letter = await _context.Letters
                .FirstOrDefaultAsync(l => l.Id == id && l.SenderId == userId);

            if (letter == null)
                throw new KeyNotFoundException("Letter not found or access denied");

            if (letter.Status != LetterStatus.Draft)
                throw new InvalidOperationException("Only draft letters can be updated");

            _mapper.Map(updateLetterDto, letter);
            letter.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetLetterByIdAsync(id, userId);
        }

        public async Task<bool> DeleteLetterAsync(int id, string userId)
        {
            var letter = await _context.Letters
                .FirstOrDefaultAsync(l => l.Id == id && l.SenderId == userId);

            if (letter == null)
                return false;

            if (letter.Status != LetterStatus.Draft)
                throw new InvalidOperationException("Only draft letters can be deleted");

            _context.Letters.Remove(letter);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SendLetterAsync(int id, string userId)
        {
            var letter = await _context.Letters
                .FirstOrDefaultAsync(l => l.Id == id && l.SenderId == userId);

            if (letter == null)
                return false;

            if (letter.Status != LetterStatus.Draft)
                throw new InvalidOperationException("Only draft letters can be sent");

            letter.Status = LetterStatus.Sent;
            letter.SentAt = DateTime.UtcNow;
            letter.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarkAsReadAsync(int id, string userId)
        {
            var letter = await _context.Letters
                .FirstOrDefaultAsync(l => l.Id == id && l.RecipientId == userId);

            if (letter == null)
                return false;

            if (letter.Status == LetterStatus.Sent)
            {
                letter.Status = LetterStatus.Received;
                letter.ReceivedAt = DateTime.UtcNow;
            }

            if (letter.Status == LetterStatus.Received)
            {
                letter.Status = LetterStatus.Read;
                letter.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ArchiveLetterAsync(int id, string userId)
        {
            var letter = await _context.Letters
                .FirstOrDefaultAsync(l => l.Id == id && (l.SenderId == userId || l.RecipientId == userId));

            if (letter == null)
                return false;

            letter.Status = LetterStatus.Archived;
            letter.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GenerateLetterNumberAsync(LetterType type)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var typeCode = GetTypeCode(type);

            var lastNumber = await _context.Letters
                .Where(l => l.LetterNumber.StartsWith($"{typeCode}/{year:D4}/{month:D2}"))
                .OrderByDescending(l => l.LetterNumber)
                .Select(l => l.LetterNumber)
                .FirstOrDefaultAsync();

            int sequence = 1;
            if (lastNumber != null)
            {
                var parts = lastNumber.Split('/');
                if (parts.Length >= 4 && int.TryParse(parts[3], out int lastSequence))
                {
                    sequence = lastSequence + 1;
                }
            }

            return $"{typeCode}/{year:D4}/{month:D2}/{sequence:D3}";
        }

        public async Task<List<UserSummaryDto>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserSummaryDto>>(users);
        }

        private static string GetTypeCode(LetterType type)
        {
            return type switch
            {
                LetterType.Internal => "INT",
                LetterType.External => "EXT",
                LetterType.Memo => "MEM",
                LetterType.Official => "OFF",
                _ => "GEN"
            };
        }

        private static IQueryable<Letter> ApplySorting(IQueryable<Letter> queryable, string sortBy, string sortDirection)
        {
            var isDescending = sortDirection.ToLower() == "desc";

            Expression<Func<Letter, object>> keySelector = sortBy.ToLower() switch
            {
                "subject" => l => l.Subject,
                "type" => l => l.Type,
                "status" => l => l.Status,
                "priority" => l => l.Priority,
                "sentat" => l => l.SentAt ?? DateTime.MinValue,
                "receivedat" => l => l.ReceivedAt ?? DateTime.MinValue,
                "updatedat" => l => l.UpdatedAt,
                _ => l => l.CreatedAt
            };

            return isDescending 
                ? queryable.OrderByDescending(keySelector) 
                : queryable.OrderBy(keySelector);
        }
    }
}