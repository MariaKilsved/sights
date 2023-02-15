using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sights.JwtAuthorization;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqliteContext _context;
        private readonly JwtSettings _jwtSettings;


        public UserController(SqliteContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
            if(user == null)
            {
                return BadRequest("User is null.");
            }
            if(string.IsNullOrWhiteSpace(user.Username))
            {
                return BadRequest("User must have a username");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("User must have a password");
            }

            var findUser = _context.Users.Where(x => x.Username == user.Username).FirstOrDefault();

            //If the username was found
            if (findUser != null)
            {
                return BadRequest("The username already exists");
            }

            //Encrypt password
            string encryptedPassword = Utility.Encryption.HashPbkdf2(user.Password);
            user.Password = encryptedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("Login")]
        [ProducesResponseType(200, Type = typeof(JwtUserToken))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<User>> Login(string username, string? password)
        {
            if (_context.Users == null)
            {
                return NotFound("Context was null");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Must have a username");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Must have a password");
            }

            //Testing for username only first
            IEnumerable<User> Users = await _context.Users.Where(u => u.Username == username).ToListAsync();

            if (!Users.Any())
            {
                return NotFound("Username is incorrect or user does not exist.");
            }

            //Encrypt password
            string encryptedPassword = Utility.Encryption.HashPbkdf2(password);

            //Testing for both username and password
            IEnumerable<User> Users2 = await _context.Users.Where(u => u.Username == username && u.Password == encryptedPassword).ToListAsync();

            if (!Users2.Any())
            {
                return BadRequest("Password is incorrect.");
            }

            JwtUserToken Token = JwtAuthorization.JwtAuthorization.CreateJwtTokenKey(new JwtUserToken()
            {
                UserName = username,
                UserId = Users2.First().Id,
            }, _jwtSettings); ;

            return Ok(Token);
        }
    }
}
