using System.ComponentModel.DataAnnotations;

namespace SuratMenyuratAPI.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class UserUpdateDto
    {
        [StringLength(100)]
        public string? Username { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }
        
        [StringLength(100)]
        public string? FullName { get; set; }
        
        [StringLength(50)]
        public string? Role { get; set; }
    }
}