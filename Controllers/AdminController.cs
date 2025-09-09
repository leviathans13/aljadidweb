using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuratMenyuratAPI.Data;
using SuratMenyuratAPI.DTOs;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var totalCount = await _context.Users.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var users = await _context.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = roles.ToList();
                userDtos.Add(userDto);
            }

            var result = new PaginatedResult<UserDto>
            {
                Data = userDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasNextPage = page < totalPages,
                HasPreviousPage = page > 1
            };

            return Ok(result);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles.ToList();

            return Ok(userDto);
        }

        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(string id, [FromBody] UpdateUserRoleDto updateRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            
            if (!removeResult.Succeeded)
                return BadRequest(removeResult.Errors);

            var addResult = await _userManager.AddToRoleAsync(user, updateRoleDto.Role);
            if (!addResult.Succeeded)
                return BadRequest(addResult.Errors);

            return Ok(new { message = "User role updated successfully" });
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // Check if user has any letters
            var hasLetters = await _context.Letters
                .AnyAsync(l => l.SenderId == id || l.RecipientId == id);

            if (hasLetters)
                return BadRequest("Cannot delete user with existing letters");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpGet("letters")]
        public async Task<IActionResult> GetAllLetters([FromQuery] LetterQueryDto query)
        {
            var queryable = _context.Letters
                .Include(l => l.Sender)
                .Include(l => l.Recipient)
                .AsQueryable();

            // Apply filters (similar to LetterService but without user restriction)
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(l => 
                    l.Subject.Contains(query.Search) || 
                    l.Content.Contains(query.Search) ||
                    l.LetterNumber.Contains(query.Search));
            }

            if (query.Type.HasValue)
                queryable = queryable.Where(l => l.Type == query.Type.Value);

            if (query.Status.HasValue)
                queryable = queryable.Where(l => l.Status == query.Status.Value);

            if (query.Priority.HasValue)
                queryable = queryable.Where(l => l.Priority == query.Priority.Value);

            if (!string.IsNullOrEmpty(query.SenderId))
                queryable = queryable.Where(l => l.SenderId == query.SenderId);

            if (!string.IsNullOrEmpty(query.RecipientId))
                queryable = queryable.Where(l => l.RecipientId == query.RecipientId);

            if (query.DateFrom.HasValue)
                queryable = queryable.Where(l => l.CreatedAt >= query.DateFrom.Value);

            if (query.DateTo.HasValue)
                queryable = queryable.Where(l => l.CreatedAt <= query.DateTo.Value);

            var totalCount = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

            var letters = await queryable
                .OrderByDescending(l => l.CreatedAt)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            var letterDtos = _mapper.Map<List<LetterSummaryDto>>(letters);

            var result = new PaginatedResult<LetterSummaryDto>
            {
                Data = letterDtos,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = totalPages,
                HasNextPage = query.Page < totalPages,
                HasPreviousPage = query.Page > 1
            };

            return Ok(result);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalLetters = await _context.Letters.CountAsync();
            var draftLetters = await _context.Letters.CountAsync(l => l.Status == LetterStatus.Draft);
            var sentLetters = await _context.Letters.CountAsync(l => l.Status == LetterStatus.Sent);
            var readLetters = await _context.Letters.CountAsync(l => l.Status == LetterStatus.Read);
            var archivedLetters = await _context.Letters.CountAsync(l => l.Status == LetterStatus.Archived);

            var statistics = new
            {
                TotalUsers = totalUsers,
                TotalLetters = totalLetters,
                LettersByStatus = new
                {
                    Draft = draftLetters,
                    Sent = sentLetters,
                    Read = readLetters,
                    Archived = archivedLetters
                },
                LettersByType = await _context.Letters
                    .GroupBy(l => l.Type)
                    .Select(g => new { Type = g.Key.ToString(), Count = g.Count() })
                    .ToListAsync(),
                LettersByPriority = await _context.Letters
                    .GroupBy(l => l.Priority)
                    .Select(g => new { Priority = g.Key.ToString(), Count = g.Count() })
                    .ToListAsync()
            };

            return Ok(statistics);
        }
    }

    public class UpdateUserRoleDto
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}