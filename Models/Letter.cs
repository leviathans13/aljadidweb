using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratMenyuratAPI.Models
{
    public class Letter
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LetterNumber { get; set; } = string.Empty;

        [Required]
        public LetterType Type { get; set; }

        [Required]
        public LetterStatus Status { get; set; } = LetterStatus.Draft;

        [Required]
        public Priority Priority { get; set; } = Priority.Normal;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public DateTime? ReceivedAt { get; set; }

        // Foreign Keys
        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        public string RecipientId { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; } = null!;

        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; } = null!;

        public virtual ICollection<LetterAttachment> Attachments { get; set; } = new List<LetterAttachment>();
    }

    public enum LetterType
    {
        Internal = 1,
        External = 2,
        Memo = 3,
        Official = 4
    }

    public enum LetterStatus
    {
        Draft = 1,
        Sent = 2,
        Received = 3,
        Read = 4,
        Archived = 5
    }

    public enum Priority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4
    }
}