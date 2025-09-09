using System.ComponentModel.DataAnnotations;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.DTOs
{
    public class CreateLetterDto
    {
        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public LetterType Type { get; set; }

        [Required]
        public string RecipientId { get; set; } = string.Empty;

        public Priority Priority { get; set; } = Priority.Normal;
    }

    public class UpdateLetterDto
    {
        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public LetterType Type { get; set; }

        [Required]
        public string RecipientId { get; set; } = string.Empty;

        public Priority Priority { get; set; } = Priority.Normal;
    }

    public class LetterDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string LetterNumber { get; set; } = string.Empty;
        public LetterType Type { get; set; }
        public LetterStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public UserSummaryDto Sender { get; set; } = null!;
        public UserSummaryDto Recipient { get; set; } = null!;
        public List<LetterAttachmentDto> Attachments { get; set; } = new();
    }

    public class LetterSummaryDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string LetterNumber { get; set; } = string.Empty;
        public LetterType Type { get; set; }
        public LetterStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
        public UserSummaryDto Sender { get; set; } = null!;
        public UserSummaryDto Recipient { get; set; } = null!;
    }

    public class UserSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class LetterAttachmentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }

    public class LetterQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public LetterType? Type { get; set; }
        public LetterStatus? Status { get; set; }
        public Priority? Priority { get; set; }
        public string? SenderId { get; set; }
        public string? RecipientId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string SortBy { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "desc";
    }

    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}