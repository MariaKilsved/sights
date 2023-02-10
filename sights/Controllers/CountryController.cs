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
    public class CountryController : ControllerBase
    {
        private readonly SqliteContext _context;

        public CountryController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            return await _context.Countries.ToListAsync();
        }

    }
}
