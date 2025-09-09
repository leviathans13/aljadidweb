using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuratApi.Data;
using SuratApi.Dtos.Auth;
using SuratApi.Models;
using SuratApi.Services;

namespace SuratApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly AppDbContext _db;
	private readonly ITokenService _tokenService;

	public AuthController(AppDbContext db, ITokenService tokenService)
	{
		_db = db;
		_tokenService = tokenService;
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request)
	{
		if (await _db.Users.AnyAsync(u => u.Username == request.Username))
		{
			return Conflict(new { message = "Username sudah digunakan" });
		}

		var user = new User
		{
			Username = request.Username,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
			Role = UserRoles.User
		};
		_db.Users.Add(user);
		await _db.SaveChangesAsync();

		return Created($"/api/users/{user.Id}", new { user.Id, user.Username, user.Role });
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
		if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
		{
			return Unauthorized(new { message = "Username atau password salah" });
		}

		var token = _tokenService.GenerateToken(user);
		return Ok(new { token, role = user.Role, username = user.Username });
	}
}

