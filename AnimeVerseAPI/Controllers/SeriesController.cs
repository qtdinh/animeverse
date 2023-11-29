using AnimeVerse;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Series>> GetSeries()
        {

            return _context.Series.ToList();
        }

        [HttpGet("{id}")] // /api/series/2
        public ActionResult<Series> GetSeries(int id)
        {
            return _context.Series.Find(id);
        }
    }
}
