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

        // GET api/<CountriesController>/5
        [HttpGet(template: "{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CountriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

