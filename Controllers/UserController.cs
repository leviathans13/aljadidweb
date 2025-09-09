using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;
using System.Security.Claims;

namespace SuratMenyuratAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .OrderBy(u => u.Username)
                .ToListAsync();

            var userDtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserResponseDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            // Check if username or email already exists (excluding current user)
            if (!string.IsNullOrEmpty(userDto.Username) && userDto.Username != user.Username)
            {
                var existingUser = await _context.Users
                    .AnyAsync(u => u.Username == userDto.Username && u.Id != id);
                
                if (existingUser)
                {
                    return BadRequest(new { message = "Username sudah digunakan" });
                }
            }

            if (!string.IsNullOrEmpty(userDto.Email) && userDto.Email != user.Email)
            {
                var existingEmail = await _context.Users
                    .AnyAsync(u => u.Email == userDto.Email && u.Id != id);
                
                if (existingEmail)
                {
                    return BadRequest(new { message = "Email sudah digunakan" });
                }
            }

            // Update user properties
            if (!string.IsNullOrEmpty(userDto.Username))
                user.Username = userDto.Username;
            if (!string.IsNullOrEmpty(userDto.Email))
                user.Email = userDto.Email;
            if (!string.IsNullOrEmpty(userDto.FullName))
                user.FullName = userDto.FullName;
            if (!string.IsNullOrEmpty(userDto.Role))
                user.Role = userDto.Role;

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedUserDto = _mapper.Map<UserResponseDto>(user);
            return Ok(updatedUserDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            // Prevent admin from deleting themselves
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserIdClaim != null && int.TryParse(currentUserIdClaim, out int currentUserId) && currentUserId == id)
            {
                return BadRequest(new { message = "Tidak dapat menghapus akun sendiri" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}