using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sqlite.Data;
using sqlite.Models;
using Microsoft.AspNetCore.Authorization;
using sights.JwtAuthorization;
using sights.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqliteContext _context;

        public UserController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return NotFound("Entity set 'SqliteContext.Users'  is null.");
          }

          //Will complain of empty request body instead
          if (user == null)
          {
              return BadRequest("User is null.");
          }
          
          if(user.Username == null)
          {
              return BadRequest("Username is null");
          }

          if(user.Password == null)
          {
               return BadRequest("Password is null");
          }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Login")]
        [ProducesResponseType(200, Type = typeof(JwtUserToken))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> Login(string username, string? password)
            
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            //Testing for username only first
            IEnumerable<User> Users1 = await _context.Users.Where(u => u.Username == username).ToListAsync();

            if (!Users1.Any())
            {
                return NotFound("Username is incorrect or user does not exist.");
            }

            //Testing for both username and password
            IEnumerable<User> Users2 = await _context.Users.Where(u => u.Username == username).ToListAsync();

            if (!Users2.Any())
            {
                return BadRequest("Password is incorrect.");
            }

            return Users2.First();
           

        }
    }
}
