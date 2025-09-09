using System.ComponentModel.DataAnnotations;

namespace SuratMenyuratAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty; // "Admin" atau "User"
        
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Surat> SuratDibuat { get; set; } = new List<Surat>();
        public virtual ICollection<Surat> SuratDiterima { get; set; } = new List<Surat>();
    }
}