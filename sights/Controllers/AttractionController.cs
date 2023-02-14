using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sights.Models;
using sights.Utility;
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

        // POST: api/Attraction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<Attraction>> PostAttraction(Attraction attraction)
        {
            if (_context.Attractions == null)
            {
                return NotFound("Entity set 'SqliteContext.Attractions' is null.");
            }

            if (_context.Cities == null)
            {
                return NotFound("Entity set 'SqliteContext.Cities' is null.");
            }

            if (_context.Countries == null)
            {
                return NotFound("Entity set 'SqliteContext.Countries'  is null.");
            }

            if (attraction == null)
            {
                return BadRequest("Attraction is null");
            }

            if (attraction.UserId == null)
            {
                return BadRequest("UserID is null");
            }

            if (string.IsNullOrWhiteSpace(attraction.Title))
            {
                return BadRequest("Attraction must have a title");
            }

            if (string.IsNullOrWhiteSpace(attraction?.Description))
            {
                return BadRequest("Attraction must have a description");
            }

            attraction.Title = attraction.Title.Trim();
            attraction.Description = attraction.Description.Trim();

            attraction.Title = Utility.Utility.FirstLetterToUpper(attraction.Title);
            attraction.Description = Utility.Utility.FirstLetterToUpper(attraction.Description);

            //Double-checking due to static method
            if (attraction == null)
            {
                return BadRequest("Attraction is null");
            }

            //First  checking if the country has already been added or not
            bool hasNewCountry = false;
            if (!string.IsNullOrWhiteSpace(attraction?.Country?.Name))
            {
                var findCountry = _context.Countries.Where(x => x.Name.ToUpper() == attraction.Country.Name.ToUpper()).FirstOrDefault();

                //If the country was found
                if (findCountry != null)
                {
                    attraction.CountryId = findCountry?.Id;
                    attraction.Country = findCountry;
                }
                //If the country didn't exist
                else
                {
                    hasNewCountry = true;
                    attraction.Country.UserId = attraction.UserId;
                    attraction.Country.Name = attraction.Country.Name.Trim();
                    attraction.Country.Name = Utility.Utility.FirstLetterToUpper(attraction.Country.Name);
                }
            }

            //Then, checking if the city already existed or not
            if (!string.IsNullOrWhiteSpace(attraction?.City?.Name))
            {
                var findCity = _context.Cities.Where(x => x.Name == Utility.Utility.FirstLetterToUpper(attraction.City.Name)).FirstOrDefault();

                //If the city was found
                if (findCity != null)
                {
                    attraction.City = findCity;
                    attraction.CityId = findCity?.Id;
                }
                //If the city didn't exist
                else
                {
                    attraction.City.Name = attraction.City.Name.Trim();
                    attraction.City.Name = Utility.Utility.FirstLetterToUpper(attraction.City.Name);

                    //If the city didn't exist, and the country was new as well
                    if (hasNewCountry)
                    {
                        attraction.City.Country = attraction.Country;

                    }
                    //If the city didn't exist, but the country already did
                    else
                    {
                        attraction.City.CountryId = attraction.CountryId;
                    }
                    attraction.City.UserId = attraction.UserId;
                }
            }

            _context.Attractions.Add(attraction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttraction", new { id = attraction.Id }, attraction);
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
    }
}
