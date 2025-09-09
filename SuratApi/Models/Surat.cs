using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SuratApi.Models;

public class Surat
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(50)]
	public string Nomor { get; set; } = string.Empty;

	[Required]
	[StringLength(200)]
	public string Judul { get; set; } = string.Empty;

	[Required]
	[StringLength(4000)]
	public string Isi { get; set; } = string.Empty;

	[Required]
	public DateTime TanggalSurat { get; set; } = DateTime.UtcNow;

	[Required]
	public Guid OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	[JsonIgnore]
	public User? Owner { get; set; }

	public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAtUtc { get; set; }
}

