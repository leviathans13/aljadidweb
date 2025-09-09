using System.ComponentModel.DataAnnotations;

namespace SuratMenyuratAPI.DTOs
{
    public class SuratCreateDto
    {
        [Required]
        [StringLength(200)]
        public string NomorSurat { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Perihal { get; set; } = string.Empty;
        
        [Required]
        public string IsiSurat { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Pengirim { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Penerima { get; set; } = string.Empty;
        
        public DateTime TanggalSurat { get; set; } = DateTime.UtcNow;
        
        [StringLength(50)]
        public string Status { get; set; } = "Draft";
        
        [StringLength(50)]
        public string JenisSurat { get; set; } = "Biasa";
        
        public string? Lampiran { get; set; }
        
        public int? AssignedToId { get; set; }
    }

    public class SuratUpdateDto
    {
        [StringLength(200)]
        public string? NomorSurat { get; set; }
        
        [StringLength(500)]
        public string? Perihal { get; set; }
        
        public string? IsiSurat { get; set; }
        
        [StringLength(100)]
        public string? Pengirim { get; set; }
        
        [StringLength(100)]
        public string? Penerima { get; set; }
        
        public DateTime? TanggalSurat { get; set; }
        
        [StringLength(50)]
        public string? Status { get; set; }
        
        [StringLength(50)]
        public string? JenisSurat { get; set; }
        
        public string? Lampiran { get; set; }
        
        public int? AssignedToId { get; set; }
    }

    public class SuratResponseDto
    {
        public int Id { get; set; }
        public string NomorSurat { get; set; } = string.Empty;
        public string Perihal { get; set; } = string.Empty;
        public string IsiSurat { get; set; } = string.Empty;
        public string Pengirim { get; set; } = string.Empty;
        public string Penerima { get; set; } = string.Empty;
        public DateTime TanggalSurat { get; set; }
        public string Status { get; set; } = string.Empty;
        public string JenisSurat { get; set; } = string.Empty;
        public string? Lampiran { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByUsername { get; set; } = string.Empty;
        public int? AssignedToId { get; set; }
        public string? AssignedToUsername { get; set; }
    }
}