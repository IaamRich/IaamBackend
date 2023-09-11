using Core.Entities;
using UserManagementService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserController> _logger;

    public UserController(UserManager<UserEntity> userManager, 
        RoleManager<IdentityRole> roleManager,
            ILogger<UserController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(RegisterModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("Username and password must be provided.");
        }

        var newUser = new UserEntity { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        _logger.LogInformation($"The user with the email {model.Email} was created");
        return Ok(new { Message = "Registration successful" });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("User ID must be provided.");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            _logger.LogInformation($"User with ID {id} not found.");
            return NotFound($"User with ID {id} doesn't exist.");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            _logger.LogInformation($"User with ID {id} was deleted.");
            return Ok($"User with ID {id} has been deleted.");
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole(string userId, string roleName)
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
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
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

    [HttpPost("AddClaim")]
    public async Task<IActionResult> AddClaim(string userId, string claimType, string claimValue)
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

    [HttpPost("RemoveClaim")]
    public async Task<IActionResult> RemoveClaim(string userId, string claimType, string claimValue)
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