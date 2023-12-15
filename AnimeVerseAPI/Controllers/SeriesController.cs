using AnimeVerse;
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
        [Authorize]
        public IEnumerable<SeriesItem> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        [HttpGet("{id}")] // /api/series/2
        [Authorize]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            return await _context.Series.FindAsync(id);
        }
    }
}
