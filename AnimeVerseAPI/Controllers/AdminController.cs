using AnimeVerse;
using AnimeVerseAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AnimeVerseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AnimeVerseUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        public AdminController(UserManager<AnimeVerseUser> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            AnimeVerseUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Bad user name");
            }
            bool success = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!success)
            {
                return Unauthorized("Wrong password");
            }
            JwtSecurityToken secToken = await _jwtHandler.GetTokenAsync(user);
            string? jwtstr = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Token = jwtstr,
            });
        }

    }
}