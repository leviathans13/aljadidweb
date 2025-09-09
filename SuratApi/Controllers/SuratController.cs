using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SuratApi.Data;
using SuratApi.Models;

namespace SuratApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SuratController : ControllerBase
{
	private readonly AppDbContext _db;

	public SuratController(AppDbContext db)
	{
		_db = db;
	}

	private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(ClaimTypes.Name) ?? throw new InvalidOperationException("Invalid token claims"));
	private bool IsAdmin() => User.IsInRole(UserRoles.Admin);

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		if (IsAdmin())
		{
			var all = await _db.Surat.AsNoTracking().ToListAsync();
			return Ok(all);
		}
		var userId = GetUserId();
		var mine = await _db.Surat.AsNoTracking().Where(s => s.OwnerId == userId).ToListAsync();
		return Ok(mine);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var surat = await _db.Surat.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
		if (surat == null) return NotFound();
		if (!IsAdmin() && surat.OwnerId != GetUserId()) return Forbid();
		return Ok(surat);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Surat surat)
	{
		if (!IsAdmin())
		{
			surat.OwnerId = GetUserId();
		}
		else if (surat.OwnerId == Guid.Empty)
		{
			surat.OwnerId = GetUserId();
		}

		_db.Surat.Add(surat);
		await _db.SaveChangesAsync();
		return CreatedAtAction(nameof(GetById), new { id = surat.Id }, surat);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] Surat update)
	{
		var surat = await _db.Surat.FirstOrDefaultAsync(s => s.Id == id);
		if (surat == null) return NotFound();
		if (!IsAdmin() && surat.OwnerId != GetUserId()) return Forbid();

		surat.Nomor = update.Nomor;
		surat.Judul = update.Judul;
		surat.Isi = update.Isi;
		surat.TanggalSurat = update.TanggalSurat;
		surat.UpdatedAtUtc = DateTime.UtcNow;
		if (IsAdmin() && update.OwnerId != Guid.Empty)
		{
			surat.OwnerId = update.OwnerId;
		}

		await _db.SaveChangesAsync();
		return Ok(surat);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var surat = await _db.Surat.FirstOrDefaultAsync(s => s.Id == id);
		if (surat == null) return NotFound();
		if (!IsAdmin() && surat.OwnerId != GetUserId()) return Forbid();

		_db.Surat.Remove(surat);
		await _db.SaveChangesAsync();
		return NoContent();
	}
}

