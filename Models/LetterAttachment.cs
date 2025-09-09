using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratMenyuratAPI.Models
{
    public class LetterAttachment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [Required]
        public int LetterId { get; set; }

        // Navigation Property
        [ForeignKey("LetterId")]
        public virtual Letter Letter { get; set; } = null!;
    }
}