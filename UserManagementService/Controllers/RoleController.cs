using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RoleController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleController> _logger;

    public RoleController(UserManager<UserEntity> userManager, 
        RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpPost("FindById")]
    public async Task<IActionResult> FindById(string roleId)
    {
        if (string.IsNullOrEmpty(roleId))
        {
            return BadRequest("Role ID must be provided.");
        }

        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
            return NotFound($"Role with ID {roleId} not found.");
        }

        return Ok(role);
    }

    [HttpPost("FindByName")]
    public async Task<IActionResult> FindByName(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            return BadRequest("Role name must be provided.");
        }

        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null)
        {
            return NotFound($"Role with name {roleName} not found.");
        }

        return Ok(role);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            return BadRequest("Role name must be set.");
        }

        if (await _roleManager.RoleExistsAsync(roleName))
        {
            return BadRequest("Role already exists.");
        }

        var identityRole = new IdentityRole(roleName);
        var result = await _roleManager.CreateAsync(identityRole);

        if (result.Succeeded)
        {
            return Ok($"Role {roleName} has been created.");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            return BadRequest("Role name must be provided.");
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return NotFound($"Role with the name {roleName} doesn't exist.");
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        if (usersInRole.Count > 0)
        {
            return BadRequest($"Cannot delete role {roleName} because it is assigned to {usersInRole.Count} users.");
        }

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return Ok($"Role {roleName} has been deleted.");
        }
        else
        {
            return BadRequest("Failed to delete role.");
        }
    }
}