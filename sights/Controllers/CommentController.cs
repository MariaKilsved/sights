using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sights.Models;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly SqliteContext _context;

        public CommentController(SqliteContext context)
        {
            _context = context;
        }


        // GET: api/Comment/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Comment>> GetComment(long id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comment/ByAttraction
        [HttpGet("ByAttraction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<CommentUser>>> ByAttraction(long attractionId)
        {
           return await ByAttractionAsync(attractionId); 
        }

        private Task<ActionResult<List<CommentUser>>> ByAttractionAsync(long attractionId) => Task.Run(() => ByAttractionQuery(attractionId));
        private ActionResult<List<CommentUser>> ByAttractionQuery(long attractionId)
        {
            List<CommentUser> groupedCommentUser;

            using(var context = _context)
            {
                groupedCommentUser = (from attraction in context.Attractions
                                  join comment in context.Comments on attraction equals comment.Attraction
                                  join user in context.Users on comment.UserId equals user.Id
                                  where (attraction.Id == attractionId)
                                  select new CommentUser
                                  {
                                        Comment = comment,
                                        Username = user.Username,
                                  }).ToList();
            }
            return groupedCommentUser;
        }

        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
          if (_context.Comments == null)
          {
              return NotFound("Entity set 'SqliteContext.Comments'  is null.");
          }

          if(comment.UserId == null)
          {
                return BadRequest("userId is null");

          }
            if (comment.AttractionId == null)
            {
                return BadRequest("AttractionId is null");
            }
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                return BadRequest("Content can't be empty");
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

    }
}
