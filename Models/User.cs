using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuratMenyuratAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Letter> SentLetters { get; set; } = new List<Letter>();
        public virtual ICollection<Letter> ReceivedLetters { get; set; } = new List<Letter>();
    }
}