using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly SqliteContext _context;

        public LikeController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/Like
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
          if (_context.Likes == null)
          {
              return NotFound("Context was null");
          }
            return await _context.Likes.ToListAsync();
        }

        // GET: api/Like/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Like>> GetLike(long id)
        {
          if (_context.Likes == null)
          {
              return NotFound("Context was null");
          }
            var like = await _context.Likes.FindAsync(id);

            if (like == null)
            {
                return NotFound("Like was null");
            }

            return like;
        }

        // POST: api/Like
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            if (_context.Likes == null)
            {
                return NotFound("Entity set 'SqliteContext.Likes'  is null.");
            }
            if(like.UserId == null)
            {
                return BadRequest("UserId is null");
            }
            if(like.AttractionId== null)
            {
                return BadRequest("AttractionId is null");
            }
            if(like.Like1== null)
            {
                return BadRequest("Like is null");
            }
            if(like.Like1 != 1 && like.Like1 != 0)
            {
                return BadRequest("Like must be either 0 or 1");
            }

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Like/5
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> DeleteLike(long id, long? attractionId, long? userId)
        {
            if (_context.Likes == null)
            {
                return NotFound("Context is null");
            }
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound("There is no like for this Id");
            }

            if (like.UserId == null)
            {
                return BadRequest("UserId is null");
            }

            if (like.AttractionId == null)
            {
                return BadRequest("AttractionId is null");
            }

            if (like.Like1 == null)
            {
                return BadRequest("There is no like or dislike for this Id");
            }

            if (like.UserId != userId)
            {
                return Unauthorized("UserIds does not match");
            }

            if (like.AttractionId != attractionId)
            {
                return BadRequest("AttractionIds does not match");
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
