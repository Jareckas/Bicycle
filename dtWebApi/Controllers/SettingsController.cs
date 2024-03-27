using dtWebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using dtWebApi.Models;

namespace dtWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SettingsController> _logger;
        private readonly AppDbContext _dbContext;

        public SettingsController(UserManager<IdentityUser> userManager, ILogger<SettingsController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpPost ("UpdateUsername")]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameModel model)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Update the username in the Settings table
                var settings = await _dbContext.Settings.FindAsync(userId);

                settings.UserName = model.NewUsername;
                _dbContext.Settings.Update(settings);
                await _dbContext.SaveChangesAsync();

                return Ok("Username updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the username.");
                return StatusCode(500, "Failed to update username.");
            }
        }

        [HttpGet("GetUsername")]
        public async Task<IActionResult> GetUsername()
        {
            try
            {
                // Retrieve the user ID from the claims
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Find the corresponding settings entry for the user
                var settings = await _dbContext.Settings.FindAsync(userId);
                if (settings == null)
                {
                    return NotFound("Settings not found for the user.");
                }

                // Return the username from the settings as a response
                return Ok(settings.UserName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the username.");
                return StatusCode(500, "Failed to get username.");
            }

        }

        [HttpPost("SettingsEntry")]
        public async Task<IActionResult> CreateSettingsEntry([FromQuery] string email)
        {
            try
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Check if a settings entry already exists for the user
                var existingSettings = _dbContext.Settings.FirstOrDefault(s => s.UserId == user.Id);
                if (existingSettings != null)
                {
                    return Conflict("Settings entry already exists for this user.");
                }

                var atIndex = user.Email.IndexOf("@");
                var defaultUsername = user.Email.Substring(0, atIndex);
                // Create a new settings entry
                var newSettings = new Settings
                {
                    UserId = user.Id,
                    UserName = defaultUsername
                };

                _dbContext.Settings.Add(newSettings);
                await _dbContext.SaveChangesAsync();

                return Ok("Settings entry created successfully.");
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while creating settings entry.");
                return StatusCode(500, "Failed to create settings entry.");
            }

        }
    }
}
