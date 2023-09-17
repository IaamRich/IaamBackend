using Core.Entities;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<UserEntity> userManager,
            JwtService jwtService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken(GetTokenModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userClaims = await _userManager.GetClaimsAsync(user);
                var token = await _jwtService.GenerateTokenAsync(user, roles, userClaims);

                _logger.LogInformation($"The user with the username {model.Username} has logged in");
                return Ok(new { Token = token });
            }



            return Unauthorized();
        }
    }
}