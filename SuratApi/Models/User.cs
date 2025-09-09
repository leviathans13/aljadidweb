using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratApi.Models;

public static class UserRoles
{
	public const string Admin = "Admin";
	public const string User = "User";
}

public class User
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(100)]
	public string Username { get; set; } = string.Empty;

	[Required]
	[StringLength(200)]
	public string PasswordHash { get; set; } = string.Empty;

	[Required]
	[StringLength(20)]
	public string Role { get; set; } = UserRoles.User;

	public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

	// Navigation
	public ICollection<Surat> OwnedSurat { get; set; } = new List<Surat>();
}

