using AnimeVerse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeVerseAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //directs HTTP request to the appropriate controller and endpoint
    public class GenresController : ControllerBase
    {
        private readonly AnimeVerseContext _context;

        public GenresController(AnimeVerseContext context)
        {
            _context = context; //injects the context into the controller
        }

        [HttpGet]
        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
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
