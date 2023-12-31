﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeVerse;
using AnimeVerseAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AnimeVerseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly AnimeVerseContext _context;

        public CharacterController(AnimeVerseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<CharacterDTO> GetCharacters()
        {
            var characters = _context.Characters
                .Include(c => c.SeriesItem) // Ensure SeriesItem is loaded
                .ToList();

            var characterDTOs = characters.Select(character => new CharacterDTO
            {
                CharacterId = character.CharacterId,
                Name = character.Name,
                Age = character.Age,
                Gender = character.Gender,
                Series = character.SeriesItem.Title // Use only the title
            });

            return characterDTOs;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            if (_context.Characters == null)
            {
                return Problem("Entity set 'AnimeVerse.Characters' is null.");
            }
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.CharacterId }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return (_context.Characters?.Any(e => e.CharacterId == id)).GetValueOrDefault();
        }
    }
}
