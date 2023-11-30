using AnimeVerseAPI.Data;
using AnimeVerse;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace AnimeVerseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly AnimeVerseContext _db;
        private readonly UserManager<AnimeVerseUser> _userManager;
        private readonly string _pathName;

        public SeedController(AnimeVerseContext db, IWebHostEnvironment environment, UserManager<AnimeVerseUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _pathName = Path.Combine(environment.ContentRootPath);
        }

        [HttpPost(template: "Users")]
        public async Task<IActionResult> ImportUsersAsync()
        {

            (string name, string email) = ("user", "user@email.com");
            AnimeVerseUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await _userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user1";
            }
            _ = await _userManager.CreateAsync(user, password: "P@ssw0rd!")
                   ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await _db.SaveChangesAsync();

            return Ok();
        }

    }
}
