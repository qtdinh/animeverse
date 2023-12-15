using AnimeVerseAPI.Data;
using CsvHelper;
using CsvHelper.Configuration;
using AnimeVerse;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Globalization;
using System;
using System.IO;
using System.Threading.Tasks;


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
            _pathName = Path.Combine(environment.ContentRootPath, "Data/AnimeVerse.csv");
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

        [HttpPost("Series")]
        public async Task<IActionResult> ImportSeriesItemAsync()
        {
            // create a lookup dictionary containing all the series already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, SeriesItem> seriesByName = _db.Series
                .AsNoTracking().ToDictionary(x => x.Title, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<AnimeVerseCsv> records = csv.GetRecords<AnimeVerseCsv>().ToList();
            foreach (AnimeVerseCsv record in records)
            {
                if (seriesByName.ContainsKey(record.seriesItem))
                {
                    continue;
                }

                SeriesItem seriesItem = new()
                {
                    Title = record.seriesItem,
                    Year = record.year,
                    Demographic = record.demographic
                };
                await _db.Series.AddAsync(seriesItem);
                seriesByName.Add(record.seriesItem, seriesItem);
            }

            await _db.SaveChangesAsync();

            return new JsonResult(seriesByName.Count);

        }

        [HttpPost("Characters")]
        public async Task<IActionResult> ImportCharactersItemAsync()
        {
            // create a lookup dictionary containing all the series already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Character> charactersByName = _db.Characters
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<AnimeVerseCsv> records = csv.GetRecords<AnimeVerseCsv>().ToList();
            foreach (AnimeVerseCsv record in records)
            {
                int seriesId = _db.Series
                    .Where(s => s.Title == record.seriesItem)
                    .Select(s => s.SeriesId)
                    .FirstOrDefault();
                if (charactersByName.ContainsKey(record.character))
                {
                    continue;
                }

                Character character = new()
                {
                    Name = record.character,
                    Age = record.age,
                    Gender = record.gender,
                    SeriesId = seriesId
                };
                await _db.Characters.AddAsync(character);
                charactersByName.Add(record.character, character);
            }

            await _db.SaveChangesAsync();

            return new JsonResult(charactersByName.Count);

        }

        [HttpPost("Genres")]
        public async Task<IActionResult> ImportGenresAsync()
        {
            // Create a lookup dictionary for existing genres in the database
            Dictionary<string, Genre> genresByName = _db.Genres
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<AnimeVerseCsv> records = csv.GetRecords<AnimeVerseCsv>().ToList();
            foreach (AnimeVerseCsv record in records)
            {

                int seriesId = _db.Series
                .Where(s => s.Title == record.seriesItem)
                .Select(s => s.SeriesId)
                .FirstOrDefault();

                foreach (string genreName in record.genres.Split(','))
                {
                    // Trim and normalize genre name
                    string trimmedGenreName = genreName.Trim();

                    // Check if the genre already exists in the database
                    Genre genreItem = genresByName.TryGetValue(trimmedGenreName, out var existingGenre)
                        ? existingGenre
                        : new Genre { Name = trimmedGenreName };

                    if (!genresByName.ContainsKey(trimmedGenreName))
                    {
                        await _db.Genres.AddAsync(genreItem);
                        genresByName.Add(trimmedGenreName, genreItem);
                    }

                    // Create SeriesGenre
                    SeriesGenre seriesGenre = new SeriesGenre
                    {
                        SeriesId = seriesId,
                        GenreId = genreItem.Id // Use the ID of the genre
                    };

                    await _db.SeriesGenres.AddAsync(seriesGenre);

                    // Detach the entities to prevent tracking issues
                    _db.Entry(genreItem).State = EntityState.Detached;
                    _db.Entry(seriesGenre).State = EntityState.Detached;
                }

            }

            // Save changes once, outside the loop
            await _db.SaveChangesAsync();

            return new JsonResult(genresByName.Count);
        }


    }
}
