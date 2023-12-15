using AnimeVerse;
using AnimeVerseAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeVerseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //directs HTTP request to the appropriate controller and endpoint
    public class SeriesController : ControllerBase //derives from class called ControllerBase
    {
        private readonly AnimeVerseContext _context;

        public SeriesController(AnimeVerseContext context)
        {
            _context = context; //injects the context into the controller
        }

        [HttpGet]
        public IEnumerable<SeriesDTO> GetSeries()
        {
            var seriesWithGenres = _context.Series
                .Include(s => s.SeriesGenres)
                .ThenInclude(sg => sg.Genre)
            .ToList();

            var seriesDtos = seriesWithGenres.Select(series => new SeriesDTO
            {
                SeriesId = series.SeriesId,
                Title = series.Title,
                Demographic = series.Demographic,
                Year = series.Year,
                Genres = series.SeriesGenres.Select(sg => sg.Genre.Name)
            });

            return seriesDtos;
        }

        [HttpGet("{id}")] // /api/series/2
        [Authorize]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            return await _context.Series.FindAsync(id);
        }
    }
}
