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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<AttractionBase64>>> GetAttractions()
        {
            if (_context.Attractions == null)
            {
                return NotFound("No attractions to show yet");
            }

            var attractions = await _context.Attractions.ToListAsync();
            List<AttractionBase64> attractions2 = new();

            foreach (var attraction in attractions)
            {
                //Convert Picture
                var attraction2 = new AttractionBase64
                {
                    Id = attraction.Id,
                    Coordinates = attraction.Coordinates,
                    Title = attraction.Title,
                    Description = attraction.Description,
                    UserId = attraction.UserId,
                    CountryId = attraction.CountryId,
                    CityId = attraction.CityId
                };

                if (attraction.Picture != null)
                {
                    attraction2.Picture = Convert.ToBase64String(attraction.Picture);
                }

                attractions2.Add(attraction2);
            }

            return attractions2;

        }

        // GET: api/Attraction/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AttractionBase64>> GetAttraction(long id)
        {
          if (_context.Attractions == null)
          {
              return NotFound();
          }
            var attraction = await _context.Attractions.FindAsync(id);

            if (attraction == null)
            {
                return NotFound("That attraction does not exist");
            }

            //Convert Picture
            var attraction2 = new AttractionBase64
            {
                Id = attraction.Id,
                Coordinates = attraction.Coordinates,
                Title = attraction.Title,
                Description = attraction.Description,
                UserId = attraction.UserId,
                CountryId = attraction.CountryId,
                CityId = attraction.CityId
            };

            if (attraction.Picture != null)
            {
                attraction2.Picture = Convert.ToBase64String(attraction.Picture);
            }


            return attraction2;
        }

        // PUT: api/Attraction/5,this is to update
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAttraction(long id, AttractionBase64 attraction)
        {
            if (id != attraction.Id)
            {
                return BadRequest("Please type in Id");
            }

            //Convert Picture
            var attraction2 = new Attraction
            {
                Id = attraction.Id,
                Coordinates = attraction.Coordinates,
                Title = attraction.Title,
                Description = attraction.Description,
                UserId = attraction.UserId,
                CountryId = attraction.CountryId,
                CityId = attraction.CityId
            };

            if (attraction.Picture != null)
            {
                attraction2.Picture = Convert.FromBase64String(attraction.Picture);
            }

            _context.Entry(attraction2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
                {
                    return NotFound("Did not found any id");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Sucessfully updated!");

        }

        // TO-DO: Save country & city input to Id's 
        // POST: api/Attraction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Attraction>> PostAttraction(AttractionBase64 attraction)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Entity set 'SqliteContext.Attractions'  is null.");
            }

            if (attraction.UserId == null)
            {
                return BadRequest("UserID is null");
            }

            if (string.IsNullOrWhiteSpace(attraction.Title))
            {
                return BadRequest("Attraction must have a title");
            }

            if (string.IsNullOrWhiteSpace(attraction.Description))
            {
                return BadRequest("Attraction must have a description");
            }

            //Convert Picture
            var attraction2 = new Attraction
            {
                Id = attraction.Id,
                Coordinates = attraction.Coordinates,
                Title = attraction.Title,
                Description = attraction.Description,
                UserId = attraction.UserId,
                CountryId = attraction.CountryId,
                CityId = attraction.CityId
            };

            if(attraction.Picture != null)
            {
                attraction2.Picture = Convert.FromBase64String(attraction.Picture);
            }

            _context.Attractions.Add(attraction2);
            await _context.SaveChangesAsync();
         
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
        public async Task<ActionResult<IEnumerable<AttractionBase64>>> GetByCity(long? id)
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

            List<AttractionBase64> attractions2 = new();

            foreach (var attraction in attractions)
            {
                //Convert Picture
                var attraction2 = new AttractionBase64
                {
                    Id = attraction.Id,
                    Coordinates = attraction.Coordinates,
                    Title = attraction.Title,
                    Description = attraction.Description,
                    UserId = attraction.UserId,
                    CountryId = attraction.CountryId,
                    CityId = attraction.CityId
                };

                if (attraction.Picture != null)
                {
                    attraction2.Picture = Convert.ToBase64String(attraction.Picture);
                }

                attractions2.Add(attraction2);
            }

            return attractions2;
        }

        //By country id
        [HttpGet("ByCountry/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<AttractionBase64>>> GetByCountry(long? id)
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


            List<AttractionBase64> attractions2 = new();

            foreach (var attraction in attractions)
            {
                //Convert Picture
                var attraction2 = new AttractionBase64
                {
                    Id = attraction.Id,
                    Coordinates = attraction.Coordinates,
                    Title = attraction.Title,
                    Description = attraction.Description,
                    UserId = attraction.UserId,
                    CountryId = attraction.CountryId,
                    CityId = attraction.CityId
                };

                if (attraction.Picture != null)
                {
                    attraction2.Picture = Convert.ToBase64String(attraction.Picture);
                }

                attractions2.Add(attraction2);
            }

            return attractions2;
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

            return await AttractionLikeQueryAsync();
        }
        private Task<ActionResult<List<AttractionLike>>> AttractionLikeQueryAsync() => Task.Run(() => AttractionLikeQuery());
        private ActionResult<List<AttractionLike>> AttractionLikeQuery()
        {
            List<AttractionLike> groupedAttractionLikes;

            using (var context = _context)
            {
                var attractionLikes = from attraction in context.Attractions
                                      join like in context.Likes on attraction equals like.Attraction into al
                                      from l in al.DefaultIfEmpty()
                                      select new AttractionLike
                                      {
                                          Attraction = attraction,
                                          LikeCount = l.Like1 ?? 0
                                      };

                groupedAttractionLikes = (from al in attractionLikes
                                          group al by al.Attraction.Id)
                                              .Select(group => new AttractionLike
                                              {
                                                  Attraction = group.ToList().First().Attraction,
                                                  LikeCount = group.ToList().Sum(item => item.LikeCount)
                                              })
                                              .OrderByDescending(g => g.LikeCount)
                                              .ToList();
            }
            return groupedAttractionLikes;
        }
    }
}
