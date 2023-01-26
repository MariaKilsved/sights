using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using sights.Models;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionController : ControllerBase
    {
        private readonly SqliteContext _context;

        public AttractionController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/Attraction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
          if (_context.Attractions == null)
          {
              return NotFound();
          }
            return await _context.Attractions.ToListAsync();
        }

        // GET: api/Attraction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attraction>> GetAttraction(long id)
        {
          if (_context.Attractions == null)
          {
              return NotFound();
          }
            var attraction = await _context.Attractions.FindAsync(id);

            if (attraction == null)
            {
                return NotFound();
            }

            return attraction;
        }

        // PUT: api/Attraction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttraction(long id, Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return BadRequest();
            }

            _context.Entry(attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
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

        // POST: api/Attraction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Attraction>> PostAttraction(Attraction attraction)
        {
          if (_context.Attractions == null)
          {
              return NotFound("Entity set 'SqliteContext.Attractions'  is null.");
          }

          if (attraction.UserId == null)
          {
                return BadRequest("UserID is null");
          }
            _context.Attractions.Add(attraction);
            await _context.SaveChangesAsync();

            if (string.IsNullOrWhiteSpace(attraction.Title))
            {
                return BadRequest("Attraction must have a title");
            }

            if (string.IsNullOrWhiteSpace(attraction.Description))
            {
                return BadRequest("Attraction must have a description");
            }

         
            return CreatedAtAction("GetAttraction", new { id = attraction.Id }, attraction);
        }

        // DELETE: api/Attraction/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAttraction(long id)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Context is null");
            }
        
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound("Delete from database failed. This attraction did not exist, before you tried to delete it.");
            }

            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttractionExists(long id)
        {
            return (_context.Attractions?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        //By city id
        [HttpGet("ByCity/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetByCity(long? id)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Context is null");
            }
            if(id == null)
            {
                return BadRequest("City id is null");
            }

            var attractions = await _context.Attractions.Where(a => a.CityId == id).ToListAsync(); //This is city id

            if (!attractions.Any())
            {
                return NotFound("No attraction was found in this city.");
            }

            return attractions;
        }

        //By country id
        [HttpGet("ByCountry/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetByCountry(long? id)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Context is null");
            }
            if (id == null)
            {
                return BadRequest("Country id is null");
            }

            var attractions = await _context.Attractions.Where(a => a.CountryId == id).ToListAsync(); //This is country id

            if (!attractions.Any())
            {
                return NotFound("No attraction was found in this country.");
            }

            return attractions;
        }

        // GET: api/Attraction/ByLikes
        [HttpGet("ByLikes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<AttractionLike>>> ByLikes()
        {
            if (_context.Attractions == null)
            {
                return NotFound("Attractions context was null");
            }

            if (_context.Likes == null)
            {
                return NotFound("Likes context was null");
            }

            List<AttractionLike> attractionLikes;

            using (var context = _context) {
                attractionLikes = await _context.Attractions.Join(_context.Likes,
                a => a.Id,
                l => l.AttractionId, (a, l) => new AttractionLike
                {
                    AttractionId = a.Id,
                    Coordinates = a.Coordinates,
                    Title = a.Title,
                    Description = a.Description,
                    UserId = a.UserId,
                    Picture = a.Picture,
                    CountryId = a.CountryId,
                    CityId = a.CityId,
                    LikeId = l.Id,
                    Like1 = l.Like1
                }).ToListAsync();
            }

            /*
            var groupedAttractionLikes = (from al in attractionLikes 
                                          group al by al.AttractionId).ToList();
            */

            var groupedAttractionLikes = (from al in attractionLikes
                                          group al by al.AttractionId)
                                          .Select(group => new { Id = group.Key,  Items = group.ToList()})
                                          .ToList();




            return attractions;
        }
    }
}
