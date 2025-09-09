using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Services;
using System.Security.Claims;

namespace SuratMenyuratAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SuratController : ControllerBase
    {
        private readonly ISuratService _suratService;

        public SuratController(ISuratService suratService)
        {
            _suratService = suratService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSurats()
        {
            var surats = await _suratService.GetAllSuratsAsync();
            return Ok(surats);
        }

        [HttpGet("my-surats")]
        public async Task<IActionResult> GetMySurats()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var surats = await _suratService.GetSuratsByUserAsync(userId);
            return Ok(surats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurat(int id)
        {
            var surat = await _suratService.GetSuratByIdAsync(id);
            
            if (surat == null)
            {
                return NotFound();
            }

            // Authorization check: User can only view their own surats unless they're Admin
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userRole != "Admin" && userIdClaim != null && int.TryParse(userIdClaim, out int userId))
            {
                if (surat.CreatedById != userId && surat.AssignedToId != userId)
                {
                    return Forbid();
                }
            }

            return Ok(surat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurat([FromBody] SuratCreateDto suratDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var surat = await _suratService.CreateSuratAsync(suratDto, userId);
            return CreatedAtAction(nameof(GetSurat), new { id = surat.Id }, surat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurat(int id, [FromBody] SuratUpdateDto suratDto)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var surat = await _suratService.UpdateSuratAsync(id, suratDto, userId, userRole ?? "User");
            
            if (surat == null)
            {
                return NotFound();
            }

            return Ok(surat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurat(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var result = await _suratService.DeleteSuratAsync(id, userId, userRole ?? "User");
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}