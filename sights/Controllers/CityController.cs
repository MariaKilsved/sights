using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sights.Utility;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly SqliteContext _context;

        public CityController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/City
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            return await _context.Cities.ToListAsync();
        }
            
    }
}
