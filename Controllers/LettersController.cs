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
    public class LettersController : ControllerBase
    {
        private readonly ILetterService _letterService;

        public LettersController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLetters([FromQuery] LetterQueryDto query)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _letterService.GetLettersAsync(query, userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLetter(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var letter = await _letterService.GetLetterByIdAsync(id, userId);
                return Ok(letter);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLetter([FromBody] CreateLetterDto createLetterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var letter = await _letterService.CreateLetterAsync(createLetterDto, userId);
            return CreatedAtAction(nameof(GetLetter), new { id = letter.Id }, letter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLetter(int id, [FromBody] UpdateLetterDto updateLetterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var letter = await _letterService.UpdateLetterAsync(id, updateLetterDto, userId);
                return Ok(letter);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLetter(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var result = await _letterService.DeleteLetterAsync(id, userId);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/send")]
        public async Task<IActionResult> SendLetter(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var result = await _letterService.SendLetterAsync(id, userId);
                if (!result)
                    return NotFound();

                return Ok(new { message = "Letter sent successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _letterService.MarkAsReadAsync(id, userId);
            if (!result)
                return NotFound();

            return Ok(new { message = "Letter marked as read" });
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> ArchiveLetter(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _letterService.ArchiveLetterAsync(id, userId);
            if (!result)
                return NotFound();

            return Ok(new { message = "Letter archived successfully" });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _letterService.GetUsersAsync();
            return Ok(users);
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}