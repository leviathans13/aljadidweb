using System.ComponentModel.DataAnnotations;

namespace SuratApi.Dtos.Auth;

public class RegisterRequest
{
	[Required]
	[StringLength(100)]
	public string Username { get; set; } = string.Empty;

	[Required]
	[StringLength(100, MinimumLength = 6)]
	public string Password { get; set; } = string.Empty;
}

