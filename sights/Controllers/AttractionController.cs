using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            if (_context.Attractions == null)
            {
                return NotFound("No attractions to show yet");
            }

            var attractions = await _context.Attractions.ToListAsync();

            return attractions;

        }

        // GET: api/Attraction/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Attraction>> GetAttraction(long id)
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

            return attraction;
        }

        // PUT: api/Attraction/5,this is to update
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAttraction(long id, Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return BadRequest("Please type in Id");
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
                    return NotFound("Did not found any id");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Sucessfully updated!");

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

            if (string.IsNullOrWhiteSpace(attraction.Title))
            {
                return BadRequest("Attraction must have a title");
            }

            bool hasNewCountry = false;
            if (!string.IsNullOrWhiteSpace(attraction?.Country?.Name))
            {
                var findCountry = _context.Countries.Where(x => x.Name == attraction.Country.Name).FirstOrDefault();

                if (findCountry != null)
                {
                    attraction.CountryId = findCountry?.Id;
                    attraction.Country = null;
                }
                else
                {
                    hasNewCountry = true;
                    attraction.Country.UserId = attraction.UserId;
                }
            }

            if (!string.IsNullOrWhiteSpace(attraction?.City?.Name))
            {
                var findCity = _context.Cities.Where(x => x.Name == attraction.City.Name).FirstOrDefault();

                if (findCity != null)
                {
                    attraction.CityId = findCity?.Id;
                    attraction.City = null;
                }
                else
                {
                    if (hasNewCountry)
                    {
                        attraction.City.UserId = attraction.UserId;
                    }
                }
            }



            if (string.IsNullOrWhiteSpace(attraction?.Description))
            {
                return BadRequest("Attraction must have a description");
            }

            _context.Attractions.Add(attraction);
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
        public async Task<ActionResult<IEnumerable<Attraction>>> GetByCity(long? id)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Context is null");
            }
            if (id == null)
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

            return await ByLikesQueryAsync();
        }
        private Task<ActionResult<List<AttractionLike>>> ByLikesQueryAsync() => Task.Run(() => ByLikesQuery());
        private ActionResult<List<AttractionLike>> ByLikesQuery()
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




        // GET: api/Attraction/LikeCount
        [HttpGet("LikeCount/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<long?>> LikeCount(long attractionId)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Attractions context was null");
            }

            if (_context.Likes == null)
            {
                return NotFound("Likes context was null");
            }

            return await LikeCountQueryAsync(attractionId);
        }
        private Task<ActionResult<long?>> LikeCountQueryAsync(long attractionId) => Task.Run(() => LikeCountQuery(attractionId));
        private ActionResult<long?> LikeCountQuery(long attractionId)
        {
            List<AttractionLike> groupedAttractionLikes;

            using (var context = _context)
            {
                var attractionLikes = from attraction in context.Attractions
                                      join like in context.Likes on attraction equals like.Attraction into al
                                      from l in al.DefaultIfEmpty()
                                      where attraction.Id == attractionId
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

            return groupedAttractionLikes?.FirstOrDefault()?.LikeCount;
        }


        // GET: api/Attraction/ByCityAndLikes/{id}
        [HttpGet("ByCityAndLikes/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<AttractionLike>>> ByCityAndLikes(long cityId)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Attractions context was null");
            }

            if (_context.Likes == null)
            {
                return NotFound("Likes context was null");
            }

            return await ByCityAndLikesQueryAsync(cityId);
        }
        private Task<ActionResult<List<AttractionLike>>> ByCityAndLikesQueryAsync(long cityId) => Task.Run(() => ByCityAndLikesQuery(cityId));
        private ActionResult<List<AttractionLike>> ByCityAndLikesQuery(long cityId)
        {
            List<AttractionLike> groupedAttractionLikes;

            using (var context = _context)
            {
                var attractionLikes = from attraction in context.Attractions
                                      join like in context.Likes on attraction equals like.Attraction into al
                                      from l in al.DefaultIfEmpty()
                                      where attraction.CityId == cityId
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


        // GET: api/Attraction/ByCountryAndLikes/{id}
        [HttpGet("ByCountryAndLikes/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<AttractionLike>>> ByCountryAndLikes(long countryId)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Attractions context was null");
            }

            if (_context.Likes == null)
            {
                return NotFound("Likes context was null");
            }

            return await ByCountryAndLikesQueryAsync(countryId);
        }
        private Task<ActionResult<List<AttractionLike>>> ByCountryAndLikesQueryAsync(long countryId) => Task.Run(() => ByCountryAndLikesQuery(countryId));
        private ActionResult<List<AttractionLike>> ByCountryAndLikesQuery(long countryId)
        {
            List<AttractionLike> groupedAttractionLikes;

            using (var context = _context)
            {
                var attractionLikes = from attraction in context.Attractions
                                      join like in context.Likes on attraction equals like.Attraction into al
                                      from l in al.DefaultIfEmpty()
                                      where attraction.CountryId == countryId
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
