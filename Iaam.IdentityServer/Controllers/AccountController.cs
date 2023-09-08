using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Iaam.IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<UserEntity> userManager, 
        RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            _logger.LogInformation($"User with ID {id} not found.");
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            _logger.LogInformation($"User with ID {id} was deleted.");
            return NoContent();
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
    {
        // Ensure the role exists
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            // Create new role
            var role = new IdentityRole(roleName);
            var roleResult = await _roleManager.CreateAsync(role);

            if (!roleResult.Succeeded)
            {
                return BadRequest("Failed to create role");
            }
        }

        // Fetch the user
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        // Add role to user
        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            return Ok($"Role {roleName} added to user {userId}");
        }

        return BadRequest("Failed to add role to user");
    }

    [HttpPost("RemoveRole")]
    public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            return Ok($"Role {roleName} removed from user {userId}");
        }

        return BadRequest("Failed to remove role from user");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AddClaim")]
    public async Task<IActionResult> AddClaimToUser(string userId, string claimType, string claimValue)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var claim = new Claim(claimType, claimValue);
        var result = await _userManager.AddClaimAsync(user, claim);

        if (result.Succeeded)
        {
            return Ok($"Claim {claimType} added to user {userId}");
        }

        return BadRequest("Failed to add claim to user");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("RemoveClaim")]
    public async Task<IActionResult> RemoveClaimFromUser(string userId, string claimType, string claimValue)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var claim = new Claim(claimType, claimValue);
        var result = await _userManager.RemoveClaimAsync(user, claim);

        if (result.Succeeded)
        {
            return Ok($"Claim {claimType} removed from user {userId}");
        }

        return BadRequest("Failed to remove claim from user");
    }
}