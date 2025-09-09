using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Services
{
    public class SuratService : ISuratService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SuratService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SuratResponseDto>> GetAllSuratsAsync()
        {
            var surats = await _context.Surats
                .Include(s => s.CreatedBy)
                .Include(s => s.AssignedTo)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SuratResponseDto>>(surats);
        }

        public async Task<SuratResponseDto?> GetSuratByIdAsync(int id)
        {
            var surat = await _context.Surats
                .Include(s => s.CreatedBy)
                .Include(s => s.AssignedTo)
                .FirstOrDefaultAsync(s => s.Id == id);

            return surat != null ? _mapper.Map<SuratResponseDto>(surat) : null;
        }

        public async Task<SuratResponseDto> CreateSuratAsync(SuratCreateDto suratDto, int userId)
        {
            var surat = _mapper.Map<Surat>(suratDto);
            surat.CreatedById = userId;
            surat.CreatedAt = DateTime.UtcNow;

            _context.Surats.Add(surat);
            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var createdSurat = await _context.Surats
                .Include(s => s.CreatedBy)
                .Include(s => s.AssignedTo)
                .FirstAsync(s => s.Id == surat.Id);

            return _mapper.Map<SuratResponseDto>(createdSurat);
        }

        public async Task<SuratResponseDto?> UpdateSuratAsync(int id, SuratUpdateDto suratDto, int userId, string userRole)
        {
            var surat = await _context.Surats
                .Include(s => s.CreatedBy)
                .Include(s => s.AssignedTo)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (surat == null)
                return null;

            // Authorization check: Only Admin or the creator can update
            if (userRole != "Admin" && surat.CreatedById != userId)
                return null;

            // Update only provided fields
            if (!string.IsNullOrEmpty(suratDto.NomorSurat))
                surat.NomorSurat = suratDto.NomorSurat;
            if (!string.IsNullOrEmpty(suratDto.Perihal))
                surat.Perihal = suratDto.Perihal;
            if (!string.IsNullOrEmpty(suratDto.IsiSurat))
                surat.IsiSurat = suratDto.IsiSurat;
            if (!string.IsNullOrEmpty(suratDto.Pengirim))
                surat.Pengirim = suratDto.Pengirim;
            if (!string.IsNullOrEmpty(suratDto.Penerima))
                surat.Penerima = suratDto.Penerima;
            if (suratDto.TanggalSurat.HasValue)
                surat.TanggalSurat = suratDto.TanggalSurat.Value;
            if (!string.IsNullOrEmpty(suratDto.Status))
                surat.Status = suratDto.Status;
            if (!string.IsNullOrEmpty(suratDto.JenisSurat))
                surat.JenisSurat = suratDto.JenisSurat;
            if (suratDto.Lampiran != null)
                surat.Lampiran = suratDto.Lampiran;
            if (suratDto.AssignedToId.HasValue)
                surat.AssignedToId = suratDto.AssignedToId.Value;

            surat.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<SuratResponseDto>(surat);
        }

        public async Task<bool> DeleteSuratAsync(int id, int userId, string userRole)
        {
            var surat = await _context.Surats.FindAsync(id);

            if (surat == null)
                return false;

            // Authorization check: Only Admin or the creator can delete
            if (userRole != "Admin" && surat.CreatedById != userId)
                return false;

            _context.Surats.Remove(surat);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SuratResponseDto>> GetSuratsByUserAsync(int userId)
        {
            var surats = await _context.Surats
                .Include(s => s.CreatedBy)
                .Include(s => s.AssignedTo)
                .Where(s => s.CreatedById == userId || s.AssignedToId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SuratResponseDto>>(surats);
        }
    }
}