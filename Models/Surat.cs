using System.ComponentModel.DataAnnotations;

namespace SuratMenyuratAPI.Models
{
    public class Surat
    {
        public int Id { get; set; }
        
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
        public string Status { get; set; } = "Draft"; // Draft, Terkirim, Diterima, Ditolak
        
        [StringLength(50)]
        public string JenisSurat { get; set; } = "Biasa"; // Biasa, Penting, Rahasia
        
        public string? Lampiran { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign Keys
        public int CreatedById { get; set; }
        public int? AssignedToId { get; set; }
        
        // Navigation properties
        public virtual User CreatedBy { get; set; } = null!;
        public virtual User? AssignedTo { get; set; }
    }
}